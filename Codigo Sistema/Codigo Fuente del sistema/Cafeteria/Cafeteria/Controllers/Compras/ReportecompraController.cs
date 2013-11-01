using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Cafeteria.Models;
using Cafeteria.Models.Reportes;
using ReportManagement;

namespace Cafeteria.Controllers.Compras
{
    public class ReportecompraController : PdfViewController
    {
        private static ILog log = LogManager.GetLogger(typeof(ReportecompraController));
        reportefacade reportefacade = new reportefacade();
        public ActionResult filtro()
        {
            Reporte reporte = new Reporte();
            return View(reporte);
        }
        public ActionResult Resultado(string idSucursal, string fecha1, string fecha2, string idproveedor, string monto1, string monto2)
        {
            List<List<String>> lista = reportefacade.reportecompras(idSucursal, fecha1, fecha2, idproveedor, monto1, monto2);
            Reporte reporte = new Reporte(); reporte.fecha1 = fecha1; reporte.fecha2 = fecha2;
            reporte.listacompras = lista;
            List<int> cantidad = new List<int>();

            for (int i = 0; i < lista.Count; i++)
            {
                int aux = 0;
                string variable = lista[i][0];
                for (int j = 0; j < lista.Count; j++)
                {
                    if (variable.CompareTo(lista[j][0]) == 0)
                    {
                        aux++;
                    }
                }
                cantidad.Add(aux);
            }
            reporte.cantidad = cantidad;
            int k = 1, pos, cant;
            while (k < reporte.listacompras.Count)
            {
                if (reporte.listacompras[k - 1][0].CompareTo(reporte.listacompras[k][0]) == 0)
                {
                    pos = k - 1;
                    cant = reporte.cantidad[pos];
                    reporte.cantidad[pos] = 1;
                    k = k + cant - 2;
                }
                k++;

            }
            if (monto1.CompareTo("") != 0 && monto2.CompareTo("") != 0)
            {
                //double montoa = 55.5;
                Decimal montoa = Convert.ToDecimal(monto1, new System.Globalization.CultureInfo("en-US"));
                Decimal montob = Convert.ToDecimal(monto2, new System.Globalization.CultureInfo("en-US"));
                for (int i = 0; i < reporte.listacompras.Count; i++)
                {
                    Decimal montoaux = Convert.ToDecimal(reporte.listacompras[i][3]);//, new System.Globalization.CultureInfo("en-US"));
                    reporte.listacompras[i][3] = Convert.ToString(montoaux);
                    if (montoa <= montoaux && montoaux <= montob)
                    {
                        reporte.listacompras[i][4] = "activo";
                        
                    }
                    else
                    {
                        reporte.listacompras[i][4] = "inactivo";
                    }

                }
            }

            return this.ViewPdf("", "reportefinal", reporte);
            
        }


        
    }
}
