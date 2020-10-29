const express = require('express')
const db = require('../db/database')
var router = express.Router()


router.post('/', function (req, res) 
{
    var name = req.body.name
    var email = req.body.email

    db.query(db.rename(email,name), 
    (player) =>
    { 
        console.log("[Name] change: " + email + " -> " + name)
        res.send(player.rows[0]) 
    }
    )

})

module.exports = router