const express = require('express')
const db = require('../db/database')
const sha = require('sha1');
var router = express.Router()

const PLAYSTORE_URL = "https://play.google.com/store/apps/details?id=com.donix.logistix";

router.get('/:name', function (req, res) 
{
    var name = req.params.name;

    db.query(addRefer(name, hashIP(req)), 
    (_) => console.log(`[Invite] ${name}'s link opened by ${req.ip}.`))

    res.redirect(PLAYSTORE_URL)
})

router.get('/', (_, res) => res.redirect(PLAYSTORE_URL));

router.post('/', function (req, res) 
{
    var mail = req.body.email;

    
    //TODO: return unique invites

})

function hashIP(req)
{
    return sha(req.ip);
}

function addRefer(name, ip)
{
    return {    
        text: 'INSERT INTO invites (inviter, iphash, date) VALUES ($1, $2, $3)',
        values: [name, ip, new Date()]
    }
}

module.exports = router