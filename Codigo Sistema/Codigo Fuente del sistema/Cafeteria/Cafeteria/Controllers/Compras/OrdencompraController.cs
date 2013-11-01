using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafeteria.Models.Compra.Ordencompra;
using Cafeteria.Models.Administracion.Sucursal;
using Cafeteria.Models.Administracion;
using Cafeteria.Models.Compra.Proveedor;
using Cafeteria.Models.Compra;
using Cafeteria.Models.Almacen.Ingrediente;
using Cafeteria.Models.Almacen;
using Cafeteria.Models.Almacen.Notaentrada;
using Cafeteria.Models;


namespace Cafeteria.Controllers.Compras
{
    public class OrdencompraController : Controller
    {
        
        administracionfacade admin = new administracionfacade();
        comprasfacade comprfacade = new comprasfacade();
        almacenfacade almafacade = new almacenfacade();

        public ActionResult Index()
        {
            List<SucursalBean> suc = admin.listasucursal();
            return View(suc);
        }

        [HttpPost]
        public JsonResult listarordenes(String user, String pass)
        {
            Utils util = new Utils();
            string id2 = util.buscarsucursal(user);
            return Json(id2);
        }

        public ActionResult listadeordenes(String id)
        {
            List<OrdencompraBean> aux = new List<OrdencompraBean>();
            List<OrdencompraBean> orden = comprfacade.buscarOrdenescompra("", "", "");// new List<OrdencompraBean>();//comprfacade.buscarOrdenes(nombre, fecha1, fecha2);

            for (int i = 0; i < orden.Count; i++)
            {
                if (orden[i].idCafeteria.CompareTo(id) == 0)
                {
                    aux.Add(orden[i]);
                }
            }

            for (int i = 0; i < aux.Count; i++)
            {
                if (aux[i].estado == "Tramite" || aux[i].estado == "Cancelado") aux[i].estado2 = true;
                else aux[i].estado2 = false;
                SucursalBean suc = admin.buscarSucursal(aux[i].idCafeteria);
                aux[i].nombreSucursal = suc.nombre;
            }

            return View(aux);

        }

        public ActionResult listadeordenes2(String id)
        {
            List<OrdencompraBean> aux = new List<OrdencompraBean>();
            List<OrdencompraBean> orden = comprfacade.buscarOrdenescompra("", "", "");// new List<OrdencompraBean>();//comprfacade.buscarOrdenes(nombre, fecha1, fecha2);

            for (int i = 0; i < orden.Count; i++)
            {
                if (orden[i].idCafeteria.CompareTo(id) == 0)
                {
                    aux.Add(orden[i]);
                }
            }

            for (int i = 0; i < aux.Count; i++)
            {
                if (aux[i].estado == "Tramite" || aux[i].estado == "Cancelado") aux[i].estado2 = true;
                else aux[i].estado2 = false;
                SucursalBean suc = admin.buscarSucursal(aux[i].idCafeteria);
                aux[i].nombreSucursal = suc.nombre;
            }

            return View(aux);

        }
        

        #region Buscar

        public ActionResult Buscar()
        {
            PagoProveedorBean pagoProveedor = new PagoProveedorBean();
            return View(pagoProveedor);
        }

        [HttpPost]
        public ActionResult Buscar(PagoProveedorBean gg)
        {
            ProveedorBean prove = comprfacade.BuscarProveedor(gg.ID);
            string nombre = prove.razonSocial;

            return RedirectToAction("Buscar2", new { idprov = gg.ID });
        }


        public ActionResult Buscar2(string idprov)
        {
            string fecha1 = "";
            string fecha2 = "";


            List<OrdencompraBean> orden = comprfacade.buscarOrdenescompra(idprov, fecha1, fecha2);// new List<OrdencompraBean>();//comprfacade.buscarOrdenes(nombre, fecha1, fecha2);

            for (int i = 0; i < orden.Count; i++)
            {
                if (orden[i].estado == "Tramite" || orden[i].estado == "Cancelado") orden[i].estado2 = true;
                else orden[i].estado2 = false;
                SucursalBean suc = admin.buscarSucursal(orden[i].idCafeteria);
                orden[i].nombreSucursal = suc.nombre;
            }

            return View(orden);
        }

        #endregion


        #region crearOrdenCompra
        public ActionResult Registrar(string id)
        {
            OrdencompraBean ordencompra = new OrdencompraBean();
            ordencompra.idCafeteria = id;
            return View(ordencompra);
        } 


        [HttpPost]
        public ActionResult Registrar(OrdencompraBean orden)
        {
            //int ID = Convert.ToInt32(ordenCompra.idProv);
            //orden.idCafeteria = orden.idCafeteria;
            return RedirectToAction("Registrar2", new { idproveedor = orden.idProveedor, idsucursal = orden.idCafeteria });
        }

