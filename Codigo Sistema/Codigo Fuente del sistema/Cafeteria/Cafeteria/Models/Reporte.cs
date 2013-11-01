using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

namespace Cafeteria.Models
{
    public class Reporte
    {
        public string idSucursal { get; set; }
        public string nombresucursal { get; set; }

        public string idproveedor { get; set; }
        public string nombreproveedor { get; set; }

        public List<List<String>> listaalmacen { get; set; }
        public List<List<String>> listacompras { get; set; }
        public List<List<String>> listaventas { get; set; }
        public List<List<String>> listaadministracion { get; set; }
        public List<int> cantidad { get; set; }
        public string fecha1 { get; set; }
        public string fecha2 { get; set; }

        public SelectList listaTipo { get; set; }
        public SelectList listaTipoprov { get; set; }
        public Reporte()
        {
            listaTipo = new SelectList(GetTipo(), "ID", "Nombre");
            listaTipoprov = new SelectList(GetTipo2(), "ID", "Nombre");
        }

        public IEnumerable<Tiposucursal> GetTipo()
        {
            List<Tiposucursal> ListaTipo = new List<Tiposucursal>();
            Tiposucursal TipoP = new Tiposucursal();
            TipoP.id = "SUCU0000";
            TipoP.nombre = "Todos";
            ListaTipo.Add(TipoP);

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["Base"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Cafeteria ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();


            while (dataReader.Read())
            {
                Tiposucursal tipoSucursal = new Tiposucursal();
                tipoSucursal.id = Convert.ToString(dataReader["idCafeteria"]);
                tipoSucursal.nombre = (string)dataReader["nombre"];

                ListaTipo.Add(tipoSucursal);
            }

            return ListaTipo;
        }

        public IEnumerable<Tiposucursal> GetTipo2()
        {
            List<Tiposucursal> ListaTipo = new List<Tiposucursal>();
            Tiposucursal TipoP = new Tiposucursal();
            TipoP.id = "PROV0000";
            TipoP.nombre = "Todos";
            ListaTipo.Add(TipoP);

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["Base"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Proveedor ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();


            while (dataReader.Read())
            {
                Tiposucursal tipoSucursal = new Tiposucursal();
                tipoSucursal.id = Convert.ToString(dataReader["idProveedor"]);
                tipoSucursal.nombre = (string)dataReader["razonSocial"];

                ListaTipo.Add(tipoSucursal);
            }

            return ListaTipo;
        }

    }

    public class Tiposucursal
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }

    public class Tipoproveedor
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }

}