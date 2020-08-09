const express = require('express')
const db = require('../database')
var router = express.Router()


router.post('/', function (req, res) 
{
    var name = req.body.name
    var email = req.body.email

    db.query(rename(email,name), 
    (player) =>
    { 
        console.log("[Name] change: " + email + " -> " + name)
        res.send(player.rows[0]) 
    }
    )

})

function rename(email,name)
{
    return {    
        text : "UPDATE users SET name = $1 WHERE email = $2 RETURNING *;",
        values: [name,email],
    }
}

module.exports = router