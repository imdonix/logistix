const PORT = process.env.PORT || 3000;

const express = require('express')
const bodyParser= require('body-parser')
const path = require('path')

const level = require('./routes/level')
const version = require('./routes/version')
const player = require('./routes/palyer')
const name = require('./routes/name')
const resoult = require('./routes/resoult')

const app = express();
app.use(bodyParser.json());

app.use('/version', version)
app.use('/levels', level)
app.use('/player', player)
app.use('/name', name)
app.use('/resoult', resoult)

app.listen(PORT, () => console.log("Server started. " + PORT))