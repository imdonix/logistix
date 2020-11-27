const express = require('express')
const settings = require('../settings')
const db = require('../db/database')
const { cyrb53 } = require('../crypto')
var router = express.Router()

router.get('/', function (req, res) 
{
    console.log("[Level] requested")
    db.query(db.getLevelMap(), (r) => res.send(r.rows[0].map.data))
})

router.post('/', function (req,res)
{
    let pass = req.body.password;
    let newlevels = req.body.data;
    let packed = {'data': newlevels}


    if(pass === cyrb53(settings.MASTER_PASSWORD))
    {
        console.log("[Level] levelmap updated")
        db.query(db.updateLevelMap(packed), (r) =>
        {
            if(r.rowCount == 1)
                res.status(200).send("Updated");
            else
                res.status(500).send("Something went wrong");
        })
    }
    else
        res.status(403).send("Bad password!");
})

module.exports = router