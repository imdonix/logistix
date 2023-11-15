import { DataTypes, Model, Sequelize } from "sequelize";
import { Database } from "sqlite3";

const db = new Database('logistix-db')
const sequelize = new Sequelize('sqlite:dobabDB', {
    logging: false
});


CREATE TABLE bugs
(
    id bigint NOT NULL DEFAULT ,
    bug text,
    player text,
    device text,
    ram text,
    fixed boolean DEFAULT false,
    date date NOT NULL DEFAULT now(),
)

export interface UserModel {
    email: string
    name: string
    completed : string
    iron : number
    wood : number
    premium : boolean
}

export interface BugModel {
    id : number,
    bug : string
    player : string
    device : string
    ram : string
}


export class User extends Model<UserModel>
{
    declare email : string
    declare name : string
    declare completed : string
    declare iron : number
    declare wood : number
    declare premium : boolean
}

User.init({
    email : {
        type : DataTypes.STRING,
        primaryKey : true
    },
    name : {
        type : DataTypes.STRING,
    },
    completed : {
        type : DataTypes.STRING,
    },
    iron : {
        type : DataTypes.INTEGER,
        allowNull : true        
    },
    wood : {
        type : DataTypes.INTEGER,
        defaultValue : 0
    },
    premium : {
        type : DataTypes.BOOLEAN,
        defaultValue : 0
    }
},{
    sequelize
})

    








export const findToplistSQL = (id : string) =>
{
    return {
        text: 'SELECT name, premium, MAX(score) as score FROM result, users WHERE users.email = result.email AND mapid = $1 AND iswin GROUP BY name, premium ORDER BY MAX(score) DESC',
        values: [id],
    }
}

export const countPlayerInvitesSQL = (mail : string) =>
{
    return {    
        text: 'SELECT count(DISTINCT iphash) from invites, users WHERE email = $1 AND inviter = name GROUP BY inviter',
        values: [mail]
    }
}

export const addReferSQL = (name : string, ip : string) =>
{
    return {    
        text: 'INSERT INTO invites (inviter, iphash, date) VALUES ($1, $2, $3)',
        values: [name, ip, new Date().toString()]
    }
}

export const updatePlayerStatSQL = (mail : string, mapid : string, iron : string, wood : string) =>
{
    return {    
        text: 'UPDATE users SET completed = $1::integer || completed, iron = iron + $2, wood = wood + $3 WHERE email = $4',
        values: [mapid, iron, wood, mail],
    }
}

export const uploadResultSQL = (mail : string, mapid : string, iswin : string, score : string, lostboxes : string, submitted : string, usedmultiplies : string) =>
{
    return {    
        text: 'INSERT INTO result (mapid, iswin, score, lostboxes, submitted, email, usedmultiplies) VALUES ($1, $2, $3, $4, $5, $6, $7)',
        values: [mapid, iswin, score, lostboxes, submitted, mail, usedmultiplies],
    }
}

export const findSQL = (mail : string) =>
{
    return {
        text: 'SELECT * FROM users WHERE email = $1',
        values: [mail],
    }
}

export const constructSQL = (mail : string) =>
{
    return {    
        text: 'INSERT INTO users(email) VALUES($1) RETURNING *',
        values: [mail],
    }
}

export const renameSQL = (email : string,name : string) =>
{
    return {    
        text : "UPDATE users SET name = $1 WHERE email = $2 RETURNING *;",
        values: [name,email],
    }
}

export const makePremiumSQL = (email : string) =>
{
    return {    
        text : "UPDATE users SET premium = true WHERE email = $1",
        values: [email],
    }
}


export const getLevelMapSQL = () =>
{
    return {
        text: "SELECT map FROM levels ORDER BY version DESC LIMIT 1"
    }
}

export const updateLevelMapSQL = (levelMapJSON : string) =>
{
    return {
        text: "INSERT INTO levels (map) VALUES ($1);",
        values: [levelMapJSON]
    }
}

export const postBugSQL = (bug : string, player : string, device : string, ram : string) =>
{
    return {
        text: "INSERT INTO bugs (bug, player, device, ram) VALUES ($1, $2, $3, $4)",
        values: [bug, player, device, ram]
    }
}

export const fixBugSQL = (id : string) =>
{
    return {
        text: "UPDATE bugs SET fixed = 'true' WHERE id = $1",
        values: [id]
    }
}

export const getBugsSQL = () =>
{
    return {
        text: "SELECT * FROM bugs",
    }
}