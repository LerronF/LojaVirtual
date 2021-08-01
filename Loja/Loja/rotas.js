const rotas = require('express').Router()
const { Router } = require('express');
const connection = require('./config/connection')


//rotas
//get equipe
rotas.get('/',(req,res)=>{
    let sql = 'select * from tb_equipe'
    connection.query(sql,(err,rows,fields)=>{
        if(err) throw err;
        else{
            res.json(rows)
        }
    })
})

//get equipe codigo
rotas.get('/:id',(req,res)=>{
    const {id} = req.params
    let sql = 'select * from tb_equipe where id = ?'
    connection.query(sql,[id],(err,rows,fields)=>{
        if(err) throw err;
        else{
            res.json(rows)
        }
    })
})

//incluir equipe
rotas.post('/',(req,res)=>{
    const{descricao} = req.body

    let sql = `insert into tb_equipe (descricao) values('${descricao}')`
    connection.query(sql,(err,rows,fields)=>{
        if(err) throw err
        else{
            res.json({status: 'equipe inserida'})
        }
    })
})

//Editar equipe
rotas.put('/:id',(req,res)=>{
    const{id} = req.params
    const{descricao} = req.body

    let sql = `update tb_equipe set
                descricao = '${descricao}'
                where id = '${id}'`


    connection.query(sql,(err,rows,fields)=>{
        if(err) throw err
        else{
            res.json({status: 'Equipe modificada'})
        }
    })
})

//deletar equipe
rotas.delete('/:id',(req,res)=>{
    const{id} = req.params

    let sql = `delete from tb_equipe where id = '${id}'`
    connection.query(sql,(err,rows,fields)=>{
        if(err) throw err
        else{
            res.json({status: 'Equipe deletada'})
        }
    })
})


module.exports=rotas;