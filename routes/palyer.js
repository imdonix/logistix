const express = require('express')
const db = require('../db/database')
var router = express.Router()

router.post('/', (req, res) =>
{
    var mail = req.body.email;

    db.query(find(mail), (player) =>
    {
        if(player.rows.length > 0)
        {
            console.log("[Player] requested: " + mail);
            res.send(player.rows[0]);
        }
        else
        {
            console.log("[Player] New inicialized: " + mail);
            db.query(construct(mail), (newplayer,err) => 
            {
                res.send(newplayer.rows[0]);
            });
        }
    });
})


function construct(mail)
{
    return {    
        text: 'INSERT INTO users(email) VALUES($1) RETURNING *',
        values: [mail],
    }
}

function find(mail)
{
    return {
        text: 'SELECT * FROM users WHERE email = $1',
        values: [mail],
    }
}

module.exports = router