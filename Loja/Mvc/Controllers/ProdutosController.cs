using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
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
            if (id == 0)
                return View(new mvcProdutosModel());
            else
            {
                HttpResponseMessage response =
                    GlobalVariables.WebApiClient.GetAsync("Produtos/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcProdutosModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcProdutosModel emp)
        {
            if (emp.Codigo == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Produtos", emp).Result;
                TempData["SuccessMessage"] = "Salvo com Sucesso";
            }
            else
            {
                HttpResponseMessage response =
                    GlobalVariables.WebApiClient.PutAsJsonAsync("Produtos/"+emp.Codigo, emp).Result;
                TempData["SuccessMessage"] = "Edicao Realizada";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Produtos/"+id.ToString()).Result;
            TempData["SuccessMessage"] = "Deletado com Sucesso";
            return RedirectToAction("Index");
        }
    }
}