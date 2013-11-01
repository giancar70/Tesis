using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Compra.Proveedor
{
    public class ProveedorxIngredienteBean
    {
        [Display(Name = "Proveedor")]
        public String nombreProveedor { get; set; }
        public string idProveedor { get; set; }
        public List<ProveedorIngrediente> listadeIngredientesProveedor { get; set; }
    }
}