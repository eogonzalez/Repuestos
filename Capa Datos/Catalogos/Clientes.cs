using System;
using System.Data;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos;
namespace Capa_Datos.Catalogos
{
    public class Clientes
    {
        General.Conexion objConexion = new General.Conexion();
        
        //Funcion publica que selecciona los datos de tabla de clientes
        public DataTable SelectClientes()
        {
            var respuesta = new DataTable();
            var sql_query = string.Empty;

            sql_query = "select id_cliente, nombres, correo from clientes; ";

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

        public DataTable SelectClientes(int id_cliente)
        {
            var respuesta = new DataTable();
            var sql_query = string.Empty;

            sql_query = "select nit, cui, pasaporte, nombres, apellidos, "+
                " direccion, telefono, celular, correo "+
                " from clientes "+
                " WHERE id_cliente = @id_cliente; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_cliente", id_cliente);

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

        public Boolean GuardarFormulario(CO_Clientes objClientes)
        {
            var respuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[clientes] " +
                        " ([nit],[nombres],[direccion] " +
                        " ,[telefono],[correo]) " +
                        " VALUES " +
                        " (@nit, @nombres, @direccion " +
                        " , @telefono, @correo)";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("nit", objClientes.Nit);
                comando.Parameters.AddWithValue("nombres", objClientes.Nombres);
                comando.Parameters.AddWithValue("direccion", objClientes.Direccion);
                comando.Parameters.AddWithValue("telefono", objClientes.Telefono);
                comando.Parameters.AddWithValue("correo", objClientes.Correo);

                //Se abre la sesion para transaccion
                conecta.Open();
                //Ejecuta la consulta
                comando.ExecuteScalar();

                respuesta = true;
            }


            return respuesta;
        }

        public bool UpdateCliente(CO_Clientes objClientes)
        {
            Boolean respuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[clientes] "+
                " SET [nit] = @nit "+
                " ,[nombres] = @nombres "+
                " ,[direccion] = @direccion "+
                " ,[telefono] = @telefono "+
                " ,[correo] = @correo "+
                " WHERE id_cliente = @id_cliente";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("nit", objClientes.Nit);
                comando.Parameters.AddWithValue("nombres", objClientes.Nombres);
                comando.Parameters.AddWithValue("direccion", objClientes.Direccion);
                comando.Parameters.AddWithValue("telefono", objClientes.Telefono);
                comando.Parameters.AddWithValue("correo", objClientes.Correo);
                comando.Parameters.AddWithValue("id_cliente", objClientes.ID_Cliente);

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

        public bool DeleteCliente(int id_cliente)
        {
            Boolean respuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[clientes] " +
                    " WHERE id_cliente = @id_cliente ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_cliente", id_cliente);

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
