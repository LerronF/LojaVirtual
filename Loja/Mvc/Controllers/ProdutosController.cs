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
    }
}