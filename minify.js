const fs = require('fs')
const path = require('path')

module.exports = 
{
    getScript: (name) =>
    {
        return  fs.existsSync( __dirname + `/public/${name}.min.js`) ? 
                path.join(__dirname + `/public/${name}.min.js`) : 
                path.join(__dirname + `/public/${name}.js`)
    }
}

