
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Web.Models
{
    public partial class Vendas 
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Código")]
        public int CodCli { get; set; }

        [Display(Name = "Nome Cliente")]
        public string Cliente { get; set; }

        [Display(Name = "Data")]
        public DateTime? Data { get; set; }

        [Display(Name = "Total")]
        public decimal? Total { get; set; }


        public string Produto { get; set; }
        public decimal? Quantidade { get; set; }
        public decimal? ValorUnit { get; set; }

        //public virtual VendasItens vendasItens { get; set; }
    }
}