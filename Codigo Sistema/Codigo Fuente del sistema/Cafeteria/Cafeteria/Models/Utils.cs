using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using log4net;
using System.Web.Configuration;
using Cafeteria.Models.Administracion.Ubigeo;
using System.Net.Mail;


namespace Cafeteria.Models
{
    public class Utils
    {
        public static String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(Utils));

        public static void agregarParametro(SqlCommand objQuery, String nombreParametro, object valorParametro)
        {
            try
            {
                SqlParameter objParametro = new SqlParameter();
                objParametro.ParameterName = nombreParametro;
                objParametro.Value = valorParametro ?? DBNull.Value;
                objQuery.Parameters.Add(objParametro);
            }
            catch (Exception ex)
            {
                log.Error("agregarParametro(EXCEPTION): ", ex);
            }
        }

        public static int cantidad(string tabla)
        {
            SqlConnection objDB = null;
			int i = -1;
			try
			{
				objDB = new SqlConnection(cadenaDB);
				objDB.Open();
				String strQuery = "SELECT COUNT(*) from " + tabla;
				SqlCommand objQuery = new SqlCommand(strQuery, objDB);
				SqlDataReader objDataReader = objQuery.ExecuteReader();
				if (objDataReader.HasRows)
				{
					objDataReader.Read();
					i = Convert.ToInt32(objDataReader[0]);
				}
				return i;
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
			return i;
        }

        public static List<Ubigeo.Departamento> listarDepartamentos() {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<Ubigeo.Departamento> lstDepartamento = null;

                objDB.Open();
                String strQuery = "SELECT idDepartamento, nombre FROM Departamento";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);

                SqlDataReader objReader = objQuery.ExecuteReader();

                if (objReader.HasRows)
                {
                    lstDepartamento = new List<Ubigeo.Departamento>();
                    while (objReader.Read())
                    {
                        Ubigeo.Departamento departamento = new Ubigeo.Departamento();

                        departamento.ID = Convert.ToString(objReader[0]);
                        departamento.nombre = Convert.ToString(objReader[1]);

                        lstDepartamento.Add(departamento);
                    }
                    return lstDepartamento;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("listarDepartamentos(EXCEPTION): ", ex);
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

        public static List<Ubigeo.Provincia> listarProvincias(string idDepartamento) {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<Ubigeo.Provincia> lstProvincia = null;

                objDB.Open();
                String strQuery = "SELECT idProvincia, nombre FROM Provincia WHERE idDepartamento = @idDepartamento";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                agregarParametro(objQuery, "@idDepartamento", idDepartamento);

                SqlDataReader objReader = objQuery.ExecuteReader();

                if (objReader.HasRows)
                {
                    lstProvincia = new List<Ubigeo.Provincia>();
                    while (objReader.Read())
                    {
                        Ubigeo.Provincia provincia = new Ubigeo.Provincia();

                        provincia.ID = (string)(objReader[0]);
                        provincia.nombre = Convert.ToString(objReader[1]);

                        lstProvincia.Add(provincia);
                    }
                    return lstProvincia;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("listarProvincias(EXCEPTION): ", ex);
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

        public static List<Ubigeo.Distrito> listarDistritos(string idDepartamento, string idProvincia)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<Ubigeo.Distrito> lstDistrito = null;

                objDB.Open();
                String strQuery = "SELECT idDistrito, nombre FROM Distrito " +
                                    "WHERE idDepartamento = @idDepartamento AND idProvincia = @idProvincia";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                agregarParametro(objQuery, "@idDepartamento", idDepartamento);
                agregarParametro(objQuery, "@idProvincia", idProvincia);

                SqlDataReader objReader = objQuery.ExecuteReader();

                if (objReader.HasRows)
                {
                    lstDistrito = new List<Ubigeo.Distrito>();
                    while (objReader.Read())
                    {
                        Ubigeo.Distrito distrito = new Ubigeo.Distrito();

                        distrito.ID = (string)(objReader[0]);
                        distrito.nombre = Convert.ToString(objReader[1]);

                        lstDistrito.Add(distrito);
                    }
                    return lstDistrito;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("listarDistritos(EXCEPTION): ", ex);
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

        //login
        public static bool comprobarLogin(string usuario, string contrasenia)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery = "SELECT * FROM Usuario WHERE user_account = @usuario AND pass = @contrasenia AND estado = 'ACTIVO'";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@usuario", usuario);
                Utils.agregarParametro(objQuery, "@contrasenia", contrasenia);

                SqlDataReader objReader = objQuery.ExecuteReader();
                return objReader.HasRows;
            }
            catch (Exception ex)
            {
                log.Error("comprobarLogin(EXCEPTION): ", ex);
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


        public void mandarcorreo(string correodestino, string mensaje, string asunto)
        {
            string emailfrom= "giancarlo.rau@pucp.pe";
            string clave = "614nc4r70";
            MailMessage objMail;
            objMail = new MailMessage();
            objMail.From = new MailAddress(emailfrom); //Remitente
            objMail.To.Add(correodestino); //Email a enviar 
            //objMail.CC.Add("copia@destino.com"); //Email a enviar copia
            //objMail.Bcc.Add("oculto@destino.com"); //Email a enviar oculto

            objMail.Subject = asunto;


            objMail.Body = mensaje;
            objMail.IsBodyHtml = true; //Formato Html del email

            //objMail.Attachments.Add(new Attachment(Adjunto));
            SmtpClient SmtpMail = new SmtpClient();
            SmtpMail.Host = "smtp.gmail.com"; //el nombre del servidor de correo
            //SmtpMail.Port = Puerto; //asignamos el numero de puerto
            SmtpMail.Credentials = new System.Net.NetworkCredential(emailfrom, clave);

            SmtpMail.Send(objMail);
        }

        public string buscarsucursal(string idusuario)
        {
            SqlConnection objDB = null;
            string i = "null";
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT idCafeteria from Sucursal_x_Usuario where UPPER(idUsuario)  LIKE '%" + idusuario.ToUpper() + "%'";

                // WHERE UPPER(numero_documento) LIKE '%" + dni.ToUpper() + "%'";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    i = Convert.ToString(objDataReader[0]);
                }
                return i;
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
            return i;
        }

    }
}