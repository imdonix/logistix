const express = require('express')
const path = require('path')
var router = express.Router()

router.get('/', (req,res) => res.sendFile(path.join(__dirname + '/../public/bug.html')))
router.get('/script.js', (req,res) => res.sendFile(path.join(__dirname + '/../public/bug.js')))

router.post('/', (req,res) =>
{
    res.status(200).send();
    console.log(req.body) //TODO

})

module.exports = router