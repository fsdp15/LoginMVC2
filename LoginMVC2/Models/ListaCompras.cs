using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginMVC2.Models
{

    public class ListaCompras
    {
        public ListaCompras() { }

        public List<Compras> Items { get; set; }
        public DateTime DataCompra { get; set; }
        
    }
}