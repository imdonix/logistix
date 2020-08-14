const { Pool } = require('pg')
const CONNECTION_STRING = "postgres://mkmqubdsjwslth:6bdab8dc7a89cba14b89003536347c4b2393b776b5916f5f160beb1f418f9297@ec2-54-246-89-234.eu-west-1.compute.amazonaws.com:5432/deh00d9545rhu3";

const pool = new Pool
(
    {
        connectionString: 
        process.env.DATABASE_URL || CONNECTION_STRING,
        ssl: { rejectUnauthorized: false }
    }
)

module.exports = 
{
    query: (query, callback) => 
    {
        const start = Date.now()
        return pool.query(query, (err, res) => 
        {
          if(err) throw err;
          callback(res)
        })
      }
}