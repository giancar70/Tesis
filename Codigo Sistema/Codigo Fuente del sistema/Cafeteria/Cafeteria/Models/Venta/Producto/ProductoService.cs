using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models.Venta.Producto
{
    public class ProductoService
    {
        ProductoDao ProductoDAo = new ProductoDao();

        #region Producto
        public List<ProductoBean> ListarProducto(string nombre, string idtipo)
        {
            List<ProductoBean> prod = new List<ProductoBean>();
            prod = ProductoDAo.ListarProductos(nombre, idtipo);

            return prod;
        }

        public void RegistrarProducto(ProductoBean prod)
        {
            ProductoDAo.registrarProducto(prod);
        }
        public ProductoBean BuscarProducto(string id)
        {
            return ProductoDAo.BuscarProducto(id);
        }
        public void ActualizarProducto(ProductoBean ingre)
        {
            ProductoDAo.ActualizarProducto(ingre);
        }
        public void EliminarProducto(string ID)
        {
            ProductoDAo.EliminarProducto(ID);
        }

        public string get_tipo(string ID_tipo)
        {
            return ProductoDAo.getTipo(ID_tipo);
        }
        #endregion
        #region ProductoxIngrediente
        public ProductoxIngredienteBean listardeingredientesdeproducto(string ID)
        {
            return ProductoDAo.listaIngredientes(ID);
        }
        public void AñadirIngredientesdeproducto(ProductoxIngredienteBean ProdxIngre)
        {
            ProductoDAo.AñadirIngredientes(ProdxIngre);
        }
        public void ModificarIngredientesdeproducto(ProductoxIngredienteBean ProdxIngre)
        {
            ProductoDAo.ModificarIngredientes(ProdxIngre);
        }
        #endregion

    }
}