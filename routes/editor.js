const express = require('express')
const path = require('path')
const fs = require('fs')
var router = express.Router()
const script = "editor"

router.get('/', (req,res) => res.sendFile(path.join(__dirname + '/../public/editor.html')))
router.get('/script.js', (req,res) =>
fs.existsSync(__dirname + `/../public/${script}.min.js`) ? res.sendFile(path.join(__dirname + `/../public/${script}.min.js`)) : res.sendFile(path.join(__dirname + `/../public/${script}.js`)))

module.exports = router