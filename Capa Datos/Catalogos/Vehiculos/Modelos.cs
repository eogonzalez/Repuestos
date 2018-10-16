using System;
using System.Data;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos.Vehiculos;

namespace Capa_Datos.Catalogos.Vehiculos
{
    public class Modelos
    {
        General.Conexion objConexion = new General.Conexion();

        public DataTable SelectModelos()
        {
            var respuesta = new DataTable();

            var sql_query = string.Empty;

            sql_query = " select id_modelo, modelo " +
                " from modelos; ";

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

        public DataTable SelectModelos(int id_modelo)
        {
            var respuesta = new DataTable();

            var sql_query = string.Empty;

            sql_query = "select modelo " +
                " from modelos " +
                " where id_modelo = @id_modelo; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_modelo", id_modelo);

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

        public bool InsertModelo(CO_Modelos objModelos)
        {
            var respuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Modelos] " +
                " ([modelo]) " +
                " VALUES " +
                " (@modelo)";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);                
                comando.Parameters.AddWithValue("modelo", objModelos.Modelo);                

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

        public bool UpdateModelo(CO_Modelos objModelos)
        {
            Boolean respuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[modelos] " +
                    " SET [modelo] = @modelo" +                    
                    " WHERE id_modelo = @id_modelo";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);                
                comando.Parameters.AddWithValue("id_modelo", objModelos.Id_Modelo);
                comando.Parameters.AddWithValue("modelo", objModelos.Modelo);

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

        public bool DeleteModelo(int id_modelo)
        {
            Boolean respuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[modelos] " +
                    " WHERE id_modelo = @id_modelo ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_modelo", id_modelo);

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
