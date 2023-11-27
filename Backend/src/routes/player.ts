import { Router } from "express";
import { createUser, findUser } from "../database";


export const playerRouter = Router()

playerRouter.post('/', async (req, res) =>
{
    const mail = req.body.email;

    const player = await findUser(mail)
    if(player)
    {
        console.log(`[Player] '${mail}' returned.`)
        res.send(player)
    }
    else
    {
        console.log(`[Player] '${mail}' inicialized.`);
        const fresh = await createUser(mail)
        res.send(fresh)
    }
})