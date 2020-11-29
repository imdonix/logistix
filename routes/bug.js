const express = require('express')
const path = require('path')
const { getScript } = require('../minify')
const db = require('../db/database')
const settings = require('../settings')
const { cyrb53 } = require('../crypto')
var router = express.Router()

router.get('/', (req,res) => res.sendFile(path.join(__dirname + '/../public/bug.html')))
router.get('/script.js', (req,res) => res.sendFile(getScript("bug")))

router.post('/', (req,res) =>
{
    const data = req.body;
    res.status(200).send();
    if(data.send)
        db.query(db.postBug(data.send, data.player, data.device, data.ram), (_) => console.log("[Bug] posted"))
})

router.post('/all', (_,res) =>
{
    console.log("[Bug] requested all")
    db.query(db.getBugs(), (data) =>
    {
        res.status(200).send(data.rows);
    })
})

router.post('/fix/:id', (req,res) =>
{
    if(req.params.id)
    {
        if(req.body.pass )
        {
            if(req.body.pass === cyrb53(settings.MASTER_PASSWORD))
            {
                db.query(db.fixBug(req.params.id), (_) =>
                {
                    console.log("[Bug] bug fixed id")
                    res.send(200).send('Succes')
                })
            }
            else
                res.status(401).send('Bad master password.')
        }
        else
            res.status(401).send('You need to set the master key.')
    }
    else
        res.status(400).send('You need id.')

})

module.exports = router