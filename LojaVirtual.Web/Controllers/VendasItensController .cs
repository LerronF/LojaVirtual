using LojaVirtual.Web.Data;
using LojaVirtual.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LojaVirtual.Web.Controllers
{
    public class VendasItensController : Controller
    {
        private LojaVirtualContext db = new LojaVirtualContext();

        // GET: VendasItens
        public ActionResult Index()
        {
            return View(db.VendasItens.ToList());
        }

        // GET: VendasItens/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendasItens vendasItens = db.VendasItens.Find(id);
            if (vendasItens == null)
            {
                return HttpNotFound();
            }
            return View(vendasItens);
        }

        // GET: VendasItens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdVenda,Item,CodProd,ValUnit,QuantProd,ValTotal")] VendasItens vendasItens)
        {
            if (ModelState.IsValid)
            {
                db.VendasItens.Add(vendasItens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendasItens);
        }

        // GET: VendasItens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendasItens vendasItens = db.VendasItens.Find(id);
            if (vendasItens == null)
            {
                return HttpNotFound();
            }
            return View(vendasItens);
        }

        // POST: VendasItens/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdVenda,Item,CodProd,ValUnit,QuantProd,ValTotal")] VendasItens vendasItens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendasItens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendasItens);
        }

        // GET: VendasItens/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendasItens vendasItens = db.VendasItens.Find(id);
            if (vendasItens == null)
            {
                return HttpNotFound();
            }
            return View(vendasItens);
        }

        // POST: VendasItens/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VendasItens vendasItens = db.VendasItens.Find(id);
            db.VendasItens.Remove(vendasItens);
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
