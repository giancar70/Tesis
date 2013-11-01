using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Cafeteria.Models;
using Cafeteria.Models.Reportes;
using ReportManagement;

namespace Cafeteria.Controllers.Venta
{
    public class ReporteventaController : PdfViewController
    {

        private static ILog log = LogManager.GetLogger(typeof(ReporteventaController));
        reportefacade reportefacade = new reportefacade();

        public ActionResult filtro()
        {
            Reporte reporte = new Reporte();
            return View(reporte);
        }
        public ActionResult Resultado(string idSucursal, string fecha1, string fecha2,string monto1,string monto2)
        {

            List<List<String>> lista = reportefacade.reporteventas(idSucursal, fecha1, fecha2, monto1, monto2);
            Reporte reporte = new Reporte(); reporte.fecha1 = fecha1; reporte.fecha2 = fecha2;
            reporte.listaventas = lista;
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
            while (k < reporte.listaventas.Count)
            {
                if (reporte.listaventas[k - 1][0].CompareTo(reporte.listaventas[k][0]) == 0)
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
                for (int i = 0; i < reporte.listaventas.Count; i++)
                {
                    Decimal montoaux = Convert.ToDecimal(reporte.listaventas[i][2]);//, new System.Globalization.CultureInfo("en-US"));
                    reporte.listaventas[i][2] = Convert.ToString(montoaux);
                    if (montoa <= montoaux && montoaux <= montob)
                    {
                        reporte.listaventas[i][3] = "activo";

                    }
                    else
                    {
                        reporte.listaventas[i][3] = "inactivo";
                    }

                }
            }
            
            return this.ViewPdf("", "reportefinal", reporte);
            


        }



    }
}
