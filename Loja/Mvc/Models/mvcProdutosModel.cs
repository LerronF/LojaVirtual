using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class mvcProdutosModel
    {
        public int Codigo { get; set; }
        [Required(ErrorMessage = "Preencha a descrição")]
        public string Descricao { get; set; }
        public Nullable<int> Estoque { get; set; }
    }
}