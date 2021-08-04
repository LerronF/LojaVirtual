using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LojaVirtual.Web.Models
{
    public partial class Clientes
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}