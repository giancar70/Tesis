using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Compra.Ordencompra
{
    public class OrdenProducto
    {
        [Display(Name = "Proveedor")]
        public string proveedor { get; set; }
        public string idOrdencompra { get; set; }
        //public int id { get; set; } //idproveedor
        public string idproveedor { get; set; }
        public List<Producto> listaProducto { get; set; }

        [Display(Name = "IdCafeteria")]
        public string idcafeteria { get; set; }

        [Display(Name = "Sucursal")]
        public string nombrecafeteria { get; set; }

        public Boolean estado2 { get; set; }
    }
}