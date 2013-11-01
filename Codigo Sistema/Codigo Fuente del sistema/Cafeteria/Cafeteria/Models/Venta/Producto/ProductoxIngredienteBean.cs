using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Venta.Producto
{
    public class ProductoxIngredienteBean
    {
        public string idProducto { get; set; }
        [Display(Name = "Producto")]
        public string nombreProducto { get; set; }
        [Display(Name = "Tipo")]
        public string tipo { get; set; }
        public List<ProductoxIngrediente> listaIngredientes { get; set; }

    }
}