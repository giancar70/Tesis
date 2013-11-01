using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafeteria.Models.Administracion.Usuario;

namespace Cafeteria.Models.Administracion.Sucursal
{
    public class SucursalService
    {
        SucursalDao sucursalDAo = new SucursalDao();

        public void registrar(SucursalBean sucu)
        {
            sucursalDAo.registrar(sucu);
        }

        public List<SucursalBean> listarsucursal()
        {
            List<SucursalBean> suc = new List<SucursalBean>();
            suc = sucursalDAo.Listarsucursal();
            return suc;
        }

        public SucursalBean buscarSucursal(string Id)
        {
            SucursalBean suc = new SucursalBean();
            suc=sucursalDAo.buscarSucursal(Id);
            return suc;
        }

        public void EliminarSucursal(string Id)
        {
            sucursalDAo.EliminarSucursal(Id);
        }

        public void ActualizarSucursal(SucursalBean suc)
        {
            sucursalDAo.ActualizarSucursal(suc);
        }

        public List<sucursalproductoBean> obtenerproduct(string idsucursal)
        {
            return sucursalDAo.obtenerproduct(idsucursal);
        }

        public void añadirproductos(SucursalBean suc)
        {
            sucursalDAo.añadirproductos(suc);
        }

        public void modificarproductos(SucursalBean suc)
        {
            sucursalDAo.modificarproductos(suc);
        }

        #region personal
        public List<UsuarioBean> obtenerlistapersonal(string id)
        {
            return sucursalDAo.obtenerlistapersonal(id);
        }

        public List<List<String>> obtenerlistapersonaltotal()
        {
            return sucursalDAo.obtenerlistapersonaltotal();
        }

        public void eliminarpersonaldesucu(SucursalBean suc)
        {
            sucursalDAo.eliminarpersonaldesucu(suc);
        }

        public void guardarnuevopersonal(SucursalBean suc)
        {
            sucursalDAo.guardarnuevopersonal(suc);
        }
        #endregion
    }
}