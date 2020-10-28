const express = require('express')
const db = require('../db/database')
const sha = require('sha1');
var router = express.Router()

const PLAYSTORE_URL = "https://play.google.com/store/apps/details?id=com.donix.logistix";
const PREMIUM_UNLOCK = 200;

router.get('/:name', function (req, res) 
{
    var name = req.params.name;

    db.query(addRefer(name, hashIP(req)), 
    (_) => console.log(`[Invite] ${name}'s link opened by ${req.ip}.`))

    res.redirect(PLAYSTORE_URL)
})

router.get('/', (_, res) => res.redirect(PLAYSTORE_URL));

router.post('', function (req, res) 
{

    if(req.body.email)
    {
        console.log(`[Invite] ${req.body.email} requested the invite status.`)
        db.query(countPlayerInvites(req.body.email), 
        (data) => 
        {
            let n = 0;

            if(data.rows.length > 0)
                n = parseInt(data.rows[0].count);

            res.send(ceateResoult(n));
        })
    }
    else
        res.status(401).send("You must give the email field.");
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

function ceateResoult(c)
{
    return {
        count: c,
        reward: PREMIUM_UNLOCK
    }
}

function countPlayerInvites(mail)
{
    return {    
        text: 'SELECT count(DISTINCT iphash) from invites, users WHERE email = $1 AND inviter = name GROUP BY inviter',
        values: [mail]
    }
}

module.exports = router