import { Router } from "express"
import { fixBugSQL, getBugsSQL, postBugSQL, querySQL } from "../db/database";
import { MASTER_PASSWORD } from "../settings";
import { cyrb53 } from "../crypto";

export const bugRouter = Router()

bugRouter.post('/', (req,res) =>
{
    const data = req.body;
    res.status(200).send();
    if(data.send)
        querySQL(postBugSQL(data.send, data.player, data.device, data.ram), (_) => console.log("[Bug] posted"))
})

bugRouter.post('/all', (_,res) =>
{
    console.log("[Bug] requested all")
    querySQL(getBugsSQL(), (data) =>
    {
        // TODO: Conversion may be needed
        res.status(200).send(data);
    })
})

bugRouter.post('/fix/:id', (req,res) =>
{
    if(req.params.id)
    {
        if(req.body.pass)
        {
            if(req.body.pass === cyrb53(MASTER_PASSWORD))
            {
                querySQL(fixBugSQL(req.params.id), (_) =>
                {
                    console.log("[Bug] bug fixed id")
                    res.status(200).send('Succes')
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