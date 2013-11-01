using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafeteria.Models.Administracion.Usuario;
using Cafeteria.Models.Administracion.Sucursal;

namespace Cafeteria.Models.Administracion
{
    public class administracionfacade
    {

        UsuarioService usuarioService = new UsuarioService();
        SucursalService sucursalservice = new SucursalService();

        #region login 

        public UsuarioBean Getlogin(string user, string pass)
        {
            return usuarioService.getLogin(user, pass);
        }

        #endregion

        #region Personal
        public List<UsuarioBean> ListarPersonal(string nombre, string dni, string cargo, string sucursal)
        {
            List<UsuarioBean> usu = new List<UsuarioBean>();
            usu = usuarioService.ListarPersonal(nombre, dni, cargo, sucursal);
            return usu;
        }

        public UsuarioBean buscarusuario(string idusuario)
        {
            UsuarioBean usuario = new UsuarioBean();
            usuario = usuarioService.buscarusuario(idusuario);
            return usuario;
        }

        public void registrarpersonal(UsuarioBean usuario)
        {
            usuarioService.registrarpersonal(usuario);

        }

        public void eliminarusuario(string ID)
        {
            usuarioService.eliminarusuario(ID);
            
        }

        public void actualizarusuario(UsuarioBean usuario)
        {
            usuarioService.actualizarusuario(usuario);
        }

        public void guardarhorario(UsuarioxSucursalBean usuario)
        {
            usuarioService.guardarhorario(usuario);
        }

        public void modificarhorario(UsuarioxSucursalBean usuario)
        {
            usuarioService.modificarhorario(usuario);
        }
        public UsuarioxSucursalBean obtenerhorario(String id)
        {
            return usuarioService.obtenerhorario(id);
        }

        #endregion

        #region perfil

        public List<UsuarioxSucursalBean> ListarPersonalconperfil(string nombre, string dni, string perfil)
        {
            List<UsuarioxSucursalBean> usu = new List<UsuarioxSucursalBean>();
            usu = usuarioService.ListarPersonalconperfil(nombre, dni, perfil);
            return usu;
        }

        public List<String> obtenerperfiles(string ID)
        {
            List<String> listaperfiles = new List<string>();

            listaperfiles =usuarioService.obtenerlistaperfiles(ID);
            return listaperfiles;
        }

        public List<string> getperfiles()
        {
            List<string> listaperfiles = new List<string>();

            listaperfiles = usuarioService.getperfiles();
            return listaperfiles;
        }

        public void guardarperfil(UsuarioxSucursalBean usuario)
        {
            usuarioService.guardarperfil(usuario);
        }

        #endregion

        #region Sucursal

        public void registrarSucursal(SucursalBean suc)
        {
            sucursalservice.registrar(suc);
        }

        public List<SucursalBean> listasucursal()
        {
            List<SucursalBean> sucur = new List<SucursalBean>();
            sucur = sucursalservice.listarsucursal();
            return sucur;
        }

        public SucursalBean buscarSucursal(string Id)
        {
            SucursalBean suc = new SucursalBean();
            suc = sucursalservice.buscarSucursal(Id);
            return suc;
        }

        public void EliminarSucursal(string Id)
        {
            sucursalservice.EliminarSucursal(Id);
        }

        public void ActualizarSucursal(SucursalBean suc)
        {
            sucursalservice.ActualizarSucursal(suc);
        }


        public List<sucursalproductoBean> obtenerproductsucursal(string idsucursal)
        {
            return sucursalservice.obtenerproduct(idsucursal);
        }

        public void añadirproductos(SucursalBean suc)
        {
            sucursalservice.añadirproductos(suc);
        }

        public void modificarproductos(SucursalBean suc)
        {
            sucursalservice.modificarproductos(suc);
        }

        #endregion

        #region Sucursalxpersonal

        public List<UsuarioBean> obtenerlistapersonal(string id)
        {
            return sucursalservice.obtenerlistapersonal(id);
        }

        public List<List<String>> obtenerlistapersonaltotal()
        {
            return sucursalservice.obtenerlistapersonaltotal();
        }

        public void eliminarpersonaldesucu(SucursalBean suc)
        {
            sucursalservice.eliminarpersonaldesucu(suc);
        }

        public void guardarnuevopersonal(SucursalBean suc)
        {
            sucursalservice.guardarnuevopersonal(suc);
        }

        public string obtenersucursal(string idusua)
        {
            return usuarioService.obtenersucursal(idusua);
        }
        

        #endregion

    }
}