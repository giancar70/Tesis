using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;

using System.Data;
using log4net;

namespace Cafeteria.Models.Almacen.Ingrediente
{
    public class Ingredientedao
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(BaseDatos));

        #region Ingrediente

        public List<IngredienteBean> ListarIngrediente(string nombre)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<IngredienteBean> ListaIngre = new List<IngredienteBean>();
                objDB.Open();
                String strQuery = "SELECT * FROM Ingrediente";
                if (!String.IsNullOrEmpty(nombre)) strQuery = strQuery + " WHERE UPPER(nombre) LIKE '%" + nombre.ToUpper() + "%'";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                   while (objDataReader.Read())
                   {
                        IngredienteBean ingrediente = new IngredienteBean();
                        ingrediente.id = Convert.ToString(objDataReader["idIngrediente"]);
                        ingrediente.nombre = Convert.ToString(objDataReader["nombre"]);
                        ingrediente.descripcion = Convert.ToString(objDataReader["descripcion"]);
                        ingrediente.estado = Convert.ToString(objDataReader["estado"]);    
                        ListaIngre.Add(ingrediente);
                   }
                }

                return ListaIngre;
             }
             catch (Exception e)
             {
                log.Error("ListaIngredientes(EXCEPTION): ", e);
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
        public void registraringrediente(IngredienteBean prod)
        {
            SqlConnection objDB = null;
            int i = Utils.cantidad("Ingrediente")+1;
            string ID="INGR00";//8caracteres-4letras-4#
			if (i<10) prod.id=ID+"0"+Convert.ToString(i);
				else prod.id=ID+Convert.ToString(i);
			try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "Insert into Ingrediente (idIngrediente,nombre, descripcion, estado) values " +
                                    "(@id,@nombre, @descripcion, @estado)";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", prod.id);
                Utils.agregarParametro(objQuery, "@nombre", prod.nombre);
                Utils.agregarParametro(objQuery, "@descripcion", prod.descripcion);
                Utils.agregarParametro(objQuery, "@estado", prod.estado);
                objQuery.ExecuteNonQuery();
                this.insertaralmacenxingrediente(prod.id);
                
            }
            catch (Exception e)
            {
                log.Error("registrarIngrediente(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                    
                }
            }
        }

        private void insertaralmacenxingrediente(string idingre)
        {
            SqlConnection objDB = null;
            List<String> listalmacenes=this.listadealmacenes();
            int stockact = 100, stockminimo=50, stockmaximo=500;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                
                objDB.Open();
                for (int i = 0; i < listalmacenes.Count(); i++)
                {
                    String strQuery = "Insert into Almacen_x_Producto (idAlmacen, idIngrediente, stockactual, stockminimo, stockmaximo) values  " +
                                                        " ( @idalma, @ID, @stocka, @stockmin, @stockmaximo )";
                    SqlCommand objquery = new SqlCommand(strQuery, objDB);
                    BaseDatos.agregarParametro(objquery, "@ID", idingre);
                    BaseDatos.agregarParametro(objquery, "@idalma", listalmacenes[i]);
                    BaseDatos.agregarParametro(objquery, "@stocka", stockact);
                    BaseDatos.agregarParametro(objquery, "@stockmin", stockminimo);
                    BaseDatos.agregarParametro(objquery, "@stockmaximo", stockmaximo);
                    objquery.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                log.Error("getIngrediente(EXCEPTION): ", ex);
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

        private List<String> listadealmacenes()
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<String> lista = new List<string>();
                objDB.Open();
                String strQuery = "SELECT * FROM Almacen";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        String idalmacen = Convert.ToString(objDataReader["idAlmacen"]);
                        lista.Add(idalmacen);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                log.Error("getIngrediente(EXCEPTION): ", ex);
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


        public IngredienteBean BuscarIngre(string id)
        {
            
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                IngredienteBean ingrediente = null;

                objDB.Open();
                String strQuery = "SELECT * FROM Ingrediente WHERE idIngrediente = @ID";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", id);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    ingrediente = new IngredienteBean();
                    ingrediente.id = Convert.ToString(objDataReader["idIngrediente"]);
                    ingrediente.nombre = Convert.ToString(objDataReader["nombre"]);
                    ingrediente.descripcion = Convert.ToString(objDataReader["descripcion"]);
                    ingrediente.estado = Convert.ToString(objDataReader["estado"]);   
                }
                return ingrediente;
            }
            catch (Exception ex)
            {
                log.Error("getIngrediente(EXCEPTION): ", ex);
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
        public void ActualizarIngre(IngredienteBean ingrediente)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Ingrediente SET nombre=@nombre, descripcion=@descripcion, estado=@estado" +
                                  " WHERE idIngrediente = @id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", ingrediente.id);
                Utils.agregarParametro(objQuery, "@nombre", ingrediente.nombre);
                Utils.agregarParametro(objQuery, "@descripcion", ingrediente.descripcion);
                Utils.agregarParametro(objQuery, "@estado", ingrediente.estado);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("Actualizar_Ingrediente(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public void EliminarIngrediente(string ID)
        {
            string estado = "INACTIVO";
			SqlConnection objDB = null;
			try
			{
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Ingrediente SET estado=@estado " +
                                  "WHERE idIngrediente = @id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@estado", estado); 
                Utils.agregarParametro(objQuery, "@id", ID);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("EliminarIngrediente(EXCEPTION): ", e);
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


        #region OrdenCompra

        public IngredienteXalmacenBean obtenerlistadAlmacen(string Idalmacen)
        {
            IngredienteXalmacenBean prod = new IngredienteXalmacenBean();

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT * FROM Almacen_x_Producto WHERE idAlmacen =@ID";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", Idalmacen);

                SqlDataReader objDataReader = objquery.ExecuteReader();

                prod.listProdAlmacen = new List<IngredienteAlmacen>();
                
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        IngredienteAlmacen prodalmacen = new IngredienteAlmacen();
                        prodalmacen.id = (String)objDataReader["idIngrediente"];
                        prodalmacen.stockminimo = (int)objDataReader["stockminimo"];
                        prodalmacen.stockactual = (int)objDataReader["stockactual"];
                        prodalmacen.stockmaximo = (int)objDataReader["stockmaximo"];
                        prod.listProdAlmacen.Add(prodalmacen);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("getIngrediente(EXCEPTION): ", ex);
                throw ex;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
            return prod;


        }

        public string getnombreingrediente(string id)
        {
            //string gg = "";

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                IngredienteBean ingrediente = null;

                objDB.Open();
                String strQuery = "SELECT * FROM Ingrediente WHERE idIngrediente = @ID";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", id);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    ingrediente = new IngredienteBean();
                    ingrediente.nombre = Convert.ToString(objDataReader["nombre"]);

                }
                return ingrediente.nombre;
            }
            catch (Exception ex)
            {
                log.Error("getIngrediente(EXCEPTION): ", ex);
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