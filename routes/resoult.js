const express = require('express')
const db = require('../db/database')
var router = express.Router()


router.post('/', function (req, res) 
{
    var mail = req.body.email;
    var resoult = JSON.parse(req.body.resoult);

    if(resoult.iswin)
    {
        db.query(db.updatePlayerStat(mail, resoult.mapid, resoult.iron, resoult.wood), 
        (_) => 
        {
            console.log("[Resoult] Player updated: " + mail)
            sendPlayeBack(mail, res);
        })
    }
    else
    {
        sendPlayeBack(mail, res)
    }

    db.query(db.uploadResoult(mail, resoult.mapid, resoult.iswin, resoult.score, resoult.lostboxes, resoult.time, resoult.usedmultiplies), 
    (_) => console.log("[Resoult] New resoult added: " + mail + " | " + resoult.mapid))
})


function sendPlayeBack(mail, res)
{
    db.query(db.find(mail), (player) => res.send(player.rows[0]));
}

module.exports = router