import { Router } from "express";
import { constructSQL, findSQL, querySQL } from "../db/database";


export const playerRouter = Router()

playerRouter.post('/', (req, res) =>
{
    var mail = req.body.email;

    querySQL(findSQL(mail), (rows) =>
    {

        // TODO: Conversion may be needed
        if(rows.length > 0)
        {
            console.log("[Player] requested: " + mail);

            // TODO: Conversion may be needed
            res.send(rows[0]);
        }
        else
        {
            console.log("[Player] New inicialized: " + mail);


            querySQL(constructSQL(mail), (rows) => 
            {
                // TODO: Conversion may be needed
                res.send(rows[0]);
            });
        }
    });
})