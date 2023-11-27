import { Router } from "express"
import { fixBugSQL, getBugsSQL, postBugSQL } from "../database";
import { MASTER_PASSWORD } from "../settings";
import { cyrb53 } from "../crypto";

export const bugRouter = Router()

bugRouter.post('/', async (req,res) =>
{
    const data = req.body;
    if(data.send)
    {
        await postBugSQL(data.send, data.player, data.device, data.ram)
        console.log("[Bug] posted")
    }
    res.status(200).send();
})

bugRouter.post('/all', async (_,res) =>
{
    console.log("[Bug] requested all")
    res.status(200).send(await getBugsSQL());
})

bugRouter.post('/fix/:id', async (req,res) =>
{
    if(req.params.id)
    {
        if(req.body.pass)
        {
            if(req.body.pass === cyrb53(MASTER_PASSWORD))
            {
                await fixBugSQL(req.params.id)
                console.log("[Bug] bug fixed id")
                res.status(200).send('Succes')
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