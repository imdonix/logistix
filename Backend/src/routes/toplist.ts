import { Router } from "express";
import { findToplistSQL, querySQL } from "../db/database";

export const toplistRouter = Router()

toplistRouter.get('/:mapID', function (req, res) 
{
    var id = req.params.mapID;

    querySQL(findToplistSQL(id), (toplist) =>
    {
        console.log(`[Toplist] requested (${id}) [${toplist.length}]`)
        
        // TODO: Conversion may be needed
        res.send(toplist)
    })
})