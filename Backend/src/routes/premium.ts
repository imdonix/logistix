import { Router } from "express";
import { countPlayerInvitesSQL, findSQL, makePremiumSQL, querySQL } from "../db/database";
import { PREMIUM_UNLOCK } from "../settings";

export const premiumRouter = Router()

premiumRouter.post('/', (req, res) =>
{
    if(req.body.email)
    {
        var mail = req.body.email;
        console.log(`[Premium] ${req.body.email} requested the invite status.`)

        querySQL(findSQL(mail), (rows) =>
        {
            // TODO: Conversion may be needed
            if(rows.length > 0)
            {
                querySQL(countPlayerInvitesSQL(mail), 
                (rows) => 
                {
                    // TODO: Conversion may be needed
                    if(rows.length > 0 && rows[0].count >= PREMIUM_UNLOCK)
                    {
                        // TODO: Conversion may be needed
                        rows[0].premium = true;

                        querySQL(makePremiumSQL(mail), (rows) =>
                        {
                            // TODO: Conversion may be needed
                            if(rows.length > 0)
                            {
                                res.send(rows[0])
                            }
                            else
                            {
                                res.status(501).send("Cannot find player to update with this email. (step 2)")
                            }
                        })
                    }
                    else
                    {
                        // TODO: Conversion may be needed
                        res.send(rows[0])
                    }
                        
                })
            }
            else
            {
                res.status(401).send("Cannot find player to update with this email. (step 1)")
            }
        })
    }
    else
    {
        res.status(401).send("You must give the email field.");  
    }
})