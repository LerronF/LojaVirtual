using LojaVirtual.Web.Data;
using LojaVirtual.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaVirtual.Web.Controllers
{
    public class ItensController : Controller
    {
        private LojaVirtualContext db = new LojaVirtualContext();
        public ActionResult ListarItens(int id = 0)
        {
            var lista = db.VendasItens.Where(x => x.Pedido == id);
            ViewBag.Pedido = id;
            return PartialView(lista);
        }

        public ActionResult Create(VendasItens vendas)
        {
            string _connStr = "Data Source=ITRIAD00307;Initial Catalog=BDTransire;Integrated Security=True";

            int codigoV = 0;
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"select max(Id) as ID from Vendas";
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        codigoV = (int)reader["Id"];
                    }

                    conn.Close();
                }
            }

            using (SqlConnection connx = new SqlConnection(_connStr))
            {
                using (SqlCommand cmdx = new SqlCommand())
                {
                    cmdx.Connection = connx;
                    cmdx.CommandText = @"insert into VendasItens (Pedido,Produto,ValUnit,QuantProd) " +
                               "values("+ codigoV + "," +vendas.Produto+","+ vendas.ValUnit + ","+ vendas.QuantProd + ")";                   

                    try
                    {
                        connx.Open();
                        cmdx.ExecuteNonQuery();
                        connx.Close();
                    }
                    catch (SqlException ex)
                    {
                        connx.Close();
                    }
                }
            }

            //var item = new VendasItens()
            //{
            //    QuantProd = vendas.QuantProd
            //,
            //    Produto = vendas.Produto
            //,
            //    ValUnit = vendas.ValUnit
            //,
            //    Id = vendas.Id
            //,
            //    Pedido = db.Vendas.Find(vendas.Pedido).ID
            //};

            //db.VendasItens.Add(item);

            //db.SaveChanges();

            return Json(new { Resultado = codigoV }, JsonRequestBehavior.AllowGet);
        }
    }
}