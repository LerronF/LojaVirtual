using LojaVirtual.Web.Data;
using LojaVirtual.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            var item = new VendasItens()
            {
                QuantProd = vendas.QuantProd
            ,
                Produto = vendas.Produto
            ,
                ValUnit = vendas.ValUnit
            ,
                Id = vendas.Id
            ,
                Pedido = db.Vendas.Find(vendas.Pedido).ID
            };

            db.VendasItens.Add(item);

            db.SaveChanges();

            return Json(new { Resultado = vendas.Id }, JsonRequestBehavior.AllowGet);
        }
    }
}