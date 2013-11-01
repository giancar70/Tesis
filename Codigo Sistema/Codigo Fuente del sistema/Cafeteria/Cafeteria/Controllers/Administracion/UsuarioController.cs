using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafeteria.Models.Administracion.Usuario;
using log4net;
using Cafeteria.Models;
using System.Web.Security;
using Cafeteria.Models.Administracion;
using Cafeteria.Models.Administracion.Sucursal;
using System.Security.Cryptography;
using System.Text;


namespace Cafeteria.Controllers.Administracion
{
    public class UsuarioController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(UsuarioController));
        administracionfacade admifacade = new administracionfacade();


        public ActionResult Index()
        {
            List<UsuarioBean> usuario = admifacade.ListarPersonal("", "", "", "");


            ViewBag.estado = 0;
            return View(usuario);
                       
        }

        #region detalle
        public ActionResult Details(string id)
        {
            UsuarioBean usuario = new UsuarioBean();
            usuario = admifacade.buscarusuario(id);
            return View(usuario);
        }
        #endregion


        #region asignarhorario

        public ActionResult verhorario(String id)
        {
            UsuarioBean usarios = admifacade.buscarusuario(id);
            UsuarioxSucursalBean usua = new UsuarioxSucursalBean();
            usua = admifacade.obtenerhorario(id);
            usua.ID = id;
            usua.nombres = usarios.nombres;
            usua.nroDocumento = usarios.nroDocumento;
            usua.idsucursal = admifacade.obtenersucursal(id);
            if (usua.idsucursal.CompareTo("vacio") != 0)
            {
                SucursalBean suc = admifacade.buscarSucursal(usua.idsucursal);
                usua.sucursal = suc.nombre;
            }
            return View(usua);
        }

        public ActionResult verhorario2(String id)
        {
            UsuarioBean usarios = admifacade.buscarusuario(id);
            UsuarioxSucursalBean usua = new UsuarioxSucursalBean();
            usua = admifacade.obtenerhorario(id);
            usua.ID = id;
            usua.nombres = usarios.nombres;
            usua.nroDocumento = usarios.nroDocumento;
            usua.idsucursal = admifacade.obtenersucursal(id);
            if (usua.idsucursal.CompareTo("vacio") != 0)
            {
                SucursalBean suc = admifacade.buscarSucursal(usua.idsucursal);
                usua.sucursal = suc.nombre;
            }
            return View(usua);
        }
        public ActionResult Horario(String id)
        {
            UsuarioBean usuario = new UsuarioBean();
            UsuarioxSucursalBean usua = new UsuarioxSucursalBean();
            usuario = admifacade.buscarusuario(id);
            usua.ID = usuario.ID;
            usua.nroDocumento = usuario.nroDocumento;

            usua.idsucursal = admifacade.obtenersucursal(id);
            if (usua.idsucursal.CompareTo("vacio") != 0)
            {
                SucursalBean suc = admifacade.buscarSucursal(usua.idsucursal);
                usua.sucursal = suc.nombre;
            }

            usua.nombres = usuario.nombres +" "+ usuario.apPat + " "+usuario.apMat;
            usua.dia = new List<string>();
            usua.horaFin = new List<string>();
            usua.horaInicio = new List<string>();
            usua.dia.Add("Lunes");
            usua.dia.Add("Martes");
            usua.dia.Add("Miercoles");
            usua.dia.Add("Jueves");
            usua.dia.Add("Viernes");
            usua.dia.Add("Sabado");
            usua.dia.Add("Domingo");
            for (int i = 0; i < usua.dia.Count; i++)
            {
                usua.horaFin.Add("");
                usua.horaInicio.Add("");
            }
            
            return View(usua);
        }

        [HttpPost]
        public ActionResult Horario(UsuarioxSucursalBean usuario)
        {
            admifacade.guardarhorario(usuario);
            return RedirectToAction("verhorario/" + usuario.ID, "Usuario");
        }

        public ActionResult modificarHorario(UsuarioxSucursalBean usuario)
        {
            List<String> dia = new List<string>();
            List<String> horaini = new List<string>();
            List<String> horafin = new List<string>();
            dia.Add("Lunes");
            dia.Add("Martes");
            dia.Add("Miercoles");
            dia.Add("Jueves");
            dia.Add("Viernes");
            dia.Add("Sabado");
            dia.Add("Domingo");

            for (int i = 0; i < dia.Count; i++)
            {
                horaini.Add("");
                horafin.Add("");
            }

            for (int i = 0; i < dia.Count; i++)
            {
                for (int j = 0; j < usuario.dia.Count; j++)
                {
                    if (dia[i].CompareTo(usuario.dia[j]) == 0)
                    {
                       horaini[i]=usuario.horaInicio[j];
                       horafin[i]=usuario.horaFin[j];
                    }
                }
            }

            usuario.dia = dia;
            usuario.horaInicio = horaini;
            usuario.horaFin = horafin;
            return View(usuario);

        }

        public ActionResult modificarHorario2(UsuarioxSucursalBean usuario)
        {
            admifacade.modificarhorario(usuario);
            
            return RedirectToAction("verhorario/" + usuario.ID, "Usuario");

        }
        #endregion


        #region Crear
        public ActionResult Create()
        {
            var usuarioVMC = new UsuarioBean();
            try
            {
                usuarioVMC.Departamentos = Utils.listarDepartamentos();
                return View(usuarioVMC);
            }
            catch (Exception ex)
            {
                log.Error("Create - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(usuarioVMC);
            }
        }

        [HttpPost]
        public ActionResult Create(UsuarioBean usuario)
        {
            try
            {
                usuario.estado = "ACTIVO";
                List<UsuarioBean> usuarios = admifacade.ListarPersonal("", usuario.nroDocumento, "", "");
                if (usuarios.Count > 0)
                {
                    ViewBag.error = "El Usuario ya existe";
                    return View(usuario);
                }
                else
                {
                    admifacade.registrarpersonal(usuario);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                log.Error("Create - POST(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(usuario);
            }
        }
        #endregion

        #region editar
        public ActionResult Edit(string id)
        {
            UsuarioBean usuario = new UsuarioBean();
            usuario = admifacade.buscarusuario(id);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Edit(UsuarioBean usuario)
        {
            try
            {
                //guardar modificaciones
                admifacade.actualizarusuario(usuario);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        
                
        #region Buscar

        public ActionResult Buscar()
        {
            List<UsuarioBean> prod = admifacade.ListarPersonal("", "","", "");


            ViewBag.estado = 0;
            return View(prod);
        }

        [HttpPost]
        public ActionResult Buscar(string nombre, string cargo, string dni, string sucursal)
        {
            ViewBag.estado = 1;
            return View(admifacade.ListarPersonal(nombre, cargo, dni, sucursal));
        }

        #endregion


        #region Eliminar
        public ActionResult Delete(string ID)
        {
            return View(admifacade.buscarusuario(ID));
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(string ID)
        {
            admifacade.eliminarusuario(ID);
            //comprasfacade.EliminarProveedor(ID);
            return Json(new { me = "" });
        }
        #endregion



        #region AdministrarPerfil

        public ActionResult AdministrarPerfil()
        {
            //List<UsuarioBean> usua = admifacade.ListarPersonal2("", "", "", "");

            UsuarioxSucursalBean usua = new UsuarioxSucursalBean();
            List<UsuarioxSucursalBean> usa= new List<UsuarioxSucursalBean>();
            usa.Add(usua);
            ViewBag.estado = 0;

            return View(usa);
        }

        [HttpPost]
        public ActionResult AdministrarPerfil(string nombre, string dni, string idperfil)
        {
            ViewBag.estado = 1;
            List<UsuarioxSucursalBean> usua = admifacade.ListarPersonalconperfil(nombre, dni, idperfil);
            return View(usua);
        }

        public ActionResult Asignarperfil(string id)
        {
            //perfiles que tiene el usuario con el ID

            UsuarioxSucursalBean usuario = new UsuarioxSucursalBean();
            List<string> listaperfiles = admifacade.obtenerperfiles(id); //k tiene actualmente
            UsuarioBean usuar = admifacade.buscarusuario(id);

            usuario.ID = usuar.ID;
            usuario.nombres = usuar.nombres;
            usuario.apPat = usuar.apPat;
            usuario.apMat = usuar.apMat;
            usuario.nroDocumento = usuar.nroDocumento;
            usuario.perfilesDelUsuario = admifacade.getperfiles();
            usuario.estadosDePerfiles = new List<bool>();
            for (int i = 0; i < usuario.perfilesDelUsuario.Count; i++)
            {
                usuario.estadosDePerfiles.Add(false);
            }

            for (int i = 0; i < usuario.perfilesDelUsuario.Count; i++)
            {
                for (int j = 0; j < listaperfiles.Count; j++)
                {
                    if (usuario.perfilesDelUsuario[i] == listaperfiles[j])
                    {
                        usuario.estadosDePerfiles[i] = true;
                    }
                }
            }
            return View(usuario);
        }

        public ActionResult Modal(string id)
        {
            UsuarioxSucursalBean usuario = new UsuarioxSucursalBean();
            List<string> listaperfiles = admifacade.obtenerperfiles(id); //k tiene actualmente
            UsuarioBean usuar = admifacade.buscarusuario(id);

            usuario.ID = usuar.ID;
            usuario.nombres = usuar.nombres;
            usuario.apPat = usuar.apPat;
            usuario.apMat = usuar.apMat;
            usuario.nroDocumento = usuar.nroDocumento;
            usuario.perfilesDelUsuario = admifacade.getperfiles();
            usuario.estadosDePerfiles = new List<bool>();
            for (int i = 0; i < usuario.perfilesDelUsuario.Count; i++)
            {
                usuario.estadosDePerfiles.Add(false);
            }

            for (int i = 0; i < usuario.perfilesDelUsuario.Count; i++)
            {
                for (int j = 0; j < listaperfiles.Count; j++)
                {
                    if (usuario.perfilesDelUsuario[i] == listaperfiles[j])
                    {
                        usuario.estadosDePerfiles[i] = true;
                    }
                }
            }
            return PartialView(usuario);
        }
        public ActionResult GG2(UsuarioxSucursalBean usuario2)
        {
            int i = 0;
            //guardar nuevos perfiles con estado true
            admifacade.guardarperfil(usuario2);
            //return RedirectToAction("Index");
            return RedirectToAction("AdministrarPerfil");
        }
        #endregion


        #region asignarpersonalaSucursal

        public ActionResult AsignarpersonalSucursal()
        {

            return View();
        }

        #endregion


        /*login----------------------------->>>*/
        #region Login

        [HttpPost]
        public JsonResult LoginResult(String user, String password)
        {
            var usuario = admifacade.Getlogin(user, password);
            if (usuario != null)// && !usuario.Equals("ONLINE"))
            {
                FormsAuthentication.SetAuthCookie(user, false);
                //usuarioFac.marcarOnline(usuario.ID);
            }
            return new JsonResult() { Data = usuario };
        }
      
        #endregion

    }
}
