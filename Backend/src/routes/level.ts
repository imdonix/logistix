import { Router } from "express"
import { readFile, writeFile } from "fs/promises"
import { MASTER_PASSWORD } from "../settings"
import { cyrb53 } from "../crypto"

export const levelRouter = Router()
const FILE_NAME = 'levels.json'

levelRouter.get('/', async function (req, res) 
{
    console.log("[Level] Map requested")

    const file = await readFile(FILE_NAME)
    let levels = JSON.parse(file.toString())
    res.send(levels)

})

levelRouter.post('/', async function (req,res)
{
    let pass = req.body.password;
    let newlevels = req.body.data;


    if(pass === cyrb53(MASTER_PASSWORD))
    {
        console.log("[Level] Map updated [ADMIN]")
        await writeFile(FILE_NAME, JSON.stringify(newlevels));
    }
    else
        res.status(403).send("Bad password!");
})
