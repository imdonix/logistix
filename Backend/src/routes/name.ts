import { Router } from "express"
import { querySQL, renameSQL } from "../db/database"

export const nameRouter = Router()


nameRouter.post('/', function (req, res) 
{
    var name = req.body.name
    var email = req.body.email

    querySQL(renameSQL(email,name), 
    (rows) =>
    { 
        console.log("[Name] change: " + email + " -> " + name)

        // TODO: Conversion may be needed
        res.send(rows[0]) 
    }
    )

})
