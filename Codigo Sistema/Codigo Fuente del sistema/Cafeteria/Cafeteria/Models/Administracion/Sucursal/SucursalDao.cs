using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using log4net;
using System.Data.SqlClient;
using Cafeteria.Models.Administracion.Usuario;

namespace Cafeteria.Models.Administracion.Sucursal
{
    public class SucursalDao
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(BaseDatos));


        #region sucursal
        public void registrar(SucursalBean suc)
        {
            SqlConnection objDB = null;
            int i = Utils.cantidad("Cafeteria") + 1;
            string ID = "SUCU00";//8caracteres-4letras-4#
            if (i < 10) suc.id = ID + "0" + Convert.ToString(i);
            else suc.id = ID + Convert.ToString(i);
            suc.razonSocial = "Cafeteria S.A";
            suc.ruc = "45678912591";
            suc.estado = "Activo";
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "Insert into Cafeteria (idCafeteria,idDistrito,idProvincia,idDepartamento, nombre," +
                                   "razonsocial, ruc, direccion, telefono1, telefono2, estado) values " +
                                    "(@id,@distrito,@provincia,@departamento,@nombre, @razonsocial, @ruc, @direccion,@telefono1," +
                                    "@telefono2, @estado)";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", suc.id);
                Utils.agregarParametro(objQuery, "@distrito", suc.idDistrito);
                Utils.agregarParametro(objQuery, "@provincia", suc.idProvincia);
                Utils.agregarParametro(objQuery, "@departamento", suc.idDepartamento);
                Utils.agregarParametro(objQuery, "@nombre", suc.nombre);
                Utils.agregarParametro(objQuery, "@ruc", suc.ruc);
                Utils.agregarParametro(objQuery, "@razonsocial", suc.razonSocial);
                Utils.agregarParametro(objQuery, "@direccion", suc.direccion);
                Utils.agregarParametro(objQuery, "@telefono1", suc.telefono1);
                Utils.agregarParametro(objQuery, "@telefono2", suc.telefono2);
                Utils.agregarParametro(objQuery, "@estado", suc.estado);
                objQuery.ExecuteNonQuery();

                registrarAlmacen(suc.id);


            }
            catch (Exception e)
            {
                log.Error("Registrar_nuevaSucursal(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public List<SucursalBean> Listarsucursal()
        {

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<SucursalBean> listasucur = new List<SucursalBean>();
                objDB.Open();
                String strQuery = "SELECT * FROM Cafeteria";                                                                     
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        SucursalBean sucursal = new SucursalBean();
                        sucursal.id= Convert.ToString(objDataReader["idCafeteria"]);
                        sucursal.nombre = Convert.ToString(objDataReader["nombre"]);
                        sucursal.razonSocial = Convert.ToString(objDataReader["razonsocial"]);
                        sucursal.ruc = Convert.ToString(objDataReader["ruc"]);
                        sucursal.direccion = Convert.ToString(objDataReader["direccion"]);
                        sucursal.telefono1 = Convert.ToString(objDataReader["telefono1"]);
                        sucursal.telefono2 = Convert.ToString(objDataReader["telefono2"]);
                        sucursal.estado = Convert.ToString(objDataReader["estado"]);
                        listasucur.Add(sucursal);
                    }
                }

                return listasucur;
            }
            catch (Exception e)
            {
                log.Error("Lista_Sucursal(EXCEPTION): ", e);
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

        private void registrarAlmacen(string IDsucursal)
        {
            SqlConnection objDB = null;
            string IDNUEVO="";
            int i = Utils.cantidad("Cafeteria") + 1;
            string ID = "ALMA00";//8caracteres-4letras-4#
            if (i < 10) IDNUEVO = ID + "0" + Convert.ToString(i);
            else IDNUEVO = ID + Convert.ToString(i);

            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "Insert into Almacen (idCafeteria,idAlmacen) values " +
                                    "(@idCafeteria, @idAlmacen)";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idCafeteria", IDsucursal);
                Utils.agregarParametro(objQuery, "@idAlmacen", IDNUEVO);
                objQuery.ExecuteNonQuery();



            }
            catch (Exception e)
            {
                log.Error("Registrar_Almacen(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }

        public SucursalBean buscarSucursal(string Id)
        {

            SucursalBean suc = new SucursalBean();
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT * FROM Cafeteria WHERE idCafeteria = @ID";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", Id);
                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        
                        suc.id = Convert.ToString(objDataReader["idCafeteria"]);
                        suc.nombre = Convert.ToString(objDataReader["nombre"]);
                        suc.razonSocial = Convert.ToString(objDataReader["razonsocial"]);
                        suc.ruc = Convert.ToString(objDataReader["ruc"]);
                        suc.direccion = Convert.ToString(objDataReader["direccion"]);
                        suc.telefono1 = Convert.ToString(objDataReader["telefono1"]);
                        suc.telefono2 = Convert.ToString(objDataReader["telefono2"]);
                        suc.estado = Convert.ToString(objDataReader["estado"]);
                    }
                }

                return suc;
            }
            catch (Exception e)
            {
                log.Error("Lista_Sucursal(EXCEPTION): ", e);
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

        public void EliminarSucursal(string Id)
        {
            string estado = "Inactivo";
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Cafeteria SET estado=@estado " +
                                  "WHERE idCafeteria = @id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@estado", estado);
                Utils.agregarParametro(objQuery, "@id", Id);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("Dar de baja a una Sucursal(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public void ActualizarSucursal(SucursalBean suc)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Cafeteria SET nombre=@nombre, nombreAdministrador=@nomadmin, estado=@estado, telefono1=@tele, telefono2=@tele2 " +
                                  " WHERE idCafeteria = @id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@nombre", suc.nombre);
                Utils.agregarParametro(objQuery, "@nomadmin", suc.nombreAdministrador);
                Utils.agregarParametro(objQuery, "@estado", suc.estado);
                Utils.agregarParametro(objQuery, "@tele", suc.telefono1);
                Utils.agregarParametro(objQuery, "@tele2", suc.telefono2);
                Utils.agregarParametro(objQuery, "@id", suc.id);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("Actualizar_Sucursal(EXCEPTION): ", e);
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

        #region productos
        public List<sucursalproductoBean> obtenerproduct(string idsucursal)
        {
            List<sucursalproductoBean> suc = new List<sucursalproductoBean>();
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                
                objDB.Open();
                String strQuery = "SELECT * FROM Cafeteria_x_Producto where idCafeteria=@ID ";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", idsucursal);
                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        sucursalproductoBean aux = new sucursalproductoBean();
                        aux.id = Convert.ToString(objDataReader["idProducto"]);
                        aux.cantidad = Convert.ToInt32(objDataReader["cantidad"]);
                        aux.precioventa = (decimal)(objDataReader["precioventa"]);
                        aux.precioventa2 = Convert.ToString(objDataReader["precioventa"]);
                        suc.Add(aux);

                    }
                }
                return suc;
            }
            catch (Exception e)
            {
                log.Error("Productos_Sucursal(EXCEPTION): ", e);
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


        public void añadirproductos(SucursalBean suc)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < suc.listaProductos.Count; i++)
                {
                    if (suc.listaProductos[i].cantidad > 0)
                    {
                        String strQuery = "Insert into Cafeteria_x_Producto (idCafeteria,idProducto,precioventa, cantidad) values " +
                                            "(@idcafe,@idprod,@precio,@cantidad)";

                        SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                        Utils.agregarParametro(objQuery, "@idcafe", suc.id);
                        Utils.agregarParametro(objQuery, "@idprod", suc.listaProductos[i].id);
                        Utils.agregarParametro(objQuery, "@precio", suc.listaProductos[i].precioventa);
                        Utils.agregarParametro(objQuery, "@cantidad", suc.listaProductos[i].cantidad);
                        objQuery.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("Añadir IngredientesxCafeteria(EXCEPTION): ", ex);
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

        public void modificarproductos(SucursalBean suc)
        {

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < suc.listaProductos.Count; i++)
                {
                    String strQuery = "Update Cafeteria_x_Producto SET cantidad = @cantidad, precioventa=@precio  where idProducto=@idprod and idCafeteria=@idcafe ";

                    SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                    Utils.agregarParametro(objQuery, "@idcafe", suc.id);
                    Utils.agregarParametro(objQuery, "@idprod", suc.listaProductos[i].id);
                    Utils.agregarParametro(objQuery, "@precio", suc.listaProductos[i].precioventa);
                    Utils.agregarParametro(objQuery, "@cantidad", suc.listaProductos[i].cantidad);
                    objQuery.ExecuteNonQuery();

                }


            }
            catch (Exception ex)
            {
                log.Error("Modificar IngredientesxSucursal(EXCEPTION): ", ex);
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
        #endregion

        #region sucursalxpersonal
        public List<UsuarioBean> obtenerlistapersonal(string id)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<UsuarioBean> listapers = new List<UsuarioBean>();
                string estado = "Activo";
                objDB.Open();
                String strQuery = "SELECT * FROM Sucursal_x_Usuario where idCafeteria=@id and estado=@estado";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", id);
                Utils.agregarParametro(objQuery, "@estado", estado);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        UsuarioBean usuar = new UsuarioBean();
                        usuar.ID = Convert.ToString(objDataReader["idUsuario"]);
                        listapers.Add(usuar);
                    }
                }

                return listapers;
            }
            catch (Exception e)
            {
                log.Error("Lista_Sucursal_personal(EXCEPTION): ", e);
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

        public List<List<String>> obtenerlistapersonaltotal()
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<List<String>> listatotal = new List<List<String>>();
                string estado = "Activo";
                objDB.Open();
                String strQuery = "SELECT * FROM Sucursal_x_Usuario where estado=@estado";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@estado", estado);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        List<String> usuar = new List<String>();
                        String a,b;
                        a = Convert.ToString(objDataReader["idCafeteria"]); usuar.Add(a);
                        b = Convert.ToString(objDataReader["idUsuario"]); usuar.Add(b);
                        listatotal.Add(usuar);
                    }
                }

                return listatotal;
            }
            catch (Exception e)
            {
                log.Error("Lista_Sucursal_personal(EXCEPTION): ", e);
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

        public void eliminarpersonaldesucu(SucursalBean suc)
        {
            SqlConnection objDB = null;
            try
            {
                string estado = "Inactivo";
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < suc.listadepersonal.Count; i++)
                {
                    if (suc.listadepersonal[i].estadosucur)
                    {
                        String strQuery = "Delete Sucursal_x_Usuario where idCafeteria=@id and idUsuario=@idusu ";

                        SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                        Utils.agregarParametro(objQuery, "@id", suc.id);
                        Utils.agregarParametro(objQuery, "@idusu", suc.listadepersonal[i].ID);
                        objQuery.ExecuteNonQuery();
                    } 

                }
            }
            catch (Exception ex)
            {
                log.Error("Eliminar IngredientesxSucursal(EXCEPTION): ", ex);
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
        
        public void guardarnuevopersonal(SucursalBean suc)
        {
            SqlConnection objDB = null;
            try
            {
                string estado = "Activo";
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < suc.listadepersonal.Count; i++)
                {
                    if (suc.listadepersonal[i].estadosucur)
                    {
                        String strQuery = "Insert into Sucursal_x_Usuario (idCafeteria, idUsuario, estado) values (@id, @idusu, @estado)  ";

                        SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                        Utils.agregarParametro(objQuery, "@id", suc.id);
                        Utils.agregarParametro(objQuery, "@idusu", suc.listadepersonal[i].ID);
                        Utils.agregarParametro(objQuery, "@estado", estado);
                        objQuery.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error("Eliminar IngredientesxSucursal(EXCEPTION): ", ex);
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

        #endregion

    }
}