const express = require('express')
var router = express.Router()

var Levels =
[
    {
        color: "red",
        levels:
        [
            {
                id: 0,
                unlocks: [],
                name: "First Job",
                reward_wood: 50,
                reward_iron: 10,
                boxes: [0,0,0,0,0],
                maxlost: 0
            },
            {
                id: 1,
                unlocks: [],
                name: "Sec Job",
                reward_wood: 50,
                reward_iron: 50,
                boxes: [0,0,1,1,0,0],
                maxlost: 1
            }
        ]

    },
    {
        color: "green",
        levels:
        [
            {
                id: 2,
                unlocks: [0],
                name: "third Job",
                reward_wood: 520,
                reward_iron: 80,
                boxes: [0,0,2,1,1],
                maxlost: 0
            },
            {
                id: 3,
                unlocks: [1],
                name: "fourth Job",
                reward_wood: 50,
                reward_iron: 600,
                boxes: [1,1,1,1,1,1],
                maxlost: 1
            },
            {
                id: 4,
                unlocks: [1],
                name: "fifth Job",
                reward_wood: 510,
                reward_iron: 100,
                boxes: [2,2,2,2,2,2,2,2,2,2,2],
                maxlost: 3
            }
        ]
    },
    {
        color: "red",
        levels:
        [
            {
                id: 5,
                unlocks: [4],
                name: "fifth Job",
                reward_wood: 510,
                reward_iron: 100,
                boxes: [1,2,1,2,1,2,1,2,1,2,1],
                maxlost: 3
            }
        ]
    },
    {
        color: "red",
        levels:
        [
            {
                id: 6,
                unlocks: [5],
                name: "fifth Job",
                reward_wood: 510,
                reward_iron: 100,
                boxes: [1,1,1,0,0,0,2,2,1,0,2],
                maxlost: 3
            }
        ]
    },
    {
        color: "red",
        levels:
        [
            {
                id: 7,
                unlocks: [6],
                name: "fifth Job",
                reward_wood: 510,
                reward_iron: 100,
                boxes: [1,1,1,0,0,0,2,2,1,0,2],
                maxlost: 3
            },
            {
                id: 8,
                unlocks: [6],
                name: "fifth Job",
                reward_wood: 510,
                reward_iron: 100,
                boxes: [1,1,1,1,1,1,1,1,1,1,2],
                maxlost: 3
            },
            {
                id: 9,
                unlocks: [6],
                name: "fifth Job",
                reward_wood: 510,
                reward_iron: 100,
                boxes: [1,1,1,0,0,0,2,2,1,0,2],
                maxlost: 3
            },
            {
                id: 10,
                unlocks: [6],
                name: "fifth Job",
                reward_wood: 510,
                reward_iron: 100,
                boxes: [1,2,2,2,2,2,2,2,1,0,2],
                maxlost: 3
            }
        ]
    },
    {
        color: "red",
        levels:
        [
            {
                id: 11,
                unlocks: [7,8,9,10],
                name: "fifth Job",
                reward_wood: 510,
                reward_iron: 100,
                boxes: [1,1,1,0,0,0,2,2,1,0,2,1],
                maxlost: 3
            },
            {
                id: 12,
                unlocks: [7,8,9,10],
                name: "blabla Job",
                reward_wood: 999,
                reward_iron: 999,
                boxes: [3,3,3,4,4,4,5,5,5],
                maxlost: 2
            }
        ]
    }
]

router.get('/', function (req, res) 
{
    console.log("[Level] requested")
    res.json(Levels);
})

module.exports = router