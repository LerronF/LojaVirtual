using LojaVirtual.Web.Data;
using LojaVirtual.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LojaVirtual.Web.Controllers
{
    public class VendasController : Controller
    {
        private LojaVirtualContext db = new LojaVirtualContext();

        //public void abrirconexao()
        //{
        //    string strConn = "Data Source=ITRIAD00307;Initial Catalog=BDTransire;Integrated Security=True";
        //    connection = new SqlConnection(strConn);
        //    connection.Open();
        //}
        // GET: Vendas
        public ActionResult Index()
        {
            //var list = db.Vendas.ToList();
            string _connStr = "Data Source=ITRIAD00307;Initial Catalog=BDTransire;Integrated Security=True";

            List<Vendas> lista = new List<Vendas>();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT V.Id, V.Data,V.Cliente, Sum(VI.ValUnit) as Total " +
                                       "FROM Vendas V " +
                                       "inner join VendasItens VI ON V.Id = VI.Pedido " +
                                       "Group by V.Id, V.Data,V.Cliente";
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Vendas venda = new Vendas();
                        venda.ID = (int)reader["Id"];
                        //venda.CodCli = (int)reader["CodCli"];
                        venda.Cliente = (string)reader["Cliente"];
                        venda.Data = (DateTime)reader["Data"];
                        venda.Total = (decimal)reader["Total"];


                        lista.Add(venda);
                    }
                    conn.Close();
                }
            }
            return View(lista);
        }

        // GET: Vendas/Details
        public ActionResult Details(int? id)
        {
            string _connStr = "Data Source=ITRIAD00307;Initial Catalog=BDTransire;Integrated Security=True";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Vendas vendas = db.Vendas.Find(id);

            List<Vendas> lista = new List<Vendas>();

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT V.Id, V.Data,V.Cliente, (select Sum(Vit.ValUnit) as Total from Vendas Ve inner join VendasItens Vit on Vit.Pedido = Ve.Id Where Ve.Id = V.Id) as Total,VI.Produto,P.Descricao,VI.ValUnit,VI.QuantProd " +
                                       "FROM Vendas V  " +
                                       "inner join VendasItens VI ON V.Id = VI.Pedido " +
                                       "inner join Produtos P on P.Id = VI.Produto " +
                                       "where V.id = " + id + "" +
                                       "Group by V.Id, V.Data,V.Cliente,VI.Produto,P.Descricao,VI.ValUnit,VI.QuantProd";
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Vendas venda = new Vendas();
                        venda.ID = (int)reader["Id"];
                        //venda.CodCli = (int)reader["CodCli"];
                        venda.Cliente = (string)reader["Cliente"];
                        venda.Data = (DateTime)reader["Data"];
                        venda.Total = (decimal)reader["Total"];

                        venda.Produto = (string)reader["Descricao"];
                        venda.Quantidade = (int)reader["QuantProd"];
                        venda.ValorUnit = (decimal)reader["ValUnit"];


                        lista.Add(venda);
                    }

                    if (lista == null)
                    {
                        return HttpNotFound();
                    }

                    conn.Close();
                }
            }

            return View(lista);
        }

        // GET: Vendas/Create
        public ActionResult Create()
        {
            Vendas vendas = new Vendas();

            //CarregarListas(vendas);

            return View(vendas);
        }

        // POST: Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendas vendas)
        {
            //if (ModelState.IsValid)
            //{
            //    vendas.Data = DateTime.Now;
            //    vendas.CodCli = 1;
            //    vendas.Produto = "";
            //    vendas.Quantidade = 0;
            //    vendas.ValorUnit = 0;
            //    vendas.Total = 0;
            //    db.Vendas.Add(vendas);
            //    db.SaveChanges();
            //}
            string _connStr = "Data Source=ITRIAD00307;Initial Catalog=BDTransire;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"insert into Vendas (CodCli,Data,Total,Cliente) " +
                               "values(1,GETDATE(),0,'" + vendas.Cliente + "')";


                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (SqlException ex)
                    {
                        conn.Close();
                    }                    
                }                
            }
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

            return Json(new { Resultado = codigoV }, JsonRequestBehavior.AllowGet);
        }

        // GET: Vendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendas vendas = db.Vendas.Find(id);
            if (vendas == null)
            {
                return HttpNotFound();
            }
            return View(vendas);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CodCli,Data,Cliente,Total")] Vendas vendas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendas);
        }

        // GET: Vendas/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Vendas vendas = db.Vendas.Find(id);

            string _connStr = "Data Source=ITRIAD00307;Initial Catalog=BDTransire;Integrated Security=True";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Vendas vendas = db.Vendas.Find(id);

            List<Vendas> lista = new List<Vendas>();

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT V.Id, V.Data,V.Cliente, Sum(VI.ValUnit) as Total,VI.Produto,P.Descricao,VI.ValUnit,VI.QuantProd " +
                                       "FROM Vendas V  " +
                                       "inner join VendasItens VI ON V.Id = VI.Pedido " +
                                       "inner join Produtos P on P.Id = VI.Produto " +
                                       "where V.id = " + id + "" +
                                       "Group by V.Id, V.Data,V.Cliente,VI.Produto,P.Descricao,VI.ValUnit,VI.QuantProd";
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Vendas venda = new Vendas();
                        venda.ID = (int)reader["Id"];
                        //venda.CodCli = (int)reader["CodCli"];
                        venda.Cliente = (string)reader["Cliente"];
                        venda.Data = (DateTime)reader["Data"];
                        venda.Total = (decimal)reader["Total"];

                        venda.Produto = (string)reader["Descricao"];
                        venda.Quantidade = (int)reader["QuantProd"];
                        venda.ValorUnit = (decimal)reader["ValUnit"];


                        lista.Add(venda);
                    }

                    if (lista == null)
                    {
                        return HttpNotFound();
                    }

                    conn.Close();
                }
            }

            return View(lista);
        }

        // POST: Vendas/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Vendas vendas = db.Vendas.Find(id);
            //db.Vendas.Remove(vendas);
            //db.SaveChanges();

            string _connStr = "Data Source=ITRIAD00307;Initial Catalog=BDTransire;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"delete from VendasItens where Pedido = " + id +
                                       "delete from Vendas where Id = " + id;


                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (SqlException ex)
                    {
                        conn.Close();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public List<Clientes> CarregaClientes()
        //{
        //    var lista = db.Clientes.ToList();

        //    return lista;
        //}

        //private void CarregarListas(Vendas itemViewModel)
        //{
        //    itemViewModel.Clientes = CarregaClientes();

        //}
    }
}
