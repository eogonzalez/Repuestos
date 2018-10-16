using System;
using System.Data;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos;
namespace Capa_Datos.Catalogos
{
    public class Proveedores
    {
        General.Conexion objConexion = new General.Conexion();
        
        //Funcion publica que selecciona los datos de tabla de proveedores
        public DataTable SelectProveedores()
        {
            var respuesta = new DataTable();
            var sql_query = string.Empty;

            sql_query = "select id_proveedor, nombre_proveedor, correo from proveedores; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);

                    var dataAdapter = new SqlDataAdapter(comando);
                    dataAdapter.Fill(respuesta);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return respuesta;
        }

        public DataTable SelectProveedores(int id_proveedor)
        {
            var respuesta = new DataTable();
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
                    dataAdapter.Fill(respuesta);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return respuesta;
        }

        public bool GuardarFormulario(CO_Proveedores objProveedores)
        {
            var respuesta = false;
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
                    respuesta = true;
                }
                catch (Exception)
                {

                    throw;
                }
                
            }

            return respuesta;
        }

        public bool ActualizarProveedor(CO_Proveedores objProveedores)
        {
            var respuesta = false;
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

                    respuesta = true;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return respuesta;
            
        }

        public bool DeleteProveedor(int id_proveedor)
        {
            Boolean respuesta = false;

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

                    respuesta = true;

                }
                catch (Exception)
                {

                    throw;
                }

            }

            return respuesta;
        }
    }
}
