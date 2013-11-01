using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafeteria.Models.Venta.Producto;

namespace Cafeteria.Models.Administracion.Sucursal
{
    public class sucursalproductoBean:ProductoBean
    {
        public int cantidad { get; set; }
        public decimal precioventa { get; set; }
        public bool estadodispo { get; set; }
        public string precioventa2 { get; set; }
    }
}