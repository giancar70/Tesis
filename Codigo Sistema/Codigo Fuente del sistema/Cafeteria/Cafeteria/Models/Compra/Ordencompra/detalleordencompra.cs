using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafeteria.Models.Almacen.Ingrediente;

namespace Cafeteria.Models.Compra.Ordencompra
{
    public class detalleordencompra:IngredienteBean
    {
        public int Cantidad { get; set; }
        public int Cantidadentrante { get; set; }
        public decimal precio { get; set; }
        public decimal preciounitario { get; set; }
    }
}