using System;
using System.Data;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos.Vehiculos;

namespace Capa_Datos.Catalogos.Vehiculos
{
    public class TipoVehiculos
    {
        General.Conexion objConexion = new General.Conexion();

        public DataTable SelectTipoVehiculos()
        {
            var respuesta = new DataTable();

            var sql_query = string.Empty;

            sql_query = " select id_tipo_vehiculo, tipo " +
                " from tipo_vehiculo; ";

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

        public DataTable SelectTipoVehiculos(int id_tipo_vehiculo)
        {
            var respuesta = new DataTable();

            var sql_query = string.Empty;

            sql_query = "select tipo " +
                " from tipo_vehiculo " +
                " where id_tipo_vehiculo = @id_tipo_vehiculo; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_tipo_vehiculo", id_tipo_vehiculo);

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

        public bool InsertTipoVehiculo(CO_TipoVehiculos objTipoVehiculo)
        {
            var respuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Tipo_Vehiculo] " +
                " ([tipo]) " +
                " VALUES " +
                " (@tipo)";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("tipo", objTipoVehiculo.Tipo);

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

        public bool UpdateTipoVehiculo(CO_TipoVehiculos objTipoVehiculo)
        {
            Boolean respuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[tipo_vehiculo] " +
                    " SET [tipo] = @tipo" +
                    " WHERE id_tipo_vehiculo = @id_tipo_vehiculo";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_tipo_vehiculo", objTipoVehiculo.Id_Tipo_Vehiculo);
                comando.Parameters.AddWithValue("tipo", objTipoVehiculo.Tipo);

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

        public bool DeleteTipoVehiculo(int id_tipo_vehiculo)
        {
            Boolean respuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[tipo_vehiculo] " +
                    " WHERE id_tipo_vehiculo = @id_tipo_vehiculo ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_tipo_vehiculo", id_tipo_vehiculo);

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
