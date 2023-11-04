import { Router } from "express";
import { VERSION } from "../settings";

export const versionRouter = Router()

versionRouter.get('/', (req, res) =>
{
    console.log("[Version] requested")
    res.json(VERSION);
})