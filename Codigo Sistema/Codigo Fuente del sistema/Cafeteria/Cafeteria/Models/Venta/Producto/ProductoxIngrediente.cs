using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafeteria.Models.Almacen.Ingrediente;

namespace Cafeteria.Models.Venta.Producto
{
    public class ProductoxIngrediente:IngredienteBean
    {
        public Boolean estadod_disponible { get; set; }
        public int cantidad { get; set; }
        public string medida { get; set; }

    }
}