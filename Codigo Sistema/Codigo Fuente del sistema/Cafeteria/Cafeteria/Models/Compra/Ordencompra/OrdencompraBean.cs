using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;
using Cafeteria.Models.Almacen.Notaentrada;

namespace Cafeteria.Models.Compra.Ordencompra
{
    public class Proveedor
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }
    
    
    public class OrdencompraBean
    {
        public string idOrdenCompra { get; set; }

        [Display(Name = "Proveedor")]
        public string idProveedor { get; set; }

        public string nombreSucursal { get; set; }

        public string idCafeteria { get; set; }
        
        //[Display(Name = "Proveedor")]
        //public string idProv { get; set; } 
        
        [Display(Name = "Proveedor")]
        public string nombreProveedor { get; set; }

        [Display(Name = "Ingrediente")]
        public string idIngrediente { get; set; } 
        
        [Display(Name = "Estado")]
        public string estado { get; set; }

        public bool estado2 { get; set; }
        [Display(Name = "Fecha")]
        public string fecha { get; set; }

        [Display(Name = "Precio Total")]
        public decimal precioTotal { get; set; }

        //public List<Producto> productoList { get; set; }

        public SelectList listaProveedor { get; set; }

        public List<detalleordencompra> detalle { get; set; }
       // List<DetalleOrdenCompra> detalles { get; set; }

        public List<Notaentradabean> notasentrada { get; set; }


        public IEnumerable<Proveedor> getProveedor() 
        {
            List<Proveedor> listaProveedor = new List<Proveedor>();

            String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;
            String Estado = "ACTIVO";

            SqlConnection objDB = new SqlConnection(cadenaDB);

            objDB.Open();

            String strQuery = "SELECT * FROM Proveedor WHERE estado = @estado";
            SqlCommand objquery = new SqlCommand(strQuery, objDB);
            BaseDatos.agregarParametro(objquery, "@estado", Estado);
            SqlDataReader objDataReader = objquery.ExecuteReader();
            if (objDataReader.HasRows)
            {
                while (objDataReader.Read())
                {

                    Proveedor proveedor = new Proveedor();
                    proveedor.id = Convert.ToString(objDataReader["idProveedor"]);
                    proveedor.nombre = (string)objDataReader["razonSocial"];

                    listaProveedor.Add(proveedor);
                }
            }

            return listaProveedor;
        }

        

        public OrdencompraBean()
        {
            listaProveedor = new SelectList(getProveedor(), "ID", "Nombre");
        }



    }
}