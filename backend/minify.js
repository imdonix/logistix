const { equal } = require('assert')
const fs = require('fs')
const path = require('path')
const compressor = require('node-minify')

module.exports = 
{
    getScript: (name) =>
    {
        return  fs.existsSync( __dirname + `/public/${name}.min.js`) ? 
                path.join(__dirname + `/public/${name}.min.js`) : 
                path.join(__dirname + `/public/${name}.js`)
    },

    build: (name) =>
    {
        compressor.minify({
            compressor: 'gcc',
            input: `./public/${name}.js`,
            output: `./public/${name}.min.js`,
            callback: function(err, min) 
            { console.log(`[BUILD] ${name}.js minified`) }
          });
    }
}

