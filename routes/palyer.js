const express = require('express')
const db = require('../db/database')
var router = express.Router()

router.post('/', (req, res) =>
{
    var mail = req.body.email;

    db.query(db.find(mail), (player) =>
    {
        if(player.rows.length > 0)
        {
            console.log("[Player] requested: " + mail);
            res.send(player.rows[0]);
        }
        else
        {
            console.log("[Player] New inicialized: " + mail);
            db.query(db.construct(mail), (newplayer,err) => 
            {
                res.send(newplayer.rows[0]);
            });
        }
    });
})

module.exports = router