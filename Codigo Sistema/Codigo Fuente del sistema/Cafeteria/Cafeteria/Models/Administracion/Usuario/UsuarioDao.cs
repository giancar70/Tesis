using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using log4net;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Cafeteria.Models.Administracion.Usuario
{
    public class UsuarioDao
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(BaseDatos));

        #region login

        public UsuarioBean getLogin(string user, string pass)
        {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Select * from Usuario where user_account = @user_account AND pass = @pass AND estado = @estado";

                SqlCommand query = new SqlCommand(command, sql);

                Utils.agregarParametro(query, "user_account", user);
                Utils.agregarParametro(query, "pass", pass);
                Utils.agregarParametro(query, "estado", "ACTIVO");
                SqlDataReader data = query.ExecuteReader();

                UsuarioBean usuario = null;

                if (data.HasRows)
                {
                    data.Read();
                    string idUsuario = Convert.ToString (data["idUsuario"]);
                    usuario = this.buscarusuario(idUsuario);
                }

                sql.Close();

                return usuario;
            }
            catch (Exception e)
            {
                log.Error("getLogin(EXCEPTION): " + e);
                return null;
            }
            finally
            {
                if (sql != null) sql.Close();
            }
        }

        public String verificar(string user)
        {
            SqlConnection sql = null;
            String resultado = "";
            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Select * from Usuario where user_account = @user_account AND estado = @estado";

                SqlCommand query = new SqlCommand(command, sql);

                Utils.agregarParametro(query, "user_account", user);
                Utils.agregarParametro(query, "estado", "ACTIVO");
                SqlDataReader data = query.ExecuteReader();

                if (data.HasRows)
                {
                    data.Read();
                    resultado = Convert.ToString(data["pass"]);
                }

                sql.Close();

                return resultado;
            }
            catch (Exception e)
            {
                log.Error("getLogin(EXCEPTION): " + e);
                return resultado;
            }
            finally
            {
                if (sql != null) sql.Close();
            }
        }
       

        
        #endregion


        #region usuario
        public List<UsuarioBean> ListarPersonal(string nombre, string dni, string cargo, string sucursal)
        {
            
           
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<UsuarioBean> ListaUsuario = new List<UsuarioBean>();
                objDB.Open();
                String strQuery = "SELECT * FROM Usuario";
                if (!String.IsNullOrEmpty(dni)) strQuery = strQuery + " WHERE UPPER(numero_documento) LIKE '%" + dni.ToUpper() + "%'";
                if (!String.IsNullOrEmpty(nombre)) strQuery = strQuery + " WHERE UPPER(nombre) LIKE '%" + nombre.ToUpper() + "%'";
                //if (!String.IsNullOrEmpty(RazonSocial) && !String.IsNullOrEmpty(contacto)) strQuery = strQuery + " WHERE UPPER(razonSocial) LIKE '%" + RazonSocial.ToUpper() + "%'"+
                //                                                                            " AND UPPER(contacto) LIKE '%" + contacto.ToUpper() + "%'";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        UsuarioBean usuarioelemento = new UsuarioBean();
                        usuarioelemento.ID = Convert.ToString(objDataReader["idUsuario"]);
                        usuarioelemento.nombres = Convert.ToString(objDataReader["nombre"]);
                        usuarioelemento.apPat = Convert.ToString(objDataReader["apellido_paterno"]);
                        usuarioelemento.apMat = Convert.ToString(objDataReader["apellido_materno"]);
                        usuarioelemento.estado = Convert.ToString(objDataReader["estado"]);
                        usuarioelemento.email = Convert.ToString(objDataReader["email"]);
                        usuarioelemento.celular = Convert.ToString(objDataReader["celular"]);
                        usuarioelemento.direccion = Convert.ToString(objDataReader["direccion"]);
                        usuarioelemento.idDepartamento = Convert.ToString(objDataReader["idDepartamento"]);
                        usuarioelemento.idProvincia = Convert.ToString(objDataReader["idProvincia"]);
                        usuarioelemento.idDistrito = Convert.ToString(objDataReader["idDistrito"]);
                        ListaUsuario.Add(usuarioelemento);
                    }
                }

                return ListaUsuario;
            }
            catch (Exception e)
            {
                log.Error("Lista_Usuarios(EXCEPTION): ", e);
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

        public UsuarioBean buscarusuario(string idusuario)
        {
            //UsuarioBean usuario = new UsuarioBean();
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                UsuarioBean usuarioelemento = null;

                objDB.Open();
                String strQuery = "SELECT * FROM Usuario WHERE idUsuario = @ID";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", idusuario);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    usuarioelemento = new UsuarioBean();

                    usuarioelemento.ID = Convert.ToString(objDataReader["idUsuario"]);
                    usuarioelemento.nombres = Convert.ToString(objDataReader["nombre"]);
                    usuarioelemento.apPat = Convert.ToString(objDataReader["apellido_paterno"]);
                    usuarioelemento.apMat = Convert.ToString(objDataReader["apellido_materno"]);
                    usuarioelemento.estado = Convert.ToString(objDataReader["estado"]);
                    usuarioelemento.email = Convert.ToString(objDataReader["email"]);
                    usuarioelemento.celular = Convert.ToString(objDataReader["celular"]);
                    usuarioelemento.direccion = Convert.ToString(objDataReader["direccion"]);
                    usuarioelemento.idDepartamento = Convert.ToString(objDataReader["idDepartamento"]);
                    usuarioelemento.idProvincia = Convert.ToString(objDataReader["idProvincia"]);
                    usuarioelemento.idDistrito = Convert.ToString(objDataReader["idDistrito"]);
                    usuarioelemento.user_account = Convert.ToString(objDataReader["user_account"]);
                    usuarioelemento.nroDocumento = Convert.ToString(objDataReader["numero_documento"]);
                    usuarioelemento.listadeperfil = this.perfilesusuario(usuarioelemento.ID);
                }
                return usuarioelemento;
            }
            catch (Exception ex)
            {
                log.Error("Get_Usuario(EXCEPTION): ", ex);
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

        public void registrarpersonal(UsuarioBean usuario)
        {
            usuario.estado = "ACTIVO";
            SqlConnection objDB = null;
            int i = Utils.cantidad("Usuario") + 1;
            string ID = "USUA00";//8caracteres-4letras-4#
            if (i < 10) usuario.ID = ID + "0" + Convert.ToString(i);
            else usuario.ID = ID + Convert.ToString(i);
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "Insert into Usuario (idUsuario,idDistrito,idProvincia,idDepartamento, nombre,apellido_paterno," +
                                   "apellido_materno, estado, email, celular,numero_documento, direccion, user_account, pass) values " +
                                    "(@id,@distrito,@provincia,@departamento,@nombre, @apepater, @apematerno, @estado,@email," +
                                    "@celular, @numero_documento, @direccion, @user_account, @pass)";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", usuario.ID);
                Utils.agregarParametro(objQuery, "@distrito", usuario.idDistrito);
                Utils.agregarParametro(objQuery, "@provincia", usuario.idProvincia);
                Utils.agregarParametro(objQuery, "@departamento", usuario.idDepartamento);
                Utils.agregarParametro(objQuery, "@nombre", usuario.nombres);
                Utils.agregarParametro(objQuery, "@apepater", usuario.apPat);
                Utils.agregarParametro(objQuery, "@apematerno", usuario.apMat);
                Utils.agregarParametro(objQuery, "@estado", usuario.estado);
                Utils.agregarParametro(objQuery, "@email", usuario.estado);
                Utils.agregarParametro(objQuery, "@celular", usuario.celular);
                Utils.agregarParametro(objQuery, "@numero_documento", usuario.nroDocumento);
                Utils.agregarParametro(objQuery, "@direccion", usuario.direccion);
                Utils.agregarParametro(objQuery, "@user_account", usuario.user_account);
                Utils.agregarParametro(objQuery, "@pass", usuario.pass);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("Registrar_Usuario(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }

        public void eliminarusuario(string ID)
        {
            string estado = "INACTIVO";
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Usuario SET estado=@estado " +
                                  "WHERE idUsuario = @idUsuario";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@estado", estado);
                Utils.agregarParametro(objQuery, "@idUsuario", ID);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("EliminarUsuario(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }


        }

        public void actualizarusuario(UsuarioBean usuario)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Usuario SET nombre=@nombre, apellido_paterno=@apepaterno" +
                                  ", apellido_materno=@apematerno, email=@email,direccion=@direccion," +
                                  "celular=@celular, estado=@estado " +
                                  "WHERE idUsuario = @id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", usuario.ID);
                Utils.agregarParametro(objQuery, "@nombre", usuario.nombres);
                Utils.agregarParametro(objQuery, "@apepaterno", usuario.apPat);
                Utils.agregarParametro(objQuery, "@apematerno", usuario.apMat);
                Utils.agregarParametro(objQuery, "@email", usuario.email);
                Utils.agregarParametro(objQuery, "@direccion", usuario.direccion);
                Utils.agregarParametro(objQuery, "@celular", usuario.celular);
                Utils.agregarParametro(objQuery, "@estado", usuario.estado);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("Actualizar_Usuario(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }

        public void guardarhorario(UsuarioxSucursalBean usuario)
        {
            SqlConnection objDB = null;
            int i = Utils.cantidad("Horario") + 1;
            string ID = "HORA00";//8caracteres-4letras-4#
            if (i < 10) usuario.idhorario = ID + "0" + Convert.ToString(i);
            else usuario.idhorario = ID + Convert.ToString(i);
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "Insert into Horario( idHorario, fechaini, fechafin, idempleado) values(@idhora,@fecha1" +
                                  ", @fecha2, @idemple)";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idhora", usuario.idhorario);
                Utils.agregarParametro(objQuery, "@fecha1", usuario.fechaIngreso);
                Utils.agregarParametro(objQuery, "@fecha2", usuario.fechaFin);
                Utils.agregarParametro(objQuery, "@idemple", usuario.ID);
                objQuery.ExecuteNonQuery();
                this.guardardetalle(usuario);
            }
            catch (Exception e)
            {
                log.Error("Insertar horario(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        private void guardardetalle(UsuarioxSucursalBean usuario)
        {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < usuario.dia.Count; i++)
                {
                   // if (!string.IsNullOrEmpty(usuario.horaInicio[i]))
                  //  {
                        
                        String strQuery = "Insert into HorarioDetalle(idHorario, diasemana, horaentrada, horasalida) values(@idhora,@dia" +
                                  ", @hora1, @hora2)";

                        SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                        Utils.agregarParametro(objQuery, "@idhora", usuario.idhorario);
                        Utils.agregarParametro(objQuery, "@dia", usuario.dia[i]);
                        Utils.agregarParametro(objQuery, "@hora1", usuario.horaInicio[i]);
                        Utils.agregarParametro(objQuery, "@hora2", usuario.horaFin[i]);
                        objQuery.ExecuteNonQuery();
                //    }
                }
                

            }
            catch (Exception e)
            {
                log.Error("Insertar horariodetalle(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }


        public UsuarioxSucursalBean obtenerhorario(String id)
        {
            SqlConnection objDB = null;
            UsuarioxSucursalBean usua = new UsuarioxSucursalBean();
            usua.dia = new List<string>();
            
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "Select * from Horario where idempleado=@idemple";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idemple", id);
                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    usua.ID = Convert.ToString(objDataReader["idempleado"]);
                    usua.fechaIngreso = (DateTime)(objDataReader["fechaini"]);
                    usua.fechaFin = (DateTime)(objDataReader["fechafin"]);
                    usua.idhorario = Convert.ToString(objDataReader["idHorario"]);
                    UsuarioxSucursalBean aux = new UsuarioxSucursalBean();
                    aux = this.devolverdetallehorario(usua.idhorario);
                    usua.horaFin = aux.horaFin;
                    usua.horaInicio = aux.horaInicio;
                    usua.dia = aux.dia;
                }
                

            }
            catch (Exception e)
            {
                log.Error("Insertar horariodetalle(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
            return usua;
        }

        private UsuarioxSucursalBean devolverdetallehorario(string idhorario)
        {
            SqlConnection objDB = null; int i = 0;
            UsuarioxSucursalBean usua = new UsuarioxSucursalBean();
            usua.dia = new List<string>();
            usua.horaInicio = new List<string>();
            usua.horaFin = new List<string>();
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "Select * from HorarioDetalle where idHorario=@idho";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idho", idhorario);
                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    usua.dia = new List<string>();
                    usua.horaInicio = new List<string>();
                    usua.horaFin = new List<string>();
                    while (objDataReader.Read())
                    {
                        
                        usua.dia.Add(Convert.ToString(objDataReader["diasemana"]));
                        usua.horaInicio.Add(Convert.ToString(objDataReader["horaentrada"]));
                        usua.horaFin.Add(Convert.ToString(objDataReader["horasalida"]));
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Insertar horariodetalle(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
            return usua;
        }

        public string obtenersucursal(string idusua)
        {
            SqlConnection objDB = null;
            string id = "vacio";
            try
            {
                objDB = new SqlConnection(cadenaDB);
                string estado = "Activo";
                objDB.Open();
                String strQuery = "SELECT * FROM Sucursal_x_Usuario where idUsuario=@id and estado=@estado";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", idusua);
                Utils.agregarParametro(objQuery, "@estado", estado);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        id = Convert.ToString(objDataReader["idCafeteria"]);
                    }
                }

                
            }
            catch (Exception e)
            {
                log.Error("Lista_Sucursal_personal(EXCEPTION): ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
            return id;
        }

        public void modificarusuario(UsuarioxSucursalBean usuario)
        {
            UsuarioxSucursalBean usuarioaux = this.obtenerhorario(usuario.ID);
            usuario.idhorario = usuarioaux.idhorario;
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < usuario.dia.Count; i++)
                {
                    String strQuery = "Update HorarioDetalle set horaentrada=@hora1 , horasalida= @hora2  where " +
                                  " idHorario=@idhora and diasemana=@dia ";

                    SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                    Utils.agregarParametro(objQuery, "@idhora", usuario.idhorario);
                    Utils.agregarParametro(objQuery, "@dia", usuario.dia[i]);
                    Utils.agregarParametro(objQuery, "@hora1", usuario.horaInicio[i]);
                    Utils.agregarParametro(objQuery, "@hora2", usuario.horaFin[i]);
                    objQuery.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                log.Error("modificar horariodetalle(EXCEPTION): ", e);
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

        #region perfil 

        public List<String> ListarPersonalconperfil(string nombre, string dni, string perfil)
        {


            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<UsuarioxSucursalBean> ListaUsuario = new List<UsuarioxSucursalBean>();
                objDB.Open();
                String strQuery = " ";
                if (perfil.Equals("PERF0000"))
                {
                    strQuery = strQuery + "SELECT * FROM Perfil_usuario_x_Usuario";
                }
                else
                {
                    strQuery = strQuery + "SELECT * FROM Perfil_usuario_x_Usuario where UPPER(idPerfil_usuario) LIKE '%" + perfil.ToUpper() + "%'";
                }

                List<string> listaIdusuario = new List<string>();

//                List<UsuarioBean> usuario = this.ListarPersonal(nombre, dni, " ", " "); // lista de usuarios con nombre y dni

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        string id = Convert.ToString(objDataReader["idUsuario"]); ;
                        listaIdusuario.Add(id);
                    }
                }

                return listaIdusuario;
                //return ListaUsuario;
            }
            catch (Exception e)
            {
                log.Error("Lista_Usuarios(EXCEPTION): ", e);
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

        public string getnombreperfil(string idperfil)
        {
            string nombre = "";

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<UsuarioxSucursalBean> ListaUsuario = new List<UsuarioxSucursalBean>();
                objDB.Open();
                String strQuery = "SELECT * FROM Perfil_usuario WHERE UPPER(idPerfil_usuario) LIKE '%" + idperfil.ToUpper() + "%'";
                               
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        nombre = Convert.ToString(objDataReader["nombre"]);   
                    }
                }

                return nombre;
            }
            catch (Exception e)
            {
                log.Error("Lista_Usuarios(EXCEPTION): ", e);
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

        public List<String> Listaperfiles(string IDusuario)
        {

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<UsuarioxSucursalBean> ListaUsuario = new List<UsuarioxSucursalBean>();
                objDB.Open();
                String strQuery = "SELECT * FROM Perfil_usuario_x_usuario WHERE UPPER(idUsuario) LIKE '%" + IDusuario.ToUpper() + "%'";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                List<string> listaperfiles = new List<string>();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        string idperfil = Convert.ToString(objDataReader["idPerfil_usuario"]);
                        string espacio = " -- ";
                        listaperfiles.Add(this.getnombreperfil(idperfil));
                        listaperfiles.Add(espacio);
                    }
                }

                return listaperfiles;
            }
            catch (Exception e)
            {
                log.Error("Lista_Usuarios(EXCEPTION): ", e);
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

        public List<String> getperfiles()
        {

            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<UsuarioxSucursalBean> ListaUsuario = new List<UsuarioxSucursalBean>();
                objDB.Open();
                String strQuery = "SELECT * FROM Perfil_usuario";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                List<string> listaperfiles = new List<string>();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        string idperfil = Convert.ToString(objDataReader["idPerfil_usuario"]);
                        listaperfiles.Add(this.getnombreperfil(idperfil));
                    }
                }

                return listaperfiles;
            }
            catch (Exception e)
            {
                log.Error("Lista_Usuarios(EXCEPTION): ", e);
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


        public DateTime buscarfecha(string id)
        {
            DateTime date = new DateTime();
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT a.fechaini FROM Horario a where a.idempleado=@id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", id);
                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        date = Convert.ToDateTime(objDataReader[0]);
                        //string espacio = " -- ";
                        //listaperfiles.Add(this.getnombreperfil(idperfil));
                        //listaperfiles.Add(espacio);
                    }
                }

                return date;
            }
            catch (Exception e)
            {
                log.Error("Lista_Usuarios(EXCEPTION): ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

            //return date;
        }

        private Boolean haydatos(string idusuario)
        {
            SqlConnection objDB = null;
            int i = -1;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT COUNT(*) from Perfil_usuario_x_Usuario where UPPER(idUsuario) LIKE '%" + idusuario.ToUpper() + "%'"; 
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    i = Convert.ToInt32(objDataReader[0]);
                }
                
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
            if (i > 0) return true;
            else return false;
        }

        private void eliminardatos(string idusuario)
        {
            if (haydatos(idusuario))
            {
                SqlConnection objDB = null;
                
                try
                {
                    objDB = new SqlConnection(cadenaDB);
                    objDB.Open();
                    String strQuery = "Delete from Perfil_usuario_x_Usuario where UPPER(idUsuario) LIKE '%" + idusuario.ToUpper() + "%'";
                    SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                    SqlDataReader objDataReader = objQuery.ExecuteReader();

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
            }
        }

        public void guardarperfil(UsuarioxSucursalBean usuario)
        {

            eliminardatos(usuario.ID);
            SqlConnection objDB = null;
            try
            {

                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < usuario.perfilesDelUsuario.Count; i++) //perfilesDelUsuario
                {

                    if (usuario.estadosDePerfiles[i])
                    {
                        String strQuery = "Insert into  Perfil_usuario_x_Usuario (idPerfil_usuario, idUsuario) values(@id, @id2)";

                        SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                        Utils.agregarParametro(objQuery, "@id", this.buscaridperfil(usuario.perfilesDelUsuario[i]));

                        Utils.agregarParametro(objQuery, "@id2", usuario.ID);
                        objQuery.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception e)
            {
                log.Error("Guardar_perfil(EXCEPTION): ", e);
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

        private string buscaridperfil(string nombre)
        {
            string id=null;
            SqlConnection objDB = null;
            try
            {

                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT a.idPerfil_usuario FROM Perfil_usuario a WHERE nombre =@nom ";//nombre.ToUpper() + "%'"; 


                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@nom", nombre);
                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        id = Convert.ToString(objDataReader[0]);
                    }
                }

                return id;
            }
            catch (Exception e)
            {
                log.Error("Guardar_perfil(EXCEPTION): ", e);
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

        private List<int> perfilesusuario(string idusuario)
        {
            SqlConnection objDB = null;
            List<int> lista = new List<int>();

            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT a.token FROM Perfil_usuario a, Perfil_usuario_x_Usuario b where a.idPerfil_usuario=b.idPerfil_usuario and b.idUsuario=@id order by 1";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", idusuario);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        int valor = Convert.ToInt32(objDataReader[0]);
                        lista.Add(valor);
                    }
                }

                return lista;
            }
            catch (Exception e)
            {
                log.Error("Lista_Usuarios(EXCEPTION): ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

            //return null;
        }



        #endregion



    }
}

