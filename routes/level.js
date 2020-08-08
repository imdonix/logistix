const express = require('express')
var router = express.Router()

var Levels =
    [
        {
            id: 0,
            unlocks: [],
            name: "First Job",
            reward_wood: 50,
            reward_iron: 10,
            boxes: [0,0,0,0,0],
            maxlost: 0
        }
    ]

router.get('/', function (req, res) {
    res.json(Levels);
})

module.exports = router