const PORT = process.env.PORT || 3000;

const express = require('express')
const bodyParser= require('body-parser')
const path = require('path')

const level = require('./routes/level')
const version = require('./routes/version')
const player = require('./routes/palyer')

const app = express();
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

app.use('/version', version)
app.use('/levels', level)
app.use('/player', player)

app.listen(PORT, () => console.log("Server started. " + PORT))