const { Pool } = require('pg')
const settings = require('./../settings')

const pool = new Pool
(
    {
        connectionString: settings.DATABASE_URL,
        ssl: { rejectUnauthorized: false },
        connectionTimeoutMillis: 10000,
        idleTimeoutMillis: 200,
    }
)

module.exports = 
{
    query: (query, callback) => 
    {
        const start = Date.now()
        return pool.query(query, (err, res) => 
        {
          if(err) throw err;
          callback(res)
        })
    },
    
    findToplist: (id) =>
    {
        return {
            text: 'SELECT name, premium, MAX(score) as score FROM resoults, users WHERE users.email = resoults.email AND mapid = $1 AND iswin GROUP BY name, premium ORDER BY MAX(score) DESC',
            values: [id],
        }
    },

    countPlayerInvites: (mail) =>
    {
        return {    
            text: 'SELECT count(DISTINCT iphash) from invites, users WHERE email = $1 AND inviter = name GROUP BY inviter',
            values: [mail]
        }
    },

    addRefer: (name, ip) =>
    {
        return {    
            text: 'INSERT INTO invites (inviter, iphash, date) VALUES ($1, $2, $3)',
            values: [name, ip, new Date()]
        }
    },

    updatePlayerStat: (mail, mapid, iron, wood) =>
    {
        return {    
            text: 'UPDATE users SET completed = $1::integer || completed, iron = iron + $2, wood = wood + $3 WHERE email = $4',
            values: [mapid, iron, wood, mail],
        }
    },

    uploadResoult: (mail, mapid, iswin, score, lostboxes, time, usedmultiplies) =>
    {
        return {    
            text: 'INSERT INTO resoults (mapid, iswin, score, lostboxes, time, email, usedmultiplies) VALUES ($1, $2, $3, $4, $5, $6, $7)',
            values: [mapid, iswin, score, lostboxes, time, mail, usedmultiplies],
        }
    },

    find: (mail) =>
    {
        return {
            text: 'SELECT * FROM users WHERE email = $1',
            values: [mail],
        }
    },

    construct: (mail) =>
    {
        return {    
            text: 'INSERT INTO users(email) VALUES($1) RETURNING *',
            values: [mail],
        }
    },

    rename: (email,name) =>
    {
        return {    
            text : "UPDATE users SET name = $1 WHERE email = $2 RETURNING *;",
            values: [name,email],
        }
    },

    makePremium: (email) =>
    {
        return {    
            text : "UPDATE users SET premium = true WHERE email = $1",
            values: [email],
        }
    },

    getLevelMap: () =>
    {
        return {
            text: "SELECT map FROM levels ORDER BY version DESC LIMIT 1"
        }
    },

    updateLevelMap: (levelMap) =>
    {
        return {
            text: "INSERT INTO levels (map) VALUES ($1);",
            values: [levelMap]
        }
    }

}