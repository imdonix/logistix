const express = require('express')
const bodyParser= require('body-parser')
const path = require('path')
const settings = require('./settings')

const level = require('./routes/level')
const version = require('./routes/version')
const player = require('./routes/palyer')
const name = require('./routes/name')
const resoult = require('./routes/resoult')
const toplist = require('./routes/toplist')
const invite = require('./routes/invite')
const premium = require('./routes/premium')
const editor = require('./routes/editor')
const bug = require('./routes/bug')

const app = express()
app.use(bodyParser.json())

app.use('/version', version)
app.use('/levels', level)
app.use('/player', player)
app.use('/name', name)
app.use('/resoult', resoult)
app.use('/toplist', toplist)
app.use('/invite', invite);
app.use('/premium', premium);
app.use('/editor', editor)
app.use('/bug', bug)
app.get('/privacy', (_, res) => res.sendFile(path.join(__dirname + '/public/privacy.html')))

app.listen(settings.PORT, () => console.log(`API running on (${settings.PORT})`))