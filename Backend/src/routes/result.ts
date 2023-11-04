import { Router } from "express";
import { findSQL, querySQL, updatePlayerStatSQL, uploadResultSQL } from "../db/database";

export const resultRouter = Router()


resultRouter.post('/', function (req, res) 
{
    var mail = req.body.email;
    var resoult = JSON.parse(req.body.resoult);

    if(resoult.iswin)
    {
        querySQL(updatePlayerStatSQL(mail, resoult.mapid, resoult.iron, resoult.wood), 
        (rows) => 
        {
            console.log("[Resoult] Player updated: " + mail)
            sendPlayeBack(mail, res)
        })
    }
    else
    {
        sendPlayeBack(mail, res)
    }

    querySQL(uploadResultSQL(mail, resoult.mapid, resoult.iswin, resoult.score, resoult.lostboxes, resoult.time, resoult.usedmultiplies), 
    (rows) => console.log("[Resoult] New resoult added: " + mail + " | " + resoult.mapid))
})


function sendPlayeBack(mail : string, res : any)
{
    querySQL(findSQL(mail), (rows) => {

        // TODO: Conversion may be needed
        res.send(rows[0])
    })
}
