using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using log4net;
using System.Data.SqlClient;


namespace Cafeteria.Models.Compra.Proveedor
{
    public class ProveedorDao
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["Base"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(BaseDatos));
        
        #region Proveedor
        public List<ProveedorBean> ListarProveedor(string RazonSocial, string contacto)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<ProveedorBean> ListaIngre = new List<ProveedorBean>();
                objDB.Open();
                String strQuery = "SELECT * FROM Proveedor";

                if (!String.IsNullOrEmpty(RazonSocial) || !String.IsNullOrEmpty(contacto))
                {
                    if (!String.IsNullOrEmpty(RazonSocial) && !String.IsNullOrEmpty(contacto))
                    {
                        strQuery = strQuery + " WHERE UPPER(razonSocial) LIKE '%" + RazonSocial.ToUpper() + "%'" + " AND UPPER(contacto) LIKE '%" + contacto.ToUpper() + "%'";
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(RazonSocial))
                        {
                            strQuery = strQuery + " WHERE UPPER(razonSocial) LIKE '%" + RazonSocial.ToUpper() + "%'";
                        }
                        if (!String.IsNullOrEmpty(contacto))
                        {
                            strQuery = strQuery + " WHERE UPPER(contacto) LIKE '%" + contacto.ToUpper() + "%'";
                        }
                    }

                }
                //if (!String.IsNullOrEmpty(RazonSocial)) strQuery = strQuery + " WHERE UPPER(razonSocial) LIKE '%" + RazonSocial.ToUpper() + "%'";
                //if (!String.IsNullOrEmpty(contacto)) strQuery = strQuery + " WHERE UPPER(contacto) LIKE '%" + contacto.ToUpper() + "%'";
                //if (!String.IsNullOrEmpty(RazonSocial) && !String.IsNullOrEmpty(contacto)) strQuery = strQuery + " WHERE UPPER(razonSocial) LIKE '%" + RazonSocial.ToUpper() + "%'"+
                //                                                                            " AND UPPER(contacto) LIKE '%" + contacto.ToUpper() + "%'";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                   while (objDataReader.Read())
                   {
                        ProveedorBean Proveedor = new ProveedorBean();
                        Proveedor.id = Convert.ToString(objDataReader["idProveedor"]);
                        Proveedor.razonSocial = Convert.ToString(objDataReader["razonSocial"]);
                        Proveedor.estado = Convert.ToString(objDataReader["estado"]);
                        Proveedor.contacto = Convert.ToString(objDataReader["contacto"]);
                        Proveedor.emailContacto = Convert.ToString(objDataReader["email_contacto"]);
                        Proveedor.direccion = Convert.ToString(objDataReader["direccion"]);
                        Proveedor.ruc = Convert.ToString(objDataReader["ruc"]);
                        Proveedor.telefono1 = Convert.ToString(objDataReader["telefono1"]);
                        Proveedor.CargoContacto = Convert.ToString(objDataReader["cargo_contacto"]);
                        Proveedor.telefonoContacto = Convert.ToString(objDataReader["telefono_contacto"]);
                        Proveedor.web = Convert.ToString(objDataReader["web"]);
                        Proveedor.Observacion = Convert.ToString(objDataReader["observacion"]);
                        Proveedor.telefono2 = Convert.ToString(objDataReader["telefono2"]);
                        ListaIngre.Add(Proveedor);
                   }
                }

                return ListaIngre;
             }
             catch (Exception e)
             {
                log.Error("Lista_Proveedores(EXCEPTION): ", e);
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
        public void RegistrarProveedor(ProveedorBean Prov)
        {
            Prov.estado = "ACTIVO";
            Prov.CargoContacto = "gG";
            SqlConnection objDB = null;
            int i = Utils.cantidad("Proveedor")+1;
            string ID="PROV00";//8caracteres-4letras-4#
            if (i < 10) Prov.id = ID + "0" + Convert.ToString(i);
            else Prov.id = ID + Convert.ToString(i);
			try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "Insert into Proveedor (idProveedor,razonSocial, estado,contacto,email_contacto,direccion,"+
                                   " ruc, telefono1, cargo_contacto, telefono_contacto, web, observacion, telefono2) values " +
                                    "(@id,@razonsocial,@estado,@contacto,@email_contacto, @direccion, @ruc, @telefono1,@cargo_contacto," +
                                    "@telefono_contacto, @web, @observacion, @telefono2)";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", Prov.id);
                Utils.agregarParametro(objQuery, "@razonsocial", Prov.razonSocial);
                Utils.agregarParametro(objQuery, "@estado", Prov.estado);
                Utils.agregarParametro(objQuery, "@contacto", Prov.contacto);
                Utils.agregarParametro(objQuery, "@email_contacto", Prov.emailContacto);
                Utils.agregarParametro(objQuery, "@direccion", Prov.direccion);
                Utils.agregarParametro(objQuery, "@ruc", Prov.ruc);
                Utils.agregarParametro(objQuery, "@telefono1", Prov.telefono1);
                Utils.agregarParametro(objQuery, "@cargo_contacto", Prov.CargoContacto);
                Utils.agregarParametro(objQuery, "@telefono_contacto", Prov.telefonoContacto);
                Utils.agregarParametro(objQuery, "@web", Prov.web);
                Utils.agregarParametro(objQuery, "@observacion", Prov.Observacion);
                Utils.agregarParametro(objQuery, "@telefono2", Prov.telefono2);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("Registrar_Proveedor(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }
        
        public ProveedorBean BuscarProveedor(string id)
        {
            
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                ProveedorBean Proveedor = null;

                objDB.Open();
                String strQuery = "SELECT * FROM Proveedor WHERE idProveedor = @ID";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", id);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    Proveedor = new ProveedorBean();

                    Proveedor.id = Convert.ToString(objDataReader["idProveedor"]);
                    Proveedor.razonSocial = Convert.ToString(objDataReader["razonSocial"]);
                    Proveedor.estado = Convert.ToString(objDataReader["estado"]);
                    Proveedor.contacto = Convert.ToString(objDataReader["contacto"]);
                    Proveedor.emailContacto = Convert.ToString(objDataReader["email_contacto"]);
                    Proveedor.direccion = Convert.ToString(objDataReader["direccion"]);
                    Proveedor.ruc = Convert.ToString(objDataReader["ruc"]);
                    Proveedor.telefono1 = Convert.ToString(objDataReader["telefono1"]);
                    Proveedor.CargoContacto = Convert.ToString(objDataReader["cargo_contacto"]);
                    Proveedor.telefonoContacto = Convert.ToString(objDataReader["telefono_contacto"]);
                    Proveedor.web = Convert.ToString(objDataReader["web"]);
                    Proveedor.Observacion = Convert.ToString(objDataReader["observacion"]);
                    Proveedor.telefono2 = Convert.ToString(objDataReader["telefono2"]);
                }
                return Proveedor;
            }
            catch (Exception ex)
            {
                log.Error("Get_Proveedor(EXCEPTION): ", ex);
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
        public void ActualizarProveedor(ProveedorBean Prov)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Proveedor SET razonSocial=@razonsocial, estado=@estado" +
                                  ", contacto=@contacto, email_contacto=@email_contacto,direccion=@direccion,ruc=@ruc,"+
                                  "telefono1=@telefono1, cargo_contacto=@cargo_contacto,telefono_contacto=@telefono_contacto,web=@web,observacion=@observacion,telefono2=@telefono2 " +
                                  "WHERE idProveedor = @id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@id", Prov.id);
                Utils.agregarParametro(objQuery, "@razonsocial", Prov.razonSocial);
                Utils.agregarParametro(objQuery, "@estado", Prov.estado);
                Utils.agregarParametro(objQuery, "@contacto", Prov.contacto); 
                Utils.agregarParametro(objQuery, "@email_contacto", Prov.emailContacto);
                Utils.agregarParametro(objQuery, "@direccion", Prov.direccion);
                Utils.agregarParametro(objQuery, "@ruc", Prov.ruc);
                Utils.agregarParametro(objQuery, "@telefono1", Prov.telefono1);
                Utils.agregarParametro(objQuery, "@cargo_contacto", Prov.CargoContacto);
                Utils.agregarParametro(objQuery, "@telefono_contacto", Prov.telefonoContacto);
                Utils.agregarParametro(objQuery, "@web", Prov.web);
                Utils.agregarParametro(objQuery, "@observacion", Prov.Observacion);
                Utils.agregarParametro(objQuery, "@telefono2", Prov.telefono2);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("Actualizar_Proveedor(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public void EliminarProveedor(string ID)
        {
            string estado = "INACTIVO";
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "UPDATE Proveedor SET estado=@estado " +
                                  "WHERE idProveedor = @id";

                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@estado", estado);
                Utils.agregarParametro(objQuery, "@id", ID);
                objQuery.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                log.Error("EliminarProveedor(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public Boolean existe_ruc(string ruc)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT * FROM Proveedor WHERE ruc = @ruc";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ruc", ruc);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                log.Error("existe_Ruc(EXCEPTION): ", ex);
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
        public Boolean existe_razonSocial(string razonSocial)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                String strQuery = "SELECT * FROM Proveedor WHERE razonSocial = @razonsocial";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@razonsocial", razonSocial);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                log.Error("existe_razonsocial(EXCEPTION): ", ex);
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

        #endregion

        #region IngredientexProveedor
        public ProveedorxIngredienteBean listaIngredientes(string ID)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                ProveedorxIngredienteBean prov = null;

                objDB.Open();
                String strQuery = "SELECT * FROM Proveedor_x_Producto WHERE idProveedor = @ID";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                BaseDatos.agregarParametro(objquery, "@ID", ID);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                prov = new ProveedorxIngredienteBean();
                prov.listadeIngredientesProveedor = new List<ProveedorIngrediente>();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        ProveedorIngrediente aux = new ProveedorIngrediente();
                        aux.id = Convert.ToString(objDataReader["idIngrediente"]);
                        aux.precio = (decimal)objDataReader["precio"];
                        prov.listadeIngredientesProveedor.Add(aux);
                    }
                }
                return prov;
            }
            catch (Exception ex)
            {
                log.Error("Get_ListadeIngredientesxProveedor(EXCEPTION): ", ex);
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

        public void AñadirIngredientes (ProveedorxIngredienteBean ProvxIngre)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for(int i=0; i< ProvxIngre.listadeIngredientesProveedor.Count;i++)
                {
                    if (ProvxIngre.listadeIngredientesProveedor[i].precio>0)
                    {
                        String strQuery = "Insert into Proveedor_x_Producto (idProveedor,idIngrediente,precio) values " +
                                            "(@idproveedor,@idingrediente,@precio)";

                        SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                        Utils.agregarParametro(objQuery, "@idproveedor", ProvxIngre.idProveedor);
                        Utils.agregarParametro(objQuery, "@idingrediente", ProvxIngre.listadeIngredientesProveedor[i].id);
                        Utils.agregarParametro(objQuery, "@precio", ProvxIngre.listadeIngredientesProveedor[i].precio);
                        objQuery.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("AñadirIngredientesxProveedor(EXCEPTION): ", ex);
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

        public void ModificarIngredientes(ProveedorxIngredienteBean ProvxIngre)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();
                for (int i = 0; i < ProvxIngre.listadeIngredientesProveedor.Count; i++)
                {
                    String strQuery = "Update Proveedor_x_Producto SET precio = @precio where idProveedor=@idproveedor and idIngrediente=@idingrediente ";

                    SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                    Utils.agregarParametro(objQuery, "@idproveedor", ProvxIngre.idProveedor);
                    Utils.agregarParametro(objQuery, "@idingrediente", ProvxIngre.listadeIngredientesProveedor[i].id);
                    Utils.agregarParametro(objQuery, "@precio", ProvxIngre.listadeIngredientesProveedor[i].precio);
                    objQuery.ExecuteNonQuery();

                }


            }
            catch (Exception ex)
            {
                log.Error("Modificar IngredientesxProveedor(EXCEPTION): ", ex);
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
        #endregion


    }
}