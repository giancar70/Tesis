using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafeteria.Models;

namespace Cafeteria.Controllers.Administracion
{
    public class CafeteriaController : Controller
    {
        
        #region Metodos JSON para combos de Ubigeo
        public ActionResult getProvincias(string idDepartamento)
        {
            return Json(Utils.listarProvincias(idDepartamento), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getDistritos(string idDepartamento, string idProvincia)
        {
            return Json(Utils.listarDistritos(idDepartamento, idProvincia), JsonRequestBehavior.AllowGet);
        }

        #endregion



    }
}
