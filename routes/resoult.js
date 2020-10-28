const express = require('express')
const db = require('../db/database')
var router = express.Router()


router.post('/', function (req, res) 
{
    var mail = req.body.email;
    var resoult = JSON.parse(req.body.resoult);

    if(resoult.iswin)
    {
        db.query(updatePlayerStat(mail, resoult.mapid, resoult.iron, resoult.wood), 
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

    db.query(uploadResoult(mail, resoult.mapid, resoult.iswin, resoult.score, resoult.lostboxes, resoult.time, resoult.usedmultiplies), 
    (_) => console.log("[Resoult] New resoult added: " + mail + " | " + resoult.mapid))
})

function sendPlayeBack(mail, res)
{
    db.query(find(mail), (player) => res.send(player.rows[0]));
}

function updatePlayerStat(mail, mapid, iron, wood)
{
    return {    
        text: 'UPDATE users SET completed = $1::integer || completed, iron = iron + $2, wood = wood + $3 WHERE email = $4',
        values: [mapid, iron, wood, mail],
    }
}

function uploadResoult(mail, mapid, iswin, score, lostboxes, time, usedmultiplies)
{
    return {    
        text: 'INSERT INTO resoults (mapid, iswin, score, lostboxes, time, email, usedmultiplies) VALUES ($1, $2, $3, $4, $5, $6, $7)',
        values: [mapid, iswin, score, lostboxes, time, mail, usedmultiplies],
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