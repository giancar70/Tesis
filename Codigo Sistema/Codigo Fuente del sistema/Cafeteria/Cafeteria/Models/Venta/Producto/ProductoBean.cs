using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

namespace Cafeteria.Models.Venta.Producto
{
    public class ProductoBean
    {
        public string id { get; set; }
        
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar nombre de Producto")]
        [RegularExpression("^[a-zA-Z áéíóúAÉÍÓÚÑñ]+$", ErrorMessage = "El nombre ingresado no es válido")]
        public string nombre { get; set; }
        
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        public string idTipo { get; set; }
        public string nombreTipo { get; set; }
        
        public string estado { get; set; }
        public SelectList listaTipo { get; set; }

        public ProductoBean()
        {
            listaTipo = new SelectList(GetTipo(), "ID", "Nombre");
        }
        public IEnumerable<tipoProducto> GetTipo()
        {
            List<tipoProducto> ListaTipo = new List<tipoProducto>();
            tipoProducto TipoP = new tipoProducto();
            TipoP.id = "TIPO0000";
            TipoP.nombre = "Todos";
            ListaTipo.Add(TipoP);

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["Base"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Tipo ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            

            while (dataReader.Read())
            {
                tipoProducto tipoProducto = new tipoProducto();
                tipoProducto.id = Convert.ToString(dataReader["id"]);
                tipoProducto.nombre = (string)dataReader["nombre"];

                ListaTipo.Add(tipoProducto);
            }

            return ListaTipo;
        }

    }
    public class tipoProducto
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }
    public class tipo
    {
        public string id { get; set; }
        public List<tipoProducto> listaProductos { get; set; }
    }

}