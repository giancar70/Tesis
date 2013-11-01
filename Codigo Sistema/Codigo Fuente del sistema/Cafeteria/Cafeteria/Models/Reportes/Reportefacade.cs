using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models.Reportes
{
    public class reportefacade
    {
        ReporteService reporteservice = new ReporteService();

        #region area de almacen

        public List<List<String>> reportealmacen(string fecha1, string fecha2, string idSucursal)
        {
            string a = fecha1, b = fecha2;
            fecha1 = cambiofecha(a);
            fecha2 = cambiofecha(b);
            return reporteservice.reportealmacen(fecha1, fecha2, idSucursal);
        }

        #endregion

        #region area de compras
        public List<List<String>> reportecompras(string idSucursal, string fecha1, string fecha2, string idproveedor, string monto1, string monto2)
        {
            string a = fecha1, b = fecha2;
            fecha1 = cambiofecha(a);
            fecha2 = cambiofecha(b);
            
            return reporteservice.reportecompras(idSucursal, fecha1, fecha2, idproveedor, monto1, monto2);
        }

        #endregion

        #region area de ventas
        public List<List<String>> reporteventas(string idSucursal, string fecha1, string fecha2, string monto1, string monto2)
        {
            string a = fecha1, b = fecha2;
            fecha1 = cambiofecha(a);
            fecha2 = cambiofecha(b);
            
            return reporteservice.reporteventas(idSucursal, fecha1, fecha2, monto1, monto2);
        }

        #endregion

        private string cambiofecha(string fecha)
        {
            int tamaño = fecha.Length;
            string dd = fecha.Substring(0, 2);
            string mm = fecha.Substring(3, 2);
            string aa = fecha.Substring(6);
            return mm+"/"+dd+"/"+aa;
        }
    }
}