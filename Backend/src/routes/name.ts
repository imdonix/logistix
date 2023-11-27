import { Router } from "express"
import { renameUser } from "../database"

export const nameRouter = Router()


nameRouter.post('/', async function (req, res) 
{
    const name = req.body.name
    const mail = req.body.email

    const updated = await renameUser(mail, name) 
    console.log(`[Player] '${mail}' given name '${name}'`)
    res.send(updated) 
})
