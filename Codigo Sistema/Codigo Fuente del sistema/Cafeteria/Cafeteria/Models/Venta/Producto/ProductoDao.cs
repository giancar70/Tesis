using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using log4net;
using Cafeteria.Models;

namespace Cafeteria.Models.Venta.Producto
{
    public class ProductoDao
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(BaseDatos));

        #region Producto
        public List<ProductoBean> ListarProductos(string nombre, string id_tipo)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<ProductoBean> ListaProductos = new List<ProductoBean>();
                ProductoBean Producto = new ProductoBean();
                objDB.Open();
                String strQuery = "SELECT * FROM Producto";
                if (!String.IsNullOrEmpty(nombre)) strQuery = "SELECT * FROM Producto WHERE UPPER(nombre) LIKE '%" + nombre.ToUpper() + "%'";
                if (!String.IsNullOrEmpty(id_tipo))
                {
                    if(id_tipo!="TIPO0000") strQuery = strQuery + " WHERE UPPER(tipo) LIKE '%" + id_tipo.ToUpper() + "%'";

                }
                if (!String.IsNullOrEmpty(id_tipo) && !String.IsNullOrEmpty(nombre)) strQuery = strQuery + " WHERE UPPER(tipo) LIKE '%" + id_tipo.ToUpper() + "%'" + " AND UPPER(nombre) LIKE '%" + nombre.ToUpper() + "%'"; 

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        Producto = new ProductoBean();
                        Producto.id = Convert.ToString(objDataReader["idProducto"]);
                        Producto.nombre = Convert.ToString(objDataReader["nombre"]);
                        Producto.descripcion = Convert.ToString(objDataReader["descripcion"]);
                        Producto.idTipo = Convert.ToString(objDataReader["tipo"]);
                        Producto.nombreTipo = getTipo(Producto.idTipo);
                        Producto.estado = Convert.ToString(objDataReader["estado"]);
                        ListaProductos.Add(Producto);
                    }
                }

                return ListaProductos;
            }
            catch (Exception e)
            {
                log.Error("Listar Productos(EXCEPTION): ", e);
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
        public void registrarProducto(ProductoBean produ)
        {
            SqlConnection objDB = null;
            int i = Utils.cantidad("Producto") + 1;
            string ID = "PROD00";//8caracteres-4letras-4#
            if (i < 10) produ.id = ID + "0" + Convert.ToString(i);
            else produ.id = ID + Convert.ToString(i);
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "Insert into Producto (idProducto,nombre, descripcion, tipo, estado) values " +
                                    "(@id,@nombre, @descripcion,@tipo, @estado)";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", produ.id);
                Utils.agregarParametro(objQuery, "@nombre", produ.nombre);
                Utils.agregarParametro(objQuery, "@descripcion", produ.descripcion);
                Utils.agregarParametro(objQuery, "@tipo", produ.idTipo);
                Utils.agregarParametro(objQuery, "@estado", produ.estado);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("RegistrarProducto(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public ProductoBean BuscarProducto(string id)
        {

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                ProductoBean Producto = null;

                objDB.Open();
                String strQuery = "SELECT * FROM Producto WHERE idProducto = @ID";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", id);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    Producto = new ProductoBean();
                    Producto.id = Convert.ToString(objDataReader["idProducto"]);
                    Producto.nombre = Convert.ToString(objDataReader["nombre"]);
                    Producto.descripcion = Convert.ToString(objDataReader["descripcion"]);
                    Producto.idTipo = Convert.ToString(objDataReader["tipo"]);
                    Producto.nombreTipo = getTipo(Producto.idTipo);
                    Producto.estado = Convert.ToString(objDataReader["estado"]);
                }
                return Producto;
            }
            catch (Exception ex)
            {
                log.Error("getProducto(EXCEPTION): ", ex);
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
        public void ActualizarProducto(ProductoBean produ)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Producto SET nombre=@nombre, descripcion=@descripcion, estado=@estado, tipo=@tipo" +
                                  " WHERE idProducto = @id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@nombre", produ.nombre);
                Utils.agregarParametro(objQuery, "@descripcion", produ.descripcion);
                Utils.agregarParametro(objQuery, "@estado", produ.estado);
                Utils.agregarParametro(objQuery, "@tipo", produ.idTipo);
                Utils.agregarParametro(objQuery, "@id", produ.id);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("Actualizar_Producto(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public void EliminarProducto(string ID)
        {
            string estado = "INACTIVO";
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Producto SET estado=@estado " +
                                  "WHERE idProducto = @id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@estado", estado);
                Utils.agregarParametro(objQuery, "@id", ID);
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

        public string getTipo(string Id_tipo)
        {
            string nombre_tipo = null;
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                

                objDB.Open();
                String strQuery = "SELECT (nombre) FROM Tipo WHERE id = @ID";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", Id_tipo);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    
                     nombre_tipo= Convert.ToString(objDataReader["nombre"]);
                    
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
        #endregion

        #region IngredientexProducto

        public ProductoxIngredienteBean  listaIngredientes(string ID)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                ProductoxIngredienteBean produc = null;

                objDB.Open();
                String strQuery = "SELECT * FROM Producto_x_Ingrediente WHERE idProducto = @ID";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", ID);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                produc = new ProductoxIngredienteBean();
                produc.listaIngredientes = new List<ProductoxIngrediente>();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        ProductoxIngrediente aux = new ProductoxIngrediente();
                        aux.id = Convert.ToString(objDataReader["idIngrediente"]);
                        aux.cantidad = (int)objDataReader["cantidad"];
                        aux.medida = (string)objDataReader["unidaddemedida"];
                        produc.listaIngredientes.Add(aux);
                    }
                }
                return produc;
            }
            catch (Exception ex)
            {
                log.Error("Get_ListadeIngredientesxProducto(EXCEPTION): ", ex);
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

        public void AñadirIngredientes(ProductoxIngredienteBean Producxingre)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < Producxingre.listaIngredientes.Count; i++)
                {
                    if (Producxingre.listaIngredientes[i].cantidad > 0)
                    {
                        String strQuery = "Insert into Producto_x_Ingrediente (idProducto,idIngrediente,cantidad, unidaddemedida) values " +
                                            "(@idproducto,@idingrediente,@cantidad,@unidad)";

                        SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                        Utils.agregarParametro(objQuery, "@idproducto", Producxingre.idProducto);
                        Utils.agregarParametro(objQuery, "@idingrediente", Producxingre.listaIngredientes[i].id);
                        Utils.agregarParametro(objQuery, "@cantidad", Producxingre.listaIngredientes[i].cantidad);
                        Utils.agregarParametro(objQuery, "@unidad", Producxingre.listaIngredientes[i].medida);
                        objQuery.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("Añadir IngredientesxProducto(EXCEPTION): ", ex);
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

        public void ModificarIngredientes(ProductoxIngredienteBean Producxingre)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < Producxingre.listaIngredientes.Count; i++)
                {
                    String strQuery = "Update Producto_x_Ingrediente SET cantidad = @cantidad, unidaddemedida=@unidad  where idProducto=@idproducto and idIngrediente=@idingrediente ";

                    SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                    Utils.agregarParametro(objQuery, "@idproducto", Producxingre.idProducto);
                    Utils.agregarParametro(objQuery, "@idingrediente", Producxingre.listaIngredientes[i].id);
                    Utils.agregarParametro(objQuery, "@cantidad", Producxingre.listaIngredientes[i].cantidad);
                    Utils.agregarParametro(objQuery, "@unidad", Producxingre.listaIngredientes[i].medida);
                    objQuery.ExecuteNonQuery();

                }


            }
            catch (Exception ex)
            {
                log.Error("Modificar IngredientesxProducto(EXCEPTION): ", ex);
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