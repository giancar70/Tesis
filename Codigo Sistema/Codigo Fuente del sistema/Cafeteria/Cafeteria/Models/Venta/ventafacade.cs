using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafeteria.Models.Venta.Producto;
using Cafeteria.Models.Venta.Venta;

namespace Cafeteria.Models.Venta
{
    public class ventafacade
    {
        ProductoService ProductoService = new ProductoService();
        VentaService ventaservice = new VentaService();

        #region Producto
        public List<ProductoBean> ListarProducto(string nombre, string idtipo)
        {
            List<ProductoBean> prod = new List<ProductoBean>();
            prod = ProductoService.ListarProducto(nombre, idtipo);

            return prod;
        }
        public void RegistrarProducto(ProductoBean prod)
        {
            ProductoService.RegistrarProducto(prod);
        }
        public ProductoBean BuscarProducto(string id)
        {
            ProductoBean ingre = ProductoService.BuscarProducto(id);
            return ingre;
        }
        public void ActualizarProducto(ProductoBean ingre)
        {
            ProductoService.ActualizarProducto(ingre);
        }
        public void EliminarProducto(string id)
        {
            ProductoService.EliminarProducto(id);
        }
        public string get_tipo(string idtipo)
        {
            return ProductoService.get_tipo(idtipo);
        }
        #endregion

        #region ProductoxIngrediente
        public ProductoxIngredienteBean obtenerlistadeingredientesdeProducto(string ID)
        {
            return ProductoService.listardeingredientesdeproducto(ID);
        }

        public void Modificaringredientesdeproducto(ProductoxIngredienteBean ProdxIngred)
        {
            ProductoService.ModificarIngredientesdeproducto(ProdxIngred);
        }

        public void AñadirIngredientesdeproducto(ProductoxIngredienteBean ProdxIngred)
        {
            ProductoService.AñadirIngredientesdeproducto(ProdxIngred);
        }

        #endregion

        #region Venta 

        public List<VentaBean> buscarventas(string fecha, string idsucursal)
        {
            return ventaservice.buscarventas(fecha, idsucursal);
        }

        public List<VentaxProductoBean> obtenerlistaproductos(string idSucursal)
        {
            return ventaservice.obtenerlistaproductos(idSucursal);
        }

        public void registrarVenta(VentaBean ven)
        {
            ventaservice.registrarVenta(ven);
        }

        public VentaBean buscarventa(string id)
        {
            return ventaservice.buscarventa(id);
        }

        public void descontar(VentaBean ven)
        {
            ventaservice.descontar(ven);
        }
        #endregion


    }
}