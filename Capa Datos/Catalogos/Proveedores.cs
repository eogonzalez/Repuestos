using System;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos;
using Capa_Objetos.General;
using System.Data;

namespace Capa_Datos.Catalogos
{
    public class Proveedores
    {
        General.Conexion objConexion = new General.Conexion();
                
        public CO_Respuesta SelectProveedores()
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = "select id_proveedor, nombre_proveedor, correo from proveedores; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);

                    var dataAdapter = new SqlDataAdapter(comando);
                    var tabla = new DataTable();
                    dataAdapter.Fill(tabla);
                    objRespuesta.DataTableRespuesta = tabla;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }

            return objRespuesta;
        }

        public CO_Respuesta SelectProveedores(int id_proveedor)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = "select nit, nombre_proveedor,direccion,telefono,correo " +
                " from proveedores " +
                " where " +
                " id_proveedor = @id_proveedor; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_proveedor", id_proveedor);

                    var dataAdapter = new SqlDataAdapter(comando);
                    var tabla = new DataTable();
                    dataAdapter.Fill(tabla);
                    objRespuesta.DataTableRespuesta = tabla;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }

            return objRespuesta;
        }

        public CO_Respuesta GuardarFormulario(CO_Proveedores objProveedores)
        {            
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[proveedores] " +
                        " ([nit],[nombre_proveedor],[direccion] " +
                        " ,[telefono],[correo]) " +
                        " VALUES " +
                        " (@nit, @nombre_proveedor, @direccion " +
                        " , @telefono, @correo)";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("nit", objProveedores.Nit);
                comando.Parameters.AddWithValue("nombre_proveedor", objProveedores.Nombre_Proveedor);
                comando.Parameters.AddWithValue("direccion", objProveedores.Direccion);
                comando.Parameters.AddWithValue("telefono", objProveedores.Telefono);
                comando.Parameters.AddWithValue("correo", objProveedores.Correo);

                try
                {
                    //Se abre la sesion para transaccion
                    conecta.Open();
                    //Ejecuta la consulta
                    comando.ExecuteScalar();                    
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;                    
                }
                
            }

            return objRespuesta;
        }

        public CO_Respuesta ActualizarProveedor(CO_Proveedores objProveedores)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[proveedores] " +
                    " SET[nit] = @nit " +
                    " ,[nombre_proveedor] = @nombre_proveedor " +
                    " ,[direccion] = @direccion " +
                    " ,[telefono] = @telefono " +
                    " ,[correo] = @correo " +
                    " WHERE id_proveedor = @id_proveedor";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_proveedor", objProveedores.ID_Proveedor);
                comando.Parameters.AddWithValue("nit", objProveedores.Nit);
                comando.Parameters.AddWithValue("nombre_proveedor", objProveedores.Nombre_Proveedor);
                comando.Parameters.AddWithValue("direccion", objProveedores.Direccion);
                comando.Parameters.AddWithValue("telefono", objProveedores.Telefono);
                comando.Parameters.AddWithValue("correo", objProveedores.Correo);

                try
                {
                    //Se abre la sesion para transaccion
                    conecta.Open();
                    //Ejecuta la consulta
                    comando.ExecuteScalar();
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }

            return objRespuesta;
            
        }

        public CO_Respuesta DeleteProveedor(int id_proveedor)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[proveedores] "+
                    " WHERE id_proveedor = @id_proveedor ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_proveedor", id_proveedor);

                try
                {

                    //Se abre la sesion para transaccion
                    conecta.Open();
                    //Ejecuta la consulta
                    comando.ExecuteScalar();
                    objRespuesta.BoolRespuesta = true;

                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }

            }

            return objRespuesta;
        }
    }
}
