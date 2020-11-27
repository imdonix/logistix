const express = require('express')
const path = require('path')
const db = require('../db/database')
var router = express.Router()

router.get('/', (req,res) => res.sendFile(path.join(__dirname + '/../public/bug.html')))
router.get('/script.js', (req,res) => res.sendFile(path.join(__dirname + '/../public/bug.js')))

router.post('/', (req,res) =>
{
    const data = req.body;
    res.status(200).send();
    if(data.send)
        db.query(db.postBug(data.send, data.player, data.device, data.ram), (_) => console.log("[Bug] posted"))
})

module.exports = router