using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Cafeteria.Models.Compra.Proveedor
{
    public class ProveedorService
    {
        ProveedorDao ProveedorDao = new ProveedorDao();

        #region Proveedor
        public List<ProveedorBean> ListarProveedor(string nombre, string contacto)
        {
            List<ProveedorBean> prod = new List<ProveedorBean>();
            prod=ProveedorDao.ListarProveedor(nombre, contacto);

            return prod;
        }

        public void RegistrarProveedor(ProveedorBean prod)
        {
            ProveedorDao.RegistrarProveedor(prod);
        }
        public ProveedorBean BuscarProveedor(string id)
        {
            return ProveedorDao.BuscarProveedor(id);
        }
        public void ActualizarProveedor(ProveedorBean ingre)
        {
            ProveedorDao.ActualizarProveedor(ingre);
        }
        public void EliminarProveedor(string ID)
        {
            ProveedorDao.EliminarProveedor(ID);
        }
        public Boolean existe_ruc(string ruc)
        {
            return (ProveedorDao.existe_ruc(ruc));
        }
        public Boolean existe_razonsocial(string razonsocial)
        {
            return (ProveedorDao.existe_razonSocial(razonsocial));
        }

        #endregion
        #region ProveedorIngrediente

        public ProveedorxIngredienteBean listardeingredientes(string ID)
        {
            return ProveedorDao.listaIngredientes(ID);
        }
        public void AñadirIngredientes(ProveedorxIngredienteBean ProvxIngre)
        {
            ProveedorDao.AñadirIngredientes(ProvxIngre);
        }
        public void ModificarIngredientes(ProveedorxIngredienteBean prov)
        {
            ProveedorDao.ModificarIngredientes(prov);
        }
        #endregion

    }
}