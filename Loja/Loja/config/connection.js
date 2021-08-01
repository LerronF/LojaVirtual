const mysql = require('mysql');
const connection = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: 'devtime',
    port: 3306,
    database: 'dbapi'
});

connection.connect((err) =>{
    if(err){
        console.log('erro: ' + err)
    }
    else{
        console.log('banco de dados conectado !')
    }
});

module.exports = connection;