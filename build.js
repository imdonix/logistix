var compressor = require('node-minify');
 
compressor.minify({
  compressor: 'gcc',
  input: './public/bug.js',
  output: './public/bug.min.js',
  callback: function(err, min) 
  { console.log("[BUILD] bug.js minified") }
});

compressor.minify({
    compressor: 'gcc',
    input: './public/editor.js',
    output: './public/editor.min.js',
    callback: function(err, min) 
    { console.log("[BUILD] editor.js minified") }
  });