const express = require('express')
var router = express.Router()

var Levels =
[
    {
        color: "green",
        levels:
        [
            {
                id: -1,
                unlocks: [],
                name: "TEST",
                reward_wood: 1,
                reward_iron: 1,
                boxes: [13,13,13,13,13],
                maxlost: 2
            }
        ]
    },

    {
        color: "green",
        levels:
        [
            {
                id: 0,
                unlocks: [],
                name: "A fresh start",
                reward_wood: 50,
                reward_iron: 20,
                boxes: [0, 0, 0, 0, 0, 0],
                maxlost: 1
            }
        ]
    },
    
    {
        color: "yellow",
        levels:
        [
            {
                id: 1,
                unlocks: [0],
                name: "Things start to work",
                reward_wood: 50,
                reward_iron: 25,
                boxes: [7,7,8,8,9,9,10,10,11,11,11,11],
                maxlost: 4
            },
            {
                id: 2,
                unlocks: [0],
                name: "Heavy supplies",
                reward_wood: 10,
                reward_iron: 40,
                boxes: [1, 1, 1, 1, 1, 1, 1],
                maxlost: 1
            },
        ]
    },
    
    {
        color: "orange",
        levels:
        [
            {
                id: 3,
                unlocks: [1],
                name: "Bouncy",
                reward_wood: 60,
                reward_iron: 30,
                boxes: [0, 0, 0, 0, 0, 0, 3, 3, 3, 3],
                maxlost: 1
            },
            {
                id: 4,
                unlocks: [2],
                name: "Important supplements",
                reward_wood: 70,
                reward_iron: 10,
                boxes: [0, 0, 1, 1, 1, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0],
                maxlost: 2
            },
        ]
    },

    {
        color: "red",
        levels:
        [
            {
                id: 5,
                unlocks: [3, 4],
                name: "Small, but expensive",
                reward_wood: 60,
                reward_iron: 40,
                boxes: [3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2],
                maxlost: 0
            },
        ]
    },

    {
        color: "blue",
        levels:
        [
            {
                id: 6,
                unlocks: [5],
                name: "Tower",
                reward_wood: 50,
                reward_iron: 25,
                boxes: [3, 3, 5, 5, 3, 3, 5, 5],
                maxlost: 0
            },
            {
                id: 7,
                unlocks: [5],
                name: "For constuction... and a few tires",
                reward_wood: 60,
                reward_iron: 70,
                boxes: [3, 3, 5, 1, 5, 1],
                maxlost: 1
            },
        ]
    },

    {
        color: "purple",
        levels:
        [
            {
                id: 8,
                unlocks: [6],
                name: "Car Enterprise",
                reward_wood: 100,
                reward_iron: 100,
                boxes: [3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
                maxlost: 3
            },
            {
                id: 9,
                unlocks: [7],
                name: "Big Deal",
                reward_wood: 100,
                reward_iron: 100,
                boxes: [0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5],
                maxlost: 1
            },
        ]
    }
]

router.get('/', function (req, res) 
{
    console.log("[Level] requested")
    res.json(Levels);
})

module.exports = router