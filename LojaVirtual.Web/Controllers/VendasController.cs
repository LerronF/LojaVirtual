using AutoMapper;
using LojaVirtual.Web.Data;
using LojaVirtual.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LojaVirtual.Web.Controllers
{
    public class VendasController : Controller
    {
        private LojaVirtualContext db = new LojaVirtualContext();

        // GET: Vendas
        public ActionResult Index()
        {
            return View(db.Vendas.ToList());
        }

        // GET: Vendas/Details
        public ActionResult Details(int? id)
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

        // GET: Vendas/Create
        public ActionResult Create()
        {
            Vendas vendas = new Vendas();
            return View(vendas);
        }

        // POST: Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Vendas vendas)
        {
            if (ModelState.IsValid)
            {
                db.Vendas.Add(vendas);
                db.SaveChanges();
            }

            return Json(new { Resultado = vendas.ID },JsonRequestBehavior.AllowGet);
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
            Vendas vendas = db.Vendas.Find(id);
            if (vendas == null)
            {
                return HttpNotFound();
            }
            return View(vendas);
        }

        // POST: Vendas/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendas vendas = db.Vendas.Find(id);
            db.Vendas.Remove(vendas);
            db.SaveChanges();
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
    }
}
