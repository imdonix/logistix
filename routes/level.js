const express = require('express')
const settings = require('../settings')
const db = require('../db/database')
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

function cyrb53(str) 
{
    let h1 = 0xdeadbeef ^ 14, h2 = 0x41c6ce57 ^ 14;
    for (let i = 0, ch; i < str.length; i++) 
    {
        ch = str.charCodeAt(i);
        h1 = Math.imul(h1 ^ ch, 2654435761);
        h2 = Math.imul(h2 ^ ch, 1597334677);
    }
    h1 = Math.imul(h1 ^ (h1>>>16), 2246822507) ^ Math.imul(h2 ^ (h2>>>13), 3266489909);
    h2 = Math.imul(h2 ^ (h2>>>16), 2246822507) ^ Math.imul(h1 ^ (h1>>>13), 3266489909);
    return 4294967296 * (2097151 & h2) + (h1>>>0);
};

module.exports = router