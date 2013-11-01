using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;
using log4net;
using Cafeteria.Models.Almacen.Notaentrada;

namespace Cafeteria.Models.Compra.Ordencompra
{
    public class OrdencompraDao
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(BaseDatos));


        #region Ordencompra
        public string obteneralmacen(string idsucursal)
        {
            string idalmacen = "";

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT * FROM Almacen WHERE idCafeteria = @ID";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objQuery, "@ID", idsucursal);

                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        idalmacen =Convert.ToString(objDataReader["idAlmacen"]);

                    }
                }

                return idalmacen;
            }
            catch (Exception e)
            {
                log.Error("getalmacen(EXCEPTION):  ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

            


        }

        public void GuardarOrdenCompra(OrdenProducto producto)
        {

            int cantidad = 0;
            for (int i = 0; i < producto.listaProducto.Count; i++)
            {
                if (producto.listaProducto[i].estadoguardar) cantidad++;
            }
            try
            {
                if (cantidad > 0)
                {
                    int m = Utils.cantidad("Ordencompra") + 1;
                    string ID = "ORDE00";//8caracteres-4letras-4#
                    if (m < 10) producto.idOrdencompra = ID + "0" + Convert.ToString(m);
                    else producto.idOrdencompra = ID + Convert.ToString(m);
                    string estado = "Tramite";
                    
                    decimal total = 0; // decimal

                    for (int i = 0; i < producto.listaProducto.Count; i++)
                    {
                        if (producto.listaProducto[i].estadoguardar)
                        {
                            int valor = producto.listaProducto.ElementAt(i).cantidad;
                            decimal precio = producto.listaProducto.ElementAt(i).precio; // decimal
                            total += (valor * precio);
                        }
                    }
                    SqlConnection objDB = null;
                    objDB = new SqlConnection(cadenaDB);
                    objDB.Open();
                    String strQuery = "INSERT INTO Ordencompra (fechaemitida, estado, idOrdencompra, preciototal, idProveedor, idSucursal) Values (GETDATE(),@estado,@id,@total, @idprov,@idcafe)";
                    SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                    BaseDatos.agregarParametro(objQuery, "@estado", estado);
                    BaseDatos.agregarParametro(objQuery, "@id", producto.idOrdencompra);
                    BaseDatos.agregarParametro(objQuery, "@total", total);
                    BaseDatos.agregarParametro(objQuery, "@idprov", producto.idproveedor);
                    BaseDatos.agregarParametro(objQuery, "@idcafe", producto.idcafeteria);
                    objQuery.ExecuteNonQuery();


                    this.guardardetalleordencompra(producto);
                }

            }
            catch (Exception ex)
            {
                log.Error("GuardarOrdenCompra(EXCEPTION): ", ex);
                throw ex;
            }
        }


        public void guardardetalleordencompra(OrdenProducto producto)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < producto.listaProducto.Count; i++)
                {
                    if (producto.listaProducto[i].estadoguardar)
                    {
                        decimal precio = 0; // decimal
                        Producto prod = producto.listaProducto.ElementAt(i);
                        precio = (prod.precio * prod.cantidad);

                        String strQuery = "Insert Into  OrdenCompraDetalle (idIngrediente,idOrdencompra,cantidad,precio) values(@ID, @idorden, @cant,@precio)";
                        SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                        BaseDatos.agregarParametro(objQuery, "@ID", prod.idproducto);
                        BaseDatos.agregarParametro(objQuery, "@idorden", producto.idOrdencompra);
                        BaseDatos.agregarParametro(objQuery, "@cant", prod.cantidad);
                        BaseDatos.agregarParametro(objQuery, "@precio", precio);
                        objQuery.ExecuteNonQuery();
                    }
                }

               
            }
            catch (Exception e)
            {
                log.Error("getalmacen(EXCEPTION):  ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }


        public List<OrdencompraBean> buscarOrdenescompra(string idprov, string fecha1, string fecha2)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<OrdencompraBean> Listaordenes = new List<OrdencompraBean>();
                objDB.Open();
                String strQuery = "SELECT * FROM Ordencompra";
                if (!String.IsNullOrEmpty(idprov)) strQuery = strQuery + " WHERE UPPER(idProveedor) LIKE '%" + idprov.ToUpper() + "%'";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);

                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        OrdencompraBean orden = new OrdencompraBean();
                        orden.idOrdenCompra = Convert.ToString(objDataReader["idOrdencompra"]);
                        orden.estado = Convert.ToString(objDataReader["estado"]);
                        orden.fecha = Convert.ToString(objDataReader["fechaemitida"]);
                        orden.idCafeteria = Convert.ToString(objDataReader["idSucursal"]);
                        orden.precioTotal = Convert.ToDecimal(objDataReader["preciototal"]);
                        Listaordenes.Add(orden);
                    }
                }

                return Listaordenes;
            }
            catch (Exception e)
            {
                log.Error("Lista de ordenes de compra(EXCEPTION): ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }


        }


        #endregion

        public decimal obtenerPrecio(string idproducto, string idproveedor)
        {
            decimal precio = 0;

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT * FROM Proveedor_x_Producto WHERE idProveedor = @ID1 and idIngrediente=@ID2 ";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objQuery, "@ID1", idproveedor);
                BaseDatos.agregarParametro(objQuery, "@ID2", idproducto);

                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        precio = Convert.ToDecimal(objDataReader["precio"]);

                    }
                }

                return precio;
            }
            catch (Exception e)
            {
                log.Error("getalmacen(EXCEPTION):  ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }


        #region notaentrada

        public OrdencompraBean buscarOrdenes(string ordencompra)
        {

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                //List<OrdencompraBean> Listaordenes = new List<OrdencompraBean>();
                objDB.Open();
                String strQuery = "SELECT * FROM Ordencompra where idOrdencompra= @id ";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objQuery, "@id", ordencompra);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                OrdencompraBean orden = new OrdencompraBean();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        
                        orden.idOrdenCompra = Convert.ToString(objDataReader["idOrdencompra"]);
                        orden.estado = Convert.ToString(objDataReader["estado"]);
                        orden.fecha = Convert.ToString(objDataReader["fechaemitida"]);
                        orden.idCafeteria = Convert.ToString(objDataReader["idSucursal"]);
                        orden.precioTotal = Convert.ToDecimal(objDataReader["preciototal"]);
                        orden.idProveedor = Convert.ToString(objDataReader["idProveedor"]);
                        //Listaordenes.Add(orden);
                    }
                }
                orden.detalle = this.retornadetalle(ordencompra);

                return orden;
            }
            catch (Exception e)
            {
                log.Error("Lista de ordenes de compra(EXCEPTION): ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
         
        }

        public List<detalleordencompra> retornadetalle(string id)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<detalleordencompra> Listaordenes = new List<detalleordencompra>();
                objDB.Open();
                String strQuery = "SELECT * FROM OrdenCompraDetalle where idOrdencompra= @id ";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objQuery, "@id", id);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                //OrdencompraBean orden = new OrdencompraBean();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {

                        detalleordencompra detalle = new detalleordencompra();
                        //detalle.id = (string)dataReader2["idIngrediente"];
                        //detalle.Cantidad = (int)dataReader2["cantidad"];
                        //detalle.precio = (decimal)dataReader2["precio"];

                        detalle.id = Convert.ToString(objDataReader["idIngrediente"]);
                        detalle.Cantidad = Convert.ToInt32(objDataReader["cantidad"]);
                        detalle.precio = Convert.ToDecimal(objDataReader["precio"]);
                        //orden.idCafeteria = Convert.ToString(objDataReader["idSucursal"]);
                        //orden.precioTotal = Convert.ToDecimal(objDataReader["preciototal"]);
                        Listaordenes.Add(detalle);
                    }
                }
               // orden.detalle = this.retornadetalle(ordencompra);

                return Listaordenes;
            }
            catch (Exception e)
            {
                log.Error("Lista de ordenes de compra(EXCEPTION): ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }

        public List<Notaentradabean> listarnotasentrada(string idordencompra)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<Notaentradabean> Listaordenes = new List<Notaentradabean>();
                objDB.Open();
                String strQuery = "SELECT * FROM Notaentrada where idOrdencompra= @id ";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objQuery, "@id", idordencompra);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {

                        Notaentradabean orden = new Notaentradabean();

                        orden.idGuiaRemision = Convert.ToString(objDataReader["idNotaentrada"]);
                        orden.idOrdenCompra = Convert.ToString(objDataReader["idOrdencompra"]);
                        orden.fechaEmitida = Convert.ToString(objDataReader["fechaEntrega"]);
                        Listaordenes.Add(orden);
                    }
                }

                return Listaordenes;
            }
            catch (Exception e)
            {
                log.Error("Lista de ordenes de compra(EXCEPTION): ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            } 

        }


        public List<Notaentrada> obtenernotas(string idguiaremision)
        {

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<Notaentrada> Listaordenes = new List<Notaentrada>();
                objDB.Open();
                String strQuery = "SELECT * FROM NotaEntradaDetalle where idNotaentrada= @id ";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objQuery, "@id", idguiaremision);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {

                        Notaentrada nota = new Notaentrada();// orden = new Notaentradabean();
                        nota.id = Convert.ToString(objDataReader["idIngrediente"]);
                        nota.cantidadentrante = Convert.ToInt32(objDataReader["cantidadentrante"]);
                        Listaordenes.Add(nota);
                    }
                }

                return Listaordenes;
            }
            catch (Exception e)
            {
                log.Error("Lista de ordenes de compra(EXCEPTION): ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }
                
        #endregion


        public void modificarestadoordencompra(string idOrdenCompra, string estado)
        {
            //string estado = "INACTIVO";
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Ordencompra SET estado=@estado " +
                                  "WHERE idOrdencompra = @id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@estado", estado);
                Utils.agregarParametro(objQuery, "@id", idOrdenCompra);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("EliminarProducto(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }


        }



    }
}