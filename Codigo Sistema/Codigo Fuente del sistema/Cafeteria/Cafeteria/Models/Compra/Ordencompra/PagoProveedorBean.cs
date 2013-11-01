using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Cafeteria.Models.Compra.Ordencompra
{
    public class Proveedores
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
    }
    
    
        


        public class PagoProveedorBean
        {
            public string ID { get; set; }
            public int idPago { get; set; }

            public IEnumerable<Proveedores> getProveedores()
            {
                List<Proveedores> listaProveedor = new List<Proveedores>();

                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["Base"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "SELECT * FROM Proveedor ";

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                SqlDataReader dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Proveedores proveedor = new Proveedores();
                    proveedor.ID = Convert.ToString(dataReader["idProveedor"]);
                    proveedor.Nombre = (string)dataReader["razonSocial"];
                    listaProveedor.Add(proveedor);
                }
                return listaProveedor;
            }

            public SelectList proveedorList { get; set; }

            public PagoProveedorBean()
            {
                proveedorList = new SelectList(getProveedores(), "ID", "Nombre");
            }

        }
    }
