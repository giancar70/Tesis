using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafeteria.Models.Venta.Producto;

namespace Cafeteria.Models.Venta.Venta
{
    public class VentaxProductoBean:ProductoBean
    {
        public string preciosubtotal { get; set; }
        public decimal subtotal { get; set; }
        
        public string preciounit2 { get; set; }
        public decimal preciouniario { get; set; }
        
        public bool estado_venta { get; set; }
        public int cantidad { get; set; }

        public int cantidadsolicitada { get; set; }
    }
}