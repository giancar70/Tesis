using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cafeteria.Models.Almacen.Notaentrada;
using Cafeteria.Models.Compra.Ordencompra;
using Cafeteria.Models.Compra.Proveedor;
using Cafeteria.Models.Administracion.Sucursal;
using Cafeteria.Models.Administracion;
using Cafeteria.Models.Compra;
using Cafeteria.Models.Almacen;
using Cafeteria.Models.Almacen.Ingrediente;

namespace Cafeteria.Controllers.Almacen
{
    public class NotaentradaController : Controller
    {

        administracionfacade admin = new administracionfacade();
        comprasfacade comprfacade = new comprasfacade();
        almacenfacade almafacade = new almacenfacade();

        public ActionResult ListarNotaEntrada(string id)
        {

            List<Notaentradabean> notas = comprfacade.listarnotasentrada(id);//idordencompra

            
            OrdencompraBean ordencompra = comprfacade.buscarOrdenes(id);
            ordencompra.notasentrada = new List<Notaentradabean>();

            for (int i = 0; i < notas.Count; i++)
            {
                ordencompra.notasentrada.Add(notas[i]);
            }

            ProveedorBean proveedor = comprfacade.BuscarProveedor(ordencompra.idProveedor);
            ordencompra.nombreProveedor = proveedor.razonSocial;
            SucursalBean suc = admin.buscarSucursal(ordencompra.idCafeteria);
            ordencompra.nombreSucursal = suc.nombre;
            return View(ordencompra);
        }


        public ActionResult RegistrarNotaEntrada(string id)
        {

            Notaentradabean notaentrada = new Notaentradabean();

            OrdencompraBean ordencompra = comprfacade.buscarOrdenes(id);
            ProveedorBean proveedor = comprfacade.BuscarProveedor(ordencompra.idProveedor);
            ordencompra.nombreProveedor = proveedor.razonSocial;

            notaentrada.idCafeteria = ordencompra.idCafeteria;
            SucursalBean suc = admin.buscarSucursal(notaentrada.idCafeteria);
            notaentrada.nombreCafeteria = suc.nombre;
            notaentrada.nombreProveedor = proveedor.razonSocial;
            notaentrada.idOrdenCompra = id;
            notaentrada.idProveedor = ordencompra.idProveedor;
            notaentrada.fechaRegistradaOrdenCompra = ordencompra.fecha;
            notaentrada.detalleNotaEntrada = new List<Notaentrada>();
            notaentrada.estado = ordencompra.estado;


            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                IngredienteBean ingre = almafacade.buscaringrediente(ordencompra.detalle[i].id);
                ordencompra.detalle[i].nombre = ingre.nombre;
            }

            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                Notaentrada notas = new Notaentrada();
                notas.cantidadsolicitada = ordencompra.detalle[i].Cantidad;
                notas.id = ordencompra.detalle[i].id;
                notas.nombre = ordencompra.detalle[i].nombre;

                notaentrada.detalleNotaEntrada.Add(notas);

            }

            List<Notaentradabean> notas2 = comprfacade.listarnotasentrada(id); // lista de notas de entrada de uan orden de compra

            for (int i = 0; i < notas2.Count; i++)
            {
                List<Notaentrada> detallenotaentrada = comprfacade.obtenernotas(notas2[i].idGuiaRemision);
                for (int j = 0; j < detallenotaentrada.Count; j++)
                {
                    for (int k = 0; k < notaentrada.detalleNotaEntrada.Count; k++)
                    {
                        if (notaentrada.detalleNotaEntrada[k].id == detallenotaentrada[j].id)
                        {
                            notaentrada.detalleNotaEntrada[k].cantidadrecibida += detallenotaentrada[j].cantidadentrante;
                        }

                    }

                }
            }
            for (int i = 0; i < notaentrada.detalleNotaEntrada.Count; i++)
            {
                notaentrada.detalleNotaEntrada[i].cantidadfaltante = notaentrada.detalleNotaEntrada[i].cantidadsolicitada - notaentrada.detalleNotaEntrada[i].cantidadrecibida;
                if (notaentrada.detalleNotaEntrada[i].cantidadfaltante == 0)
                {
                    notaentrada.detalleNotaEntrada[i].estadonota = true;
                }
            }

            return View(notaentrada);
 
        }



        [HttpPost]
        public ActionResult RegistrarNotaEntrada(Notaentradabean not) //nueva nota de entrada
        {

            for (int i = 0; i < not.detalleNotaEntrada.Count; i++)
            {
                int cantidad = not.detalleNotaEntrada[i].cantidadentrante - not.detalleNotaEntrada[i].cantidadfaltante;
                if (cantidad == 0)
                {
                    not.detalleNotaEntrada[i].estadonota = true;
                }
            }

            string estado = ""; // verificar las cantidades

            Boolean estado2 = true;
            for (int i = 0; i < not.detalleNotaEntrada.Count; i++)
            {
                estado2 = not.detalleNotaEntrada[i].estadonota && estado2;
            }

            if (estado2) estado = "Atendido"; else estado = "Parcialmente Atendido";

            almafacade.guardarnotaentrada(not, estado);
            almafacade.actualizarstock(not);//.. cambiar stock de producto
            return RedirectToAction("ListarNotaEntrada/" + not.idOrdenCompra, "Notaentrada");
        }

        public ActionResult DetallenotaEntrada(string id, string id2) //idguiaremision, idordencompra
        {
            Notaentradabean nota = new Notaentradabean();

            nota.detalleNotaEntrada = comprfacade.obtenernotas(id);
            nota.idGuiaRemision = id;
            nota.idOrdenCompra = id2;
            OrdencompraBean ordencompra = comprfacade.buscarOrdenes(id2);
            nota.idCafeteria = ordencompra.idCafeteria;
            SucursalBean suc = admin.buscarSucursal(nota.idCafeteria);
            nota.nombreCafeteria = suc.nombre;

            /*********************/


            List<Notaentradabean> notas = comprfacade.listarnotasentrada(id2);

            for (int i = 0; i < notas.Count; i++)
            {
                if (notas[i].idGuiaRemision == id)
                {
                    nota.fechaEmitida = notas[i].fechaEmitida;
                }
            }

            OrdencompraBean orden = comprfacade.buscarOrdenes(id2);

            nota.idProveedor = orden.idProveedor;

            ProveedorBean proveedor = comprfacade.BuscarProveedor(nota.idProveedor);
            nota.nombreProveedor = proveedor.razonSocial;

            for (int i = 0; i < nota.detalleNotaEntrada.Count; i++)
            {
                IngredienteBean ingre = almafacade.buscaringrediente(nota.detalleNotaEntrada[i].id);
                nota.detalleNotaEntrada[i].nombre = ingre.nombre;
            }
            return View(nota);
        }


    }
}
