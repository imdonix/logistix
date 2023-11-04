import { Router } from "express"
import { getLevelMapSQL, querySQL, updateLevelMapSQL } from "../db/database"
import { MASTER_PASSWORD } from "../settings"
import { cyrb53 } from "../crypto"

export const levelRouter = Router()

levelRouter.get('/', function (req, res) 
{
    console.log("[Level] requested")
    querySQL(getLevelMapSQL(), (r) => { 

        // TODO: Conversion may be needed
        res.send(JSON.parse(r[0]))
    })
})

levelRouter.post('/', function (req,res)
{
    let pass = req.body.password;
    let newlevels = req.body.data;


    if(pass === cyrb53(MASTER_PASSWORD))
    {
        console.log("[Level] levelmap updated")
        querySQL(updateLevelMapSQL(newlevels), (rows) =>
        {

            // TODO: Conversion may be needed
            if(rows.length == 1)
                res.status(200).send("Updated");
            else
                res.status(500).send("Something went wrong");
        })
    }
    else
        res.status(403).send("Bad password!");
})
