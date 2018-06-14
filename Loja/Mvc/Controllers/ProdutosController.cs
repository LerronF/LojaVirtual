using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class ProdutosController : Controller
    {
        // GET: Produtos
        public ActionResult Index()
        {
            IEnumerable<mvcProdutosModel> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Produtos").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<mvcProdutosModel>>().Result;
            return View(empList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            return View(new mvcProdutosModel());
        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcProdutosModel emp)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Produtos", emp).Result;
            return RedirectToAction("Index");
        }
    }
}