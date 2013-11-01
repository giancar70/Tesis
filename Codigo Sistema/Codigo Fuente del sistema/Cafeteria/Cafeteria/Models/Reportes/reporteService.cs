using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models.Reportes
{
    public class ReporteService
    {
        ReporteDao reportedao = new ReporteDao();


        #region area de almacen


        public List<List<String>> reportealmacen(string fecha1, string fecha2, string idSucursal)
        {
            return reportedao.reportealmacen(fecha1, fecha2, idSucursal);
        }

        #endregion


        #region area de compras
        public List<List<String>> reportecompras(string idSucursal, string fecha1, string fecha2, string idproveedor, string monto1, string monto2)
        {
            return  reportedao.reportecompras( idSucursal, fecha1,fecha2, idproveedor, monto1,monto2);
        }

        #endregion

        #region area de ventas
        public List<List<String>> reporteventas(string idSucursal, string fecha1, string fecha2, string monto1, string monto2)
        {
            return reportedao.reporteventas(idSucursal, fecha1, fecha2, monto1, monto2);
        }

        #endregion


    }


}