const express = require('express')
const db = require('../db/database')
var router = express.Router()

router.post('/', (req, res) =>
{
    if(req.body.email)
    {
        var mail = req.body.email;
        //TODO
    }
    else
        res.status(401).send("You must give the email field.");  
})

module.exports = router