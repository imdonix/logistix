import { Router } from "express";
import { INVITE_DESC, INVITE_TITLE, PLAYSTORE_IMG, PLAYSTORE_URL, PREMIUM_UNLOCK } from "../settings";
import { addReferSQL, countPlayerInvitesSQL, querySQL } from "../database";

export const inviteRouter = Router()

inviteRouter.get('/:name', function (req, res) 
{
    if(checkFacebook(req))
    {
        validateFacebook(res);
        return;
    }

    var name = req.params.name;

    querySQL(addReferSQL(name, req.ip), 
    (_) => console.log(`[Invite] ${name}'s link opened by ${req.ip}.`))

    res.redirect(PLAYSTORE_URL)
})

inviteRouter.get('/', (req, res) =>
{
    if(checkFacebook(req))
    {
        validateFacebook(res);
        return;
    }

    res.redirect(PLAYSTORE_URL)
} );

inviteRouter.post('/', function (req, res) 
{
    if(req.body.email)
    {
        console.log(`[Invite] ${req.body.email} requested the invite status.`)
        querySQL(countPlayerInvitesSQL(req.body.email), 
        (data) => 
        {
            let n = 0;

            // TODO: Conversion may be needed
            if(data.length > 0)
                n = parseInt(data[0].count);

            res.send(ceateResoult(n));
        })
    }
    else
        res.status(401).send("You must give the email field.");
})

function ceateResoult(c : number)
{
    return {
        count: c.toString(),
        reward: PREMIUM_UNLOCK
    }
}

function checkFacebook(req : any)
{
    return req.header('User-Agent').indexOf('facebookexternalhit') >= 0;
}

function validateFacebook(res : any)
{
    res.send
    (`
        <meta property="og:url"                content="${PLAYSTORE_URL}" />
        <meta property="og:title"              content="${INVITE_TITLE}" />
        <meta property="og:description"        content="${INVITE_DESC}" />
        <meta property="og:image"              content="${PLAYSTORE_IMG}" />
    `);
}
