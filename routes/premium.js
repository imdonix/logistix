const express = require('express')
const db = require('../db/database')
const settings = require('../settings')
var router = express.Router()

router.post('/', (req, res) =>
{
    if(req.body.email)
    {
        var mail = req.body.email;
        console.log(`[Premium] ${req.body.email} requested the invite status.`)

        db.query(db.find(mail), (player) =>
        {
            if(player.rows.length > 0)
            {
                db.query(db.countPlayerInvites(mail), 
                (data) => 
                {
                    if(data.rows.length > 0 && data.rows[0].count >= settings.PREMIUM_UNLOCK)
                    {
                        player.rows[0].premium = true;
                        db.query(db.makePremium(mail), (data) =>
                        {
                            if(data.rowCount > 0)
                                res.send(player.rows[0])
                            else
                                res.status(501).send("Cannot find player to update with this email. (step 2)")
                        })
                    }
                    else
                        res.send(player.rows[0])
                })
            }
            else
                res.status(401).send("Cannot find player to update with this email. (step 1)")
        })
    }
    else
        res.status(401).send("You must give the email field.");  
})


module.exports = router