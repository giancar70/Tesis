using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using log4net;
using System.Data.SqlClient;

namespace Cafeteria.Models.Venta.Venta
{
    public class VentaDao
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(BaseDatos));

        public List<VentaBean> buscarventas(string fecha, string idsucursal)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<VentaBean> Listadeventas = new List<VentaBean>();
                VentaBean venta = new VentaBean();
                objDB.Open();
                String strQuery = "SELECT * FROM Venta";
                if (!String.IsNullOrEmpty(fecha) && String.IsNullOrEmpty(idsucursal)) strQuery = "SELECT * FROM Venta WHERE UPPER(fechaventa) LIKE '%" + fecha.ToUpper() + "%'";
                if (!String.IsNullOrEmpty(idsucursal) && String.IsNullOrEmpty(fecha))
                {
                    if (idsucursal != "SUCU0000") strQuery = strQuery + " WHERE UPPER(idCafeteria) LIKE '%" + idsucursal.ToUpper() + "%'";

                }
                if (!String.IsNullOrEmpty(idsucursal) && !String.IsNullOrEmpty(fecha)) strQuery = strQuery + " WHERE UPPER(idCafeteria) LIKE '%" + idsucursal.ToUpper() + "%'" + " AND UPPER(fechaventa) LIKE '%" + fecha.ToUpper() + "%'";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        venta = new VentaBean();
                        venta.idventa = Convert.ToString(objDataReader["idVenta"]);
                        venta.idSucursal = Convert.ToString(objDataReader["idCafeteria"]);
                        venta.fecharegistro = Convert.ToDateTime(objDataReader["fechaventa"]);
                        venta.totalventa = Convert.ToDecimal(objDataReader["montototal"]);
                        venta.nombresucursal = this.getnombre(venta.idSucursal);
                        Listadeventas.Add(venta);
                    }
                }

                return Listadeventas;
            }
            catch (Exception e)
            {
                log.Error("Listar_Ventas_realizadas(EXCEPTION): ", e);
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

        private string getnombre(string idcafeteria)
        {
            string nombre_tipo = null;
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);


                objDB.Open();
                String strQuery = "SELECT (nombre) FROM Cafeteria WHERE idCafeteria = @ID";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", idcafeteria);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();

                    nombre_tipo = Convert.ToString(objDataReader["nombre"]);

                }
                return nombre_tipo;
            }
            catch (Exception ex)
            {
                log.Error("GetTipo(EXCEPTION): ", ex);
                throw ex;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }


        public List<VentaxProductoBean> obtenerlistaproductos(string idSucursal)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<VentaxProductoBean> Listadeproductos = new List<VentaxProductoBean>();
                VentaxProductoBean prod = new VentaxProductoBean();
                objDB.Open();
                String strQuery = "SELECT * FROM Cafeteria_x_Producto WHERE UPPER(idCafeteria) LIKE '%" + idSucursal.ToUpper() + "%'"; 

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        prod = new VentaxProductoBean();
                        prod.id = Convert.ToString(objDataReader["idProducto"]);
                        prod.preciouniario = Convert.ToDecimal(objDataReader["precioventa"]);
                        prod.cantidad = Convert.ToInt32(objDataReader["cantidad"]);
                       
                        Listadeproductos.Add(prod);
                    }
                }

                return Listadeproductos;
            }
            catch (Exception e)
            {
                log.Error("Lista_productos_sucursal(EXCEPTION): ", e);
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


        public void registrarVenta(VentaBean ven)
        {

            SqlConnection objDB = null;
            int i = Utils.cantidad("Venta") + 1;
            string ID = "VENT00";//8caracteres-4letras-4#
            if (i < 10) ven.idventa = ID + "0" + Convert.ToString(i);
            else ven.idventa = ID + Convert.ToString(i);
            string estado = "Registrado";
            try
            {
                if (Convert.ToDecimal(ven.totalventa2) > 0)
                {
                    objDB = new SqlConnection(cadenaDB);
                    objDB.Open();
                    String strQuery = "Insert into Venta (idVenta,idCafeteria,fechaventa,estado, montototal, nombrecliente, dnicliente ) values " +
                                        "(@id,@idCafeteria,@fecha,@estado,@montototal, @nomb, @dni)";

                    SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                    Utils.agregarParametro(objQuery, "@id", ven.idventa);
                    Utils.agregarParametro(objQuery, "@idCafeteria", ven.idSucursal);
                    Utils.agregarParametro(objQuery, "@fecha", ven.fecharegistro);
                    Utils.agregarParametro(objQuery, "@estado", estado);
                    Utils.agregarParametro(objQuery, "@montototal", Convert.ToDecimal(ven.totalventa2));
                    Utils.agregarParametro(objQuery, "@nomb", ven.nombrepersona);
                    Utils.agregarParametro(objQuery, "@dni", ven.dnipersona);
                    objQuery.ExecuteNonQuery();
                    this.registrardetalle(ven);
                }

            }
            catch (Exception e)
            {
                log.Error("Registrar_venta(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
            
        }

        private void registrardetalle(VentaBean ven)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < ven.listaproductos.Count; i++)
                {
                    if (ven.listaproductos[i].cantidadsolicitada>0)
                    {

                        String strQuery = "Insert Into  VentaDetalle (idVenta,idProducto,cantidad,subtotal) values(@idven, @idprod, @cant,@precio)";
                        SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                        BaseDatos.agregarParametro(objQuery, "@idven", ven.idventa);
                        BaseDatos.agregarParametro(objQuery, "@idprod", ven.listaproductos[i].id);
                        BaseDatos.agregarParametro(objQuery, "@cant", ven.listaproductos[i].cantidadsolicitada);
                        BaseDatos.agregarParametro(objQuery, "@precio", Convert.ToDecimal(ven.listaproductos[i].preciosubtotal));
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

        public VentaBean buscarventa(string idventa)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT * FROM Venta where idVenta= @id ";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objQuery, "@id", idventa);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                VentaBean venta = new VentaBean();

                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {

                        venta.idventa = Convert.ToString(objDataReader["idVenta"]);
                        venta.idSucursal = Convert.ToString(objDataReader["idCafeteria"]);
                        venta.fecharegistro = Convert.ToDateTime(objDataReader["fechaventa"]);
                        venta.totalventa = (decimal)(objDataReader["montototal"]);
                        venta.nombrepersona = Convert.ToString(objDataReader["nombrecliente"]);
                        venta.dnipersona = Convert.ToString(objDataReader["dnicliente"]);
                    }
                }
                venta.listaproductos = this.retornadetalle(idventa);

                return venta;
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

        public List<VentaxProductoBean> retornadetalle(string idventa)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<VentaxProductoBean> Listaproduc = new List<VentaxProductoBean>();
                objDB.Open();
                String strQuery = "SELECT * FROM VentaDetalle where idVenta= @id ";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objQuery, "@id", idventa);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {

                        VentaxProductoBean detalle = new VentaxProductoBean();
                        detalle.id = Convert.ToString(objDataReader["idProducto"]);
                        detalle.cantidadsolicitada = Convert.ToInt32(objDataReader["cantidad"]);
                        detalle.subtotal = Convert.ToDecimal(objDataReader["subtotal"]);

                        Listaproduc.Add(detalle);
                    }
                }

                return Listaproduc;
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

        public void descontar(VentaBean ven)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < ven.listaproductos.Count; i++)
                {
                    if (ven.listaproductos[i].cantidadsolicitada > 0)
                    {

                        String strQuery = "Update  Cafeteria_x_Producto set cantidad=@cantidad where idProducto=@idproducto and idCafeteria=@idcafete";
                        SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                        BaseDatos.agregarParametro(objQuery, "@idcafete", ven.idSucursal);
                        BaseDatos.agregarParametro(objQuery, "@idproducto", ven.listaproductos[i].id);
                        int cant = ven.listaproductos[i].cantidad - ven.listaproductos[i].cantidadsolicitada;
                        BaseDatos.agregarParametro(objQuery, "@cantidad", cant);
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


    }

}