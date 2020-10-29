const { Pool } = require('pg')
const CONNECTION_STRING = "postgres://mkmqubdsjwslth:6bdab8dc7a89cba14b89003536347c4b2393b776b5916f5f160beb1f418f9297@ec2-54-246-89-234.eu-west-1.compute.amazonaws.com:5432/deh00d9545rhu3";

const pool = new Pool
(
    {
        connectionString: 
        process.env.DATABASE_URL || CONNECTION_STRING,
        ssl: { rejectUnauthorized: false },
        connectionTimeoutMillis: 1000,
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
    }

}