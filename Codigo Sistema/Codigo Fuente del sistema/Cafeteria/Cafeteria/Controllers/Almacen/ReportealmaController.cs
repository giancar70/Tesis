using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Cafeteria.Models;
using Cafeteria.Models.Reportes;
using ReportManagement;

namespace Cafeteria.Controllers.Almacen
{
    public class ReportealmaController : PdfViewController
    {
        private static ILog log = LogManager.GetLogger(typeof(ReportealmaController));
        reportefacade reportefacade = new reportefacade();

        public ActionResult filtro()
        {
            Reporte reporte = new Reporte();
            return View(reporte);
        }
        public ActionResult Resultado(string fecha1, string fecha2, string idSucursal)
        {
            List<List<String>> lista = reportefacade.reportealmacen(fecha1, fecha2, idSucursal);
            Reporte reporte = new Reporte(); reporte.fecha1 = fecha1; reporte.fecha2 = fecha2;
            reporte.listaalmacen = lista;
            List<int> cantidad = new List<int>();
            for (int i = 0; i < lista.Count; i++)
            {
                int aux = 0;
                string variable = lista[i][0];
                for (int j = 0; j < lista.Count; j++)
                {
                    if(variable.CompareTo(lista[j][0])==0){
                        aux++;
                    }
                }
                cantidad.Add(aux);
            }
            reporte.cantidad = cantidad;
            int k=1, pos, cant;
            while (k<reporte.listaalmacen.Count){
                if(reporte.listaalmacen[k-1][0].CompareTo(reporte.listaalmacen[k][0])==0){
                    pos=k-1;
                    cant=reporte.cantidad[pos];
                    reporte.cantidad[pos]= 1;
                    k=k+cant-2;
                }
                k++;

            }

            return this.ViewPdf("", "reportefinal", reporte);

        }




    
    }



}
