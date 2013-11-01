using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using log4net;
using System.Data.SqlClient;

namespace Cafeteria.Models.Reportes
{
    public class ReporteDao
    {

        String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(BaseDatos));

        #region area de almacen

        public List<List<String>> reportealmacen(string fecha1, string fecha2, string idSucursal)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                List<List<String >> listafinal = new List<List<string>>();
                SqlCommand objQuery;
                if (idSucursal.CompareTo("SUCU0000")!=0)
                {
                    String strQuery = "Select b.nombre, d.nombre,c.stockminimo, c.stockactual, e.cantidad " +
                    "from Almacen a, Cafeteria b, Almacen_x_Producto c, Ingrediente d, " +
                    "(Select a.idSucursal as sucursal, b.idIngrediente as ingrediente, SUM(b.cantidad) as  cantidad " +
                    " from Ordencompra a, OrdenCompraDetalle b " +
                    "where  a.idOrdencompra=b.idOrdencompra and a.fechaemitida  BETWEEN  @fecha1 AND @fecha2  " +
                    "GROUP by b.idIngrediente, a.idSucursal) e " +
                    "where a.idCafeteria=b.idCafeteria and e.sucursal=a.idCafeteria " +
                    " and a.idAlmacen=c.idAlmacen and c.idIngrediente=d.idIngrediente " +
                    " and e.ingrediente=d.idIngrediente and b.idCafeteria=@idsucur order by 2";

                    objQuery = new SqlCommand(strQuery, objDB);
                    Utils.agregarParametro(objQuery, "@fecha1", fecha1);
                    Utils.agregarParametro(objQuery, "@fecha2", fecha2);
                    Utils.agregarParametro(objQuery, "@idsucur", idSucursal);
                   
                }
                else
                {
                    String strQuery = "Select b.nombre, d.nombre, c.stockminimo, c.stockactual, e.cantidad " +
                    "from Almacen a, Cafeteria b, Almacen_x_Producto c, Ingrediente d, " +
                    "(Select  a.idSucursal as sucursal,b.idIngrediente as ingrediente, SUM(b.cantidad) as  cantidad " +
                    " from Ordencompra a, OrdenCompraDetalle b " +
                    "where  a.idOrdencompra=b.idOrdencompra and a.fechaemitida  BETWEEN  @fecha1 AND @fecha2 " +
                    "GROUP by  b.idIngrediente, a.idSucursal) e " +
                    "where a.idCafeteria=b.idCafeteria and e.sucursal=a.idCafeteria " +
                    " and a.idAlmacen=c.idAlmacen and c.idIngrediente=d.idIngrediente " +
                    " and e.ingrediente=d.idIngrediente order by 1";

                    objQuery = new SqlCommand(strQuery, objDB);
                    Utils.agregarParametro(objQuery, "@fecha1", fecha1);
                    Utils.agregarParametro(objQuery, "@fecha2", fecha2);
                    //objQuery.ExecuteNonQuery();
                }

                //objQuery.ExecuteNonQuery();
                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        List<String> lis =  new List<string> ();
                        string a = Convert.ToString(objDataReader[0]); lis.Add(a);
                        string b = Convert.ToString(objDataReader[1]); lis.Add(b);
                        string c = Convert.ToString(objDataReader[2]); lis.Add(c);
                        string d = Convert.ToString(objDataReader[3]); lis.Add(d);
                        string e = Convert.ToString(objDataReader[4]); lis.Add(e);
                        listafinal.Add(lis);
                    }
                }

                return listafinal;
            }
            catch (Exception e)
            {
                log.Error("Reporte Almacen(EXCEPTION): ", e);
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

        #region area de compras

        public List<List<String>> reportecompras(string idSucursal, string fecha1, string fecha2, string idproveedor, string monto1, string monto2)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                List<List<String>> listafinal = new List<List<string>>();
                SqlCommand objQuery;
                string estado = "activo";

                string strQuery = "select b.nombre, a.razonSocial, c.fechaemitida, sum(c.preciototal) " + 
                                  "from Proveedor a,  Cafeteria b, Ordencompra c "+
                                  "where a.idProveedor=c.idProveedor and b.idCafeteria=c.idSucursal and "+ 
                                  "c.fechaemitida  BETWEEN  @fecha1 AND @fecha2 ";
               
                if (idSucursal.CompareTo("SUCU0000") != 0) strQuery = strQuery + "and UPPER(c.idSucursal) LIKE '%" + idSucursal.ToUpper() + "%'";
                if (idproveedor.CompareTo("PROV0000") != 0) strQuery = strQuery + "and UPPER(a.idProveedor) LIKE '%" + idproveedor.ToUpper() + "%'";

                strQuery = strQuery + " GROUP by b.nombre, c.fechaemitida,a.razonSocial ";

                objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@fecha1", fecha1);
                Utils.agregarParametro(objQuery, "@fecha2", fecha2);
                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        List<String> lis = new List<string>();
                        string a = Convert.ToString(objDataReader[0]); lis.Add(a);
                        string b = Convert.ToString(objDataReader[1]); lis.Add(b);
                        string c = Convert.ToString(objDataReader[2]); lis.Add(c);
                        string d = Convert.ToString(objDataReader[3]); lis.Add(d);
                        lis.Add(estado);
                        listafinal.Add(lis);
                    }
                }

                return listafinal;
            }
            catch (Exception e)
            {
                log.Error("Reporte Compras(EXCEPTION): ", e);
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

        #region area de ventas

        public List<List<String>> reporteventas(string idSucursal, string fecha1, string fecha2, string monto1, string monto2)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open(); string estado = "activo";
                List<List<String>> listafinal = new List<List<string>>();
                SqlCommand objQuery;

                string strQuery = "select a.nombre, b.fechaventa, sum(b.montototal)   " +
                                  "from Cafeteria a, Venta b " +
                                  "where a.idCafeteria= b.idCafeteria  and " +
                                  "b.fechaventa  BETWEEN  @fecha1 AND @fecha2  ";

                if (idSucursal.CompareTo("SUCU0000") != 0) strQuery = strQuery + "and UPPER(a.idCafeteria) LIKE '%" + idSucursal.ToUpper() + "%'";

                strQuery = strQuery + " group by a.nombre, b.fechaventa order by a.nombre ";


                objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@fecha1", fecha1);
                Utils.agregarParametro(objQuery, "@fecha2", fecha2);
                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        List<String> lis = new List<string>();
                        string a = Convert.ToString(objDataReader[0]); lis.Add(a);
                        string b = Convert.ToString(objDataReader[1]); lis.Add(b);
                        string c = Convert.ToString(objDataReader[2]); lis.Add(c);
                        string d = Convert.ToString(estado); lis.Add(d);
                        //string e = Convert.ToString(objDataReader[4]); lis.Add(e);
                        listafinal.Add(lis);
                    }
                }

                return listafinal;
            }
            catch (Exception e)
            {
                log.Error("Reporte ventas(EXCEPTION): ", e);
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

        #region area de administrativa

        #endregion

    }
}