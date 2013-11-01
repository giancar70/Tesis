using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafeteria.Models;
using System.ComponentModel.DataAnnotations;
using Cafeteria.Models.Almacen.Ingrediente;


namespace Cafeteria.Models.Compra.Proveedor
{
    public class ProveedorIngrediente:IngredienteBean
    {
        
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado es incorrecto")]
        public decimal precio { get; set; }

        public string precio2 { get; set; }
        public bool Estado_disponible { get; set; }

    }
}