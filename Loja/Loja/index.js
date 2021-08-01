require('./config/connection');

const express = require('express');
const port =(process.env.port || 3000);

//express
const app =express();

app.use(express.json());

//config
app.set('port',port)

//rota
app.use('/api',require('./rotas'))

//iniciar express
app.listen(app.get('port'),(error)=>{
    if(error){
        console.log(' erro ao iniciar o servidor: ' + error)
    }
    else{
        console.log('Servidor iniciado na porta: ' + port)
    }
});