        public ViewResult Registrar2(string idproveedor, string idsucursal) //registrar orden compra.......idproveedor y id sucursal
        {
            OrdenProducto prod = new OrdenProducto();
            ProveedorBean prov = comprfacade.BuscarProveedor(idproveedor);


            int cantidad = 0;
            ProveedorxIngredienteBean productosprov = comprfacade.obtenerlistadeingredientes(idproveedor); // de la tabla productoxpreoveedor
            string idalmacen = comprfacade.obteneralmacen(idsucursal);
            IngredienteXalmacenBean ingredientAlmace = almafacade.obtenerlistadAlmacen(idalmacen); // de la tabla productoxalmacen


            List<Producto> produ = new List<Producto>();

            for (int i = 0; i < ingredientAlmace.listProdAlmacen.Count; i++)
            {
                for (int j = 0; j < productosprov.listadeIngredientesProveedor.Count; j++)
                {
                    if (ingredientAlmace.listProdAlmacen[i].id == productosprov.listadeIngredientesProveedor[j].id)
                    {
                        Producto produc = new Producto();
                        produc.idproducto = ingredientAlmace.listProdAlmacen[i].id;
                        produc.Nombre = almafacade.getnombreingrediente(ingredientAlmace.listProdAlmacen[i].id);
                        produc.precio = productosprov.listadeIngredientesProveedor[j].precio;
                        produc.stockActual = ingredientAlmace.listProdAlmacen[i].stockactual;
                        produc.stockMinimo = ingredientAlmace.listProdAlmacen[i].stockminimo;
                        produc.stockMaximo = ingredientAlmace.listProdAlmacen[i].stockmaximo;
                        if (produc.stockActual <= produc.stockMinimo) { produc.estado = true; cantidad++; }
                        else { produc.estado = false; }
                        produ.Add(produc);
                    }

                }
            }

            prod.listaProducto = produ;
            prod.proveedor = prov.razonSocial;
            prod.idproveedor = idproveedor;//idproveedor
            prod.idcafeteria = idsucursal;
            SucursalBean suc = admin.buscarSucursal(idsucursal);//.getHotel(idhotel);
            prod.nombrecafeteria = suc.nombre;

            //Boolean est = prod.listaProducto[0].estado;
            if (cantidad > 0) prod.estado2 = true; else prod.estado2 = false;
            return View(prod);
        }


        [HttpPost]
        public ActionResult Registrar2(OrdenProducto producto)
        {


            for (int i = 0; i < producto.listaProducto.Count; i++)
            {
                if (producto.listaProducto[i].cantidad > 0)
                {
                    producto.listaProducto[i].estadoguardar = true;
                    producto.listaProducto[i].precio= comprfacade.obtenerPrecio(producto.listaProducto[i].idproducto, producto.idproveedor);

                }
                else
                {
                    producto.listaProducto[i].estadoguardar = false;
                    producto.listaProducto[i].precio = comprfacade.obtenerPrecio(producto.listaProducto[i].idproducto, producto.idproveedor);
                }
            }

            comprfacade.GuardarOrdenCompra(producto);
            return RedirectToAction("Index","Home");
        }

        #endregion



        #region Edit
       
        #endregion

        #region Detalle

        
        public ActionResult DetalleOrdenC(string id) //id orden compra
        {
            OrdencompraBean ordencompra = comprfacade.buscarOrdenes(id);
            ProveedorBean proveedor = comprfacade.BuscarProveedor(ordencompra.idProveedor);
            ordencompra.nombreProveedor = proveedor.razonSocial;

            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                IngredienteBean ingre = almafacade.buscaringrediente(ordencompra.detalle[i].id);
                ordencompra.detalle[i].nombre = ingre.nombre;
            }
            ProveedorxIngredienteBean productos = comprfacade.obtenerlistadeingredientes(ordencompra.idProveedor);
            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                for (int j = 0; j < productos.listadeIngredientesProveedor.Count; j++)
                {
                    if (ordencompra.detalle[i].id == productos.listadeIngredientesProveedor[j].id)
                    {
                        ordencompra.detalle[i].preciounitario = productos.listadeIngredientesProveedor[j].precio;
                    }
                }
            }

            List<Notaentradabean> notas = comprfacade.listarnotasentrada(id);
            for (int i = 0; i < notas.Count; i++)
            {
                string idguiaremision = notas[i].idGuiaRemision;
                List<Notaentrada> not2 = comprfacade.obtenernotas(idguiaremision);
                for (int j = 0; j < not2.Count; j++)
                {
                    for (int k = 0; k < ordencompra.detalle.Count(); k++)
                    {
                        if (ordencompra.detalle[k].id == not2[j].id)
                        {
                            ordencompra.detalle[k].Cantidadentrante += not2[j].cantidadentrante;
                        }
                    }
                }
            }

            SucursalBean suc = admin.buscarSucursal(ordencompra.idCafeteria);
            ordencompra.nombreSucursal = suc.nombre;
            return View(ordencompra);
        }

        #endregion

        #region Modificar ordencompra
        public ActionResult ModificarOrdenC(string id) //idordencompra
        {
            OrdencompraBean ordencompra = comprfacade.buscarOrdenes(id);//comprfacade.buscarOrdenes(id);
            ProveedorBean proveedor = comprfacade.BuscarProveedor(ordencompra.idProveedor);
            ordencompra.nombreProveedor = proveedor.razonSocial;

            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                IngredienteBean ingre = almafacade.buscaringrediente(ordencompra.detalle[i].id);
                ordencompra.detalle[i].nombre = ingre.nombre;
            }
            ProveedorxIngredienteBean productos = comprfacade.obtenerlistadeingredientes(ordencompra.idProveedor);
            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                for (int j = 0; j < productos.listadeIngredientesProveedor.Count; j++)
                {
                    if (ordencompra.detalle[i].id == productos.listadeIngredientesProveedor[j].id)
                    {
                        ordencompra.detalle[i].preciounitario = productos.listadeIngredientesProveedor[j].precio;
                    }
                }
            }
            SucursalBean suc = admin.buscarSucursal(ordencompra.idCafeteria);
            ordencompra.nombreSucursal = suc.nombre;
            return View(ordencompra);

        }


        public ActionResult GuardarestadoOrdenC(OrdencompraBean orden)
        {
            // guarda el estado de la orden de compra a registrado o cancelado
            comprfacade.modificarestadoordencompra(orden.idOrdenCompra, orden.estado);

            //mandar correo electronico
            return RedirectToAction("Index", "Home");
        }

        #endregion


    }
}
