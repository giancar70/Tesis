using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Cafeteria.Models
{
    public class BaseDatos
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;

        public static void agregarParametro(SqlCommand objQuery, String nombreParametro, object valorParametro)
        {
            SqlParameter objParametro = new SqlParameter();
            objParametro.ParameterName = nombreParametro;
            
            objParametro.Value = valorParametro ?? DBNull.Value;
            objQuery.Parameters.Add(objParametro);
        }
        public int cantidad(string tabla)
        {
            SqlConnection objDB = null;
            int i = 0;
            objDB = new SqlConnection(cadenaDB);
            objDB.Open();
            String strQuery = "SELECT COUNT(*) from "+tabla;

            SqlCommand objQuery = new SqlCommand(strQuery, objDB);

            SqlDataReader objDataReader = objQuery.ExecuteReader();
            if (objDataReader.HasRows)
            {
                objDataReader.Read();
                

                i = Convert.ToInt32(objDataReader[0]);
                
            }
            return i;
        }
    }
}