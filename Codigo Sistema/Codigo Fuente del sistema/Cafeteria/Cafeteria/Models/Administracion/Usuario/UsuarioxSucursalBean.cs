using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafeteria.Models.Administracion.Perfil_Usuario;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Configuration;

namespace Cafeteria.Models.Administracion.Usuario
{
    public class Perfiles2
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }

    public class Tiposucursal
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }
    
    public class UsuarioxSucursalBean : UsuarioBean
    {
        public DateTime fechaInicioTrabajo { get; set; }
        public DateTime fechaIngreso22 { get; set; }
        public DateTime fechaFin { get; set; }
        public string idsucursal { get; set; }
        public string sucursal { get; set; }
        public string cargo { get; set; }
        public List<string> dia { get; set; }
        public string idhorario { get; set; }

        public List<string> horaInicio { get; set; }
        public List<string> horaFin { get; set; }

        public List<Boolean> estadosDePerfiles { get; set; }
        public string perfil { get; set; }

        public string idPerfil { get; set; }

        public List<string> perfilesDelUsuario { get; set; }

        public SelectList usuarioList { get; set; }

        public List<PerfilUsuarioBean> listaPerfilUsuario { get; set; }

        public SelectList listaTipo { get; set; }


        public IEnumerable<Perfiles2> obtenerPerfil()
        {
            List<Perfiles2> listaperfil = new List<Perfiles2>();
            String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;


            SqlConnection objDB = new SqlConnection(cadenaDB);
            objDB.Open();

            string commandString = "SELECT * FROM Perfil_usuario ";

            SqlCommand sqlCmd = new SqlCommand(commandString, objDB);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            Perfiles2 perfiles = new Perfiles2();
            perfiles.id = "PERF0000";
            perfiles.nombre = "Todos";
            listaperfil.Add(perfiles);

            while (dataReader.Read())
            {
                Perfiles2 perfil = new Perfiles2();
                perfil.id = Convert.ToString(dataReader["idPerfil_usuario"]);
                perfil.nombre = (string)dataReader["nombre"];

                listaperfil.Add(perfil);
            }
            
            
            return listaperfil;
        }

        public UsuarioxSucursalBean()
        {
            usuarioList = new SelectList(obtenerPerfil(), "id", "nombre");
            listaTipo = new SelectList(GetTipo(), "ID", "Nombre");
        }

        public IEnumerable<Tiposucursal> GetTipo()
        {
            List<Tiposucursal> ListaTipo = new List<Tiposucursal>();

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



}