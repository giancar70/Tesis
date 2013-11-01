using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Cafeteria.Models.Compra;
using Cafeteria.Models;
using Cafeteria.Models.Compra.Proveedor;
using Cafeteria.Models.Almacen.Ingrediente;
using Cafeteria.Models.Almacen;


namespace Cafeteria.Controllers.Compras
{
    public class ProveedorController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(ProveedorController));
        comprasfacade comprasfacade = new comprasfacade();
        almacenfacade Almacenfacade = new almacenfacade();

        #region Proveedor
        public ActionResult Index()
        {
            List<ProveedorBean> prod = comprasfacade.ListarProveedor("", "");
            return View(prod);
            
        }


        public ActionResult Details(string  id)
        {
            ProveedorBean pro = comprasfacade.BuscarProveedor(id);
            return View(pro);
        }

        #region Buscar
        public ActionResult Buscar()
        {
            List<ProveedorBean> prod = comprasfacade.ListarProveedor("", "");


            ViewBag.estado = 0;
            return View(prod);
        }

        [HttpPost]
        public ActionResult Buscar(string nombre, string contacto)
        {
            ViewBag.estado = 1;
            return View(comprasfacade.ListarProveedor(nombre, contacto));
        }

        #endregion

        #region Registrar

        public ActionResult Create()
        {
            return View();
        } 


        [HttpPost]
        public ActionResult Create(ProveedorBean prov)
        {
            try
            {
                Boolean opcion1 = comprasfacade.existe_ruc(prov.ruc);
                Boolean opcion2 = comprasfacade.existe_razonSocial(prov.razonSocial);
                if (opcion1)
                {
                    ViewBag.error1 = "El Proveedor ya existe";
                    return View(prov);
                }
                else
                    if (opcion2)
                    {
                        ViewBag.error2 = "El numero de RUC ya existe";
                        return View(prov);
                    }
                    else
                    {
                        comprasfacade.RegistrarProveedor(prov);
                        return RedirectToAction("Index");
                    }
            }
            catch(Exception e)
            {
                log.Error("Create - GET(EXCEPTION):", e);
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        #endregion

        #region editar
        public ActionResult Edit(string id)
        {
            ProveedorBean Producto = comprasfacade.BuscarProveedor(id);
            return View(Producto);
        }

        [HttpPost]
        public ActionResult Edit(ProveedorBean Proveedor)
        {
            try
            {
                comprasfacade.ActualizarProve(Proveedor);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region eliminar
        public ActionResult Delete(string ID)
        {
            return View(comprasfacade.BuscarProveedor(ID));
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(string ID)
        {
            comprasfacade.EliminarProveedor(ID);
            return Json(new { me = "" });
        }

        #endregion

        #endregion

        #region ProveedorxIngrediente

        public ViewResult ListarIngredientes(string ID)
        {
            
            ProveedorxIngredienteBean ProveIngre = new ProveedorxIngredienteBean();
            
            ProveIngre= comprasfacade.obtenerlistadeingredientes(ID);
            ProveedorBean proveedor = comprasfacade.BuscarProveedor(ID);
            ProveIngre.nombreProveedor = proveedor.razonSocial;
            ProveIngre.idProveedor = proveedor.id;
            if (ProveIngre.listadeIngredientesProveedor.Count > 0) ViewBag.estado = 0;
            else ViewBag.estado = 1;
            for (int i = 0; i < ProveIngre.listadeIngredientesProveedor.Count; i++)
            {
                IngredienteBean Ingre = Almacenfacade.buscaringrediente(ProveIngre.listadeIngredientesProveedor[i].id);
                ProveIngre.listadeIngredientesProveedor[i].nombre = Ingre.nombre;
                
            }
            return View(ProveIngre);
        }

        public ActionResult ModificarIngredientes(ProveedorxIngredienteBean Prov) 
        {
            ProveedorxIngredienteBean ProveIngre = comprasfacade.obtenerlistadeingredientes(Prov.idProveedor);

            for (int i = 0; i < Prov.listadeIngredientesProveedor.Count; i++)
            {
                for (int j = 0; j < ProveIngre.listadeIngredientesProveedor.Count; j++)
                {
                    if (Prov.listadeIngredientesProveedor[i].id == ProveIngre.listadeIngredientesProveedor[j].id)
                    {
                        Prov.listadeIngredientesProveedor[i].precio = ProveIngre.listadeIngredientesProveedor[j].precio;
                    }
                }
            }
            return View(Prov);
        }

        
        public ActionResult ModificarIngredientes2(ProveedorxIngredienteBean Prov)
        {
            comprasfacade.Modificaringredientes(Prov);
            return RedirectToAction("ListarIngredientes/" + Prov.idProveedor, "Proveedor");
        }

        public ActionResult AñadirIngredientes(string ID) //idproveedor
        {
            ProveedorBean proveedor = comprasfacade.BuscarProveedor(ID);
            List<IngredienteBean> Ingredientes = Almacenfacade.ListarIngrediente("");
            ProveedorxIngredienteBean ProveIngre = new ProveedorxIngredienteBean();
            ProveIngre.nombreProveedor = proveedor.razonSocial;
            ProveIngre.idProveedor = proveedor.id;
            ProveIngre.listadeIngredientesProveedor = new List<ProveedorIngrediente>();
            ProveedorxIngredienteBean aux = comprasfacade.obtenerlistadeingredientes(ID);
            for (int j = 0; j < Ingredientes.Count; j++)
            {
                ProveedorIngrediente proveedorIngre = new ProveedorIngrediente();
                proveedorIngre.id = Ingredientes[j].id;
                proveedorIngre.nombre = Ingredientes[j].nombre;
                for (int i = 0; i < aux.listadeIngredientesProveedor.Count; i++)
                {
                    if (aux.listadeIngredientesProveedor[i].id == Ingredientes[j].id) proveedorIngre.Estado_disponible = true;
                }
                ProveIngre.listadeIngredientesProveedor.Add(proveedorIngre);
            }
            
            return View(ProveIngre);
        }

        [HttpPost]
        public ActionResult AñadirIngredientes(ProveedorxIngredienteBean ProvexIngre)
        {

            for (int i = 0; i < ProvexIngre.listadeIngredientesProveedor.Count; i++)
            {
                string preci = ProvexIngre.listadeIngredientesProveedor[i].precio2;
                if (!String.IsNullOrEmpty(preci))
                {
                    Decimal preci2 = Convert.ToDecimal(preci, new System.Globalization.CultureInfo("en-US"));
                    ProvexIngre.listadeIngredientesProveedor[i].precio=preci2;
                }
            }

            comprasfacade.AñadirIngredientes(ProvexIngre);
            return RedirectToAction("ListarIngredientes/"+ProvexIngre.idProveedor,"Proveedor");
        }

        

        #endregion

    }
}
