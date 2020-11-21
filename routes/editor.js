const express = require('express')
const path = require('path')
var router = express.Router()

router.get('/', (req,res) => res.sendFile(path.join(__dirname + '/../public/editor.html')))
router.get('/script.js', (req,res) => res.sendFile(path.join(__dirname + '/../public/script.js')))

module.exports = router