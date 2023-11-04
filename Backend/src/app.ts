import bodyParser from "body-parser"
import express, { json } from "express"
import { versionRouter } from "./routes/version"
import { levelRouter } from "./routes/level"
import { playerRouter } from "./routes/player"
import { nameRouter } from "./routes/name"
import { resultRouter } from "./routes/result"
import { toplistRouter } from "./routes/toplist"
import { inviteRouter } from "./routes/invite"
import { premiumRouter } from "./routes/premium"
import { bugRouter } from "./routes/bug"
import { PORT } from "./settings"

const app = express()
app.use(json())


app.use('/version', versionRouter)
app.use('/levels', levelRouter)
app.use('/player', playerRouter)
app.use('/name', nameRouter)
app.use('/resoult', resultRouter)
app.use('/toplist', toplistRouter)
app.use('/invite', inviteRouter);
app.use('/premium', premiumRouter);
app.use('/bug', bugRouter)
app.use(express.static('public', { extensions : ['html'] }))


app.listen(PORT, () => console.log(`Logistix API started. (http://localhost:${PORT}) `))