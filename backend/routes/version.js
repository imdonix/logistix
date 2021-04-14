const express = require('express')
const settings = require('../settings')
var router = express.Router()

router.get('/', function (req, res) 
{
    console.log("[Version] requested")
    res.json(settings.VERSION);
})

module.exports = router