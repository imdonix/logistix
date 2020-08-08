const express = require('express')
var router = express.Router()

const VERSION = { "api" : "1.0", "client" : "1.0" }

router.get('/', function (req, res) {
    res.json(VERSION);
})

module.exports = router