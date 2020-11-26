const express = require('express')
const path = require('path')
var router = express.Router()

router.get('/', (req,res) => res.sendFile(path.join(__dirname + '/../public/bug.html')))
router.get('/script.js', (req,res) => res.sendFile(path.join(__dirname + '/../public/bug.js')))

module.exports = router