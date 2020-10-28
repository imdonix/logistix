const express = require('express')
const db = require('../db/database')
var router = express.Router()

router.get('/:mapID', function (req, res) 
{
    var id = req.params.mapID;

    db.query(find(id), (toplist) =>
    {
        console.log(`[Toplist] requested (${id}) [${toplist.rows.length}]`)
        res.send(toplist.rows);
    })
})

function find(id)
{
    return {
        text: 'SELECT name, premium, MAX(score) as score FROM resoults, users WHERE users.email = resoults.email AND mapid = $1 AND iswin GROUP BY name, premium ORDER BY MAX(score) DESC',
        values: [id],
    }
}

module.exports = router