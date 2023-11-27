import { Router } from "express";
import { toplist } from "../database";

export const toplistRouter = Router()

toplistRouter.get('/:mapID', async function (req, res) 
{
    const mapid = req.params.mapID;

    console.log(`[Toplist] requested (${mapid})`)
    const list = await toplist(mapid)
    res.send(list)
    
})