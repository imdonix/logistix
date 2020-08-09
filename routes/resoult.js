const express = require('express')
const db = require('../database')
var router = express.Router()


router.post('/', function (req, res) 
{
    var mail = req.body.email;
    var resoult = req.body.resoult;


    /*
        [JsonProperty("mapid")] public int Map;
        [JsonProperty("iswin")] public bool IsWin;
        [JsonProperty("score")] public int Score;
        [JsonProperty("lostboxes")] public int LostBoxes;
        [JsonProperty("time")] public int PlayTime;
        [JsonProperty("iron")] public int Iron;
        [JsonProperty("wood")] public int Wood;
    */
    if(resoult.iswin)
    {
        db.query(updatePlayerStat(mail, resoult.mapid, resoult.iron, resoult.wood), 
        (_) => console.log("[Resoult] Player updated: " + mail))
    }

    db.query(uploadResoult(mail, resoult), 
    (_) => console.log("[Resoult] New resoult added: " + mail + " | " + resoult.mapid))

    db.query(find(mail), (player) => res.send(player.rows[0]))    
})

function updatePlayerStat(mail, mapid, iron, wood)
{
    return {    
        text: 'UPDATE users SET completed = $1::integer || completed, iron = iron + $2, wood = wood + $3 WHERE email = $4',
        values: [mapid, iron, wood, mail],
    }
}

function uploadResoult(mail, resoult)
{
    return {    
        text: 'INSERT INTO resoults (mapid, iswin, score, lostboxes, time, email ) VALUES ($1, $2, $3, $4, $5, $6)',
        values: [resoult.mapid, resoult.iswin, resoult.score, resoult.lostboxes, resoult.time, mail],
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