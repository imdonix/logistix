const express = require('express')
const db = require('../db/database')
var router = express.Router()

router.get('/:mapID', function (req, res) 
{
    var id = req.params.mapID;

    db.query(db.findToplist(id), (toplist) =>
    {
        console.log(`[Toplist] requested (${id}) [${toplist.rows.length}]`)
        res.send(toplist.rows);
    })
})

module.exports = router