using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models.Venta.Venta
{
    public class VentaService
    {

        VentaDao ventadao = new VentaDao();
        public List<VentaBean> buscarventas(string fecha, string idsucursal)
        {
            return ventadao.buscarventas(fecha, idsucursal);

        }

        public List<VentaxProductoBean> obtenerlistaproductos(string idSucursal)
        {
            return ventadao.obtenerlistaproductos(idSucursal);
        }

        public void registrarVenta(VentaBean ven)
        {
            ventadao.registrarVenta(ven);
        }

        public VentaBean buscarventa(string id)
        {
            return ventadao.buscarventa(id);
        }
        public void descontar(VentaBean ven)
        {
            ventadao.descontar(ven);
        }

    }
}