import { CreationOptional, DataTypes, Model, Sequelize } from "sequelize";

export const sequelize = new Sequelize('sqlite:logistix-db', {
    logging: false
});

/* Models */

export interface UserModel {
    email: string
    name: string
    completed : string
    iron : number
    wood : number
    premium : boolean
}

export interface BugModel {
    id : number
    bug : string
    player : string
    device : string
    ram : string
    fixed : boolean
}

export interface InviteModel {
    inviter : string
    ip : string
    redeemed : boolean
}

export interface ResultModel {
    mapid : string
    iswin : boolean
    score : number
    lostboxes : number
    submitted : number
    email : string
    usedmultiplies : boolean
}

/* Tables */

export class User extends Model<UserModel>
{
    declare email : string
    declare name : CreationOptional<string>
    declare completed : CreationOptional<string>
    declare iron : CreationOptional<number>
    declare wood : CreationOptional<number>
    declare premium : CreationOptional<boolean>
    declare createdAt: CreationOptional<Date>
    declare updatedAt: CreationOptional<Date>
}

export class Bug extends Model<BugModel>
{
    declare id : CreationOptional<number>
    declare bug : string
    declare player : string
    declare device : string
    declare ram : string
    declare fixed : CreationOptional<boolean>
    declare createdAt: CreationOptional<Date>
    declare updatedAt: CreationOptional<Date>
}

export class Invite extends Model<InviteModel>
{
    declare inviter : string
    declare ip : string
    declare redeemed : boolean
    declare createdAt: CreationOptional<Date>
    declare updatedAt: CreationOptional<Date>
}

export class Result extends Model<ResultModel>
{
    declare mapid : string
    declare iswin : boolean
    declare score : number
    declare lostboxes : number
    declare submitted : number
    declare email : string
    declare usedmultiplies : boolean
    declare createdAt: CreationOptional<Date>
    declare updatedAt: CreationOptional<Date>
}

export async function initdb() 
{
    User.init({
    email : {
        type : DataTypes.STRING,
        primaryKey : true
    },
    name : {
        type : DataTypes.STRING,
        defaultValue : null
    },
    completed : {
        type : DataTypes.STRING,
        defaultValue : ''
    },
    iron : {
        type : DataTypes.INTEGER,
        defaultValue : 0      
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

Bug.init({
    id : {
        type : DataTypes.INTEGER,
        primaryKey : true,
        autoIncrement : true,
    },
    bug : {
        type : DataTypes.STRING,
    },
    player : {
        type : DataTypes.STRING,
    },
    device : {
        type : DataTypes.STRING,      
    },
    ram : {
        type : DataTypes.STRING,
        defaultValue : 0
    },
    fixed : {
        type : DataTypes.BOOLEAN,
        defaultValue : false
    }
    },{
        sequelize
    })    

Invite.init({
    inviter : {
        type : DataTypes.STRING,
    },
    ip : {
        type : DataTypes.STRING,
    },
    redeemed : {
        type : DataTypes.BOOLEAN,
    },
    },{
        sequelize
    })

Result.init({
    mapid : {
        type : DataTypes.INTEGER,
    },
    iswin : {
        type : DataTypes.BOOLEAN,
    },
    score : {
        type : DataTypes.INTEGER,
    },
    lostboxes : {
        type : DataTypes.INTEGER,
        allowNull : true        
    },
    submitted : {
        type : DataTypes.INTEGER,
        defaultValue : 0
    },
    email : {
        type : DataTypes.TEXT,
        defaultValue : 0
    },
    usedmultiplies : {
        type : DataTypes.BOOLEAN,
        defaultValue : 0
    }
    },{
        sequelize
    })

    await sequelize.sync({force : true}) 
}

export async function toplist(id : string)
{
    const results = await Result.findAll({ where : { mapid : id }})
    const users = await User.findAll()
    const list = new Array()

    results.sort((a,b) => a.score - b.score)
    for (let i = 0; i < 10 && results.length > i; i++) 
    {
        let current = null;
        for (const user of users) 
        {
            if(user.email == results[i].email)
            {
                current = user
            }
        }

        list.push({
            name : current?.name,
            premium : current?.premium,
            score : results[i].score
        })   
    }

    return list
}

export async function countPlayerInvitesSQL(mail : string)
{
    return {    
        text: 'SELECT count(DISTINCT iphash) from invites, users WHERE email = $1 AND inviter = name GROUP BY inviter',
        values: [mail]
    }
}

export async function addReferSQL(name : string, ip : string)
{
    return {    
        text: 'INSERT INTO invites (inviter, iphash, date) VALUES ($1, $2, $3)',
        values: [name, ip, new Date().toString()]
    }
}

export async function updateUserStat(mail : string, mapid : number, iron : number, wood : number)
{
    const user = await User.findOne({ where : { email : mail} })
    user.iron += iron
    user.wood += wood
    
    const completed = new Set(...user.completed.split('|'))
    completed.add(`${mapid}`)
    user.completed = [...completed].join('|')

    return await user.save()
}

export async function uploadResult(mail : string, mapid : string, iswin : string, score : string, lostboxes : string, submitted : string, usedmultiplies : string)
{
    await Result.create({
        email : mail,
        iswin : iswin == 'true',
        lostboxes : Number.parseInt(lostboxes),
        mapid : mapid,
        score : Number.parseInt(score),
        submitted : Number.parseInt(submitted),
        usedmultiplies : usedmultiplies == 'true',
    })
}

export async function findUser(mail : string)
{
    return await User.findOne({ where : { email : mail} })
}

export async function createUser(mail : string)
{
    return await User.create({
        email : mail
    })
}

export async function renameUser(mail : string, name : string)
{
    const user = await User.findOne({ where : { email : mail } })
    user.name = name
    return await user.save()
}

export async function makePremiumSQL(email : string)
{
    return {    
        text : "UPDATE users SET premium = true WHERE email = $1",
        values: [email],
    }
}

/* Bugs */

export async function postBugSQL(bug : string, player : string, device : string, ram : string)
{
    const model = await Bug.create({ 
        bug,
        player,
        device,
        ram,
    })

    return model
}

export async function fixBugSQL(id : string)
{
    const bug = await Bug.findOne({ where : { id }})
    bug.fixed = true
    await bug.save()

    return bug
}

export async function getBugsSQL() 
{
    return await Bug.findAll()
}