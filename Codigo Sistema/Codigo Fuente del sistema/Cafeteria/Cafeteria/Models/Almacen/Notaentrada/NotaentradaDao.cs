using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using log4net;
using System.Data.SqlClient;
using Cafeteria.Models.Almacen.Ingrediente;

namespace Cafeteria.Models.Almacen.Notaentrada
{
    public class NotaentradaDao
    {

        String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(BaseDatos));
        String Idnotaentrada = "";

        public void guardarnotaentrada(Notaentradabean nota, string estado)
        {
            int cantidad2 = 0;

            for (int i = 0; i < nota.detalleNotaEntrada.Count; i++)
            {
                if (nota.detalleNotaEntrada[i].cantidadentrante > 0) cantidad2++;
            }
            if (cantidad2 > 0)
            {
                cambiarestado(nota.idOrdenCompra, estado);
                insertarnotaentrada(nota);
                insertardetallenota(nota);               
            }

        }

        public void actualizarstock(Notaentradabean not)
        {
            SqlConnection objDB = null;
            String idalmacen = buscaralmacen(not.idCafeteria);
            IngredienteXalmacenBean productos = new IngredienteXalmacenBean();
            Ingredientedao ingredao = new Ingredientedao();
            productos = ingredao.obtenerlistadAlmacen(idalmacen);
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                List<int> cantidades = new List<int>();
                for (int i = 0; i < not.detalleNotaEntrada.Count; i++)
                {
                    for (int j = 0; j < productos.listProdAlmacen.Count; j++)
                    {
                        if (not.detalleNotaEntrada[i].id == productos.listProdAlmacen[j].id)
                        {
                            int cant = not.detalleNotaEntrada[i].cantidadentrante + productos.listProdAlmacen[j].stockactual;
                            cantidades.Add(cant);
                        }
                    }
                }

                for (int i = 0; i < not.detalleNotaEntrada.Count; i++)
                {
                    String strQuery = "UPDATE Almacen_x_Producto SET stockactual=@stock " +
                                  "WHERE idIngrediente = @idingre and idAlmacen=@idalmacen ";

                    SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                    Utils.agregarParametro(objQuery, "@stock", cantidades[i]);
                    Utils.agregarParametro(objQuery, "@idingre", not.detalleNotaEntrada[i].id);
                    Utils.agregarParametro(objQuery, "@idalmacen", idalmacen);
                    objQuery.ExecuteNonQuery();
                }
                

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
        private void cambiarestado(string idordencompra, string estado)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Ordencompra SET estado=@estado " +
                                  "WHERE idOrdencompra = @id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@estado", estado);
                Utils.agregarParametro(objQuery, "@id", idordencompra);
                objQuery.ExecuteNonQuery();

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

        private void insertarnotaentrada(Notaentradabean nota)
        {
            SqlConnection objDB = null;
            int i = Utils.cantidad("Notaentrada") + 1;
            string ID = "NOTA00";//8caracteres-4letras-4#
            if (i < 10) nota.idGuiaRemision = ID + "0" + Convert.ToString(i);
            else nota.idGuiaRemision = ID + Convert.ToString(i);
            this.Idnotaentrada = nota.idGuiaRemision;

            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "Insert into Notaentrada (idNotaentrada,idOrdencompra, fechaEntrega) values " +
                                   "(@id,@idorden,GETDATE())";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", nota.idGuiaRemision);
                Utils.agregarParametro(objQuery, "@idorden", nota.idOrdenCompra);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("Registrar nota entrada(EXCEPTION): ", e);
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

        private void insertardetallenota(Notaentradabean nota)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < nota.detalleNotaEntrada.Count; i++)
                {
                    if (nota.detalleNotaEntrada[i].cantidadentrante > 0)
                    {
                        String strQuery = "Insert Into  notaEntradaDetalle (idNotaentrada,idIngrediente, cantidadentrante) values( @idnota,@ID, @cant)";

                        SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                        Utils.agregarParametro(objQuery, "@ID", nota.detalleNotaEntrada[i].id);
                        Utils.agregarParametro(objQuery, "@idnota", this.Idnotaentrada);
                        Utils.agregarParametro(objQuery, "@cant", nota.detalleNotaEntrada[i].cantidadentrante);
                        objQuery.ExecuteNonQuery();

                    }
                }

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

        private string buscaralmacen(string idsucur)
        {
            SqlConnection objDB = null;
            string idalmacen = "";
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();

                String strQuery = "Select * from  Almacen " +
                                  "WHERE idCafeteria = @id ";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", idsucur);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    idalmacen = (String)(objDataReader["idAlmacen"]);
                }
                return idalmacen;
                //objQuery.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                log.Error("Obtener almacen(EXCEPTION): ", e);
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