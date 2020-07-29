const PORT = process.env.PORT || 3000;
const DATABASE = "mongodb://root:1Q2w3e4r5t@ds257648.mlab.com:57648/heroku_pv6vd55j"

const VERSION = { "api" : "1.0", "client" : "1.0" }

const LEVELSAMPLE = 
[
    {
        id: 0,
        unlocks: [],
        name: "First Job",
        des: "The dock need your help.",
        reward_wood: 50,
        reward_iron: 10,
        boxes: [0,0,0,0,0],
        maxlost: 0
    },
    {
        id: 1,
        unlocks: [0],
        name: "Iron",
        des: "sample",
        reward_wood: 0,
        reward_iron: 50,
        boxes: [1,1,1],
        maxlost: 0
    },
    {
        id: 2,
        unlocks: [0],
        name: "Food",
        des: "sample",
        reward_wood: 100,
        reward_iron: 0,
        boxes: [2,2,2],
        maxlost: 0
    },
    {
        id: 3,
        unlocks: [1,2],
        name: "Mixed",
        des: "sample",
        reward_wood: 500,
        reward_iron: 500,
        boxes: [1,1,1,0,1,2,1,0],
        maxlost: 2
    },

]

const express = require('express');
const bodyParser= require('body-parser');
const path = require('path');
const mongodb = require('mongodb');

const client = mongodb.MongoClient;
const app = express();

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());


app.get('/version', (req, res) => {res.send(VERSION)})
app.get('/levels', (req, res) => {res.send(LEVELSAMPLE)})

app.listen(PORT, () => console.log("Server started. " + PORT))