import { Router } from "express";
import { findUser, updateUserStat, uploadResult } from "../database";

export const resultRouter = Router()


resultRouter.post('/', async function (req, res) 
{
    const mail = req.body.email;
    const result = JSON.parse(req.body.resoult);

    if(result.iswin)
    {
        await updateUserStat(mail, result.mapid, result.iron, result.wood), 
        console.log(`[Resoult] '${mail}' finished -> ${result.mapid} (${result.iron}|${result.wood})`)
        res.send(await findUser(mail))
    }
    else
    {
        console.log(`[Resoult] '${mail}' failed -> ${result.mapid}`)
        res.send(await findUser(mail))
    }

    await uploadResult(mail, result.mapid, result.iswin, result.score, result.lostboxes, result.time, result.usedmultiplies)
})