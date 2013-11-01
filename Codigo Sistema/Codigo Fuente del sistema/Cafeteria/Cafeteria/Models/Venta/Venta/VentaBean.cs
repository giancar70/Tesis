using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Cafeteria.Models.Venta.Venta
{
    public class VentaBean
    {
        public string idventa { get; set; }

        public DateTime fecharegistro { get; set; }

        public string nombrepersona { get; set; }
        public string dnipersona { get; set; }
        public string nombreusua { get; set; }

        public string idSucursal { get; set; }
        public string nombresucursal { get; set; }
        public List<VentaxProductoBean> listaproductos { get;set;}

        public decimal totalventa { get; set; }
        public string totalventa2 { get; set; }
        public SelectList listaTipo { get; set; }
        public VentaBean()
        {
            listaTipo = new SelectList(GetTipo(), "ID", "Nombre");
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

    }

    public class Tiposucursal
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }

}