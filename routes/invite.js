const express = require('express')
const db = require('../db/database')
const settings = require('../settings')
const sha = require('sha1');
var router = express.Router()

router.get('/:name', function (req, res) 
{
    if(checkFacebook(req))
    {
        validateFacebook(res);
        return;
    }

    var name = req.params.name;

    db.query(db.addRefer(name, hashIP(req)), 
    (_) => console.log(`[Invite] ${name}'s link opened by ${req.ip}.`))

    res.redirect(PLAYSTORE_URL)
})

router.get('/', (_, res) =>
{
    if(checkFacebook(req))
    {
        validateFacebook(res);
        return;
    }

    res.redirect(PLAYSTORE_URL)
} );

router.post('', function (req, res) 
{
    if(req.body.email)
    {
        console.log(`[Invite] ${req.body.email} requested the invite status.`)
        db.query(db.countPlayerInvites(req.body.email), 
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


function ceateResoult(c)
{
    return {
        count: c,
        reward: settings.PREMIUM_UNLOCK
    }
}

function checkFacebook(req)
{
    return req.header('User-Agent').indexOf('facebookexternalhit') >= 0;
}

function validateFacebook(res)
{
    res.send
    (`
        <meta property="og:url"                content="${settings.PLAYSTORE_URL}" />
        <meta property="og:title"              content="${settings.INVITE_TITLE}" />
        <meta property="og:description"        content="${settings.INVITE_DESC}" />
        <meta property="og:image"              content="${settings.PLAYSTORE_IMG}" />
    `);
}

module.exports = router