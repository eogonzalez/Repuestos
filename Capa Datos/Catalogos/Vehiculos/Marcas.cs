using System;
using System.Data;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos.Vehiculos;
using Capa_Objetos.General;

namespace Capa_Datos.Catalogos.Vehiculos
{
    public class Marcas
    {
        General.Conexion objConexion = new General.Conexion();

        public DataTable SelectMarcas()
        {
            var respuesta = new DataTable();

            var sql_query = string.Empty;

            sql_query = " select id_marca, Marca, Descripcion " +
                " from marcas; ";

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

        public DataTable SelectMarcas(int id_marca)
        {
            var respuesta = new DataTable();

            var sql_query = string.Empty;

            sql_query = "select marca, Descripcion " +
                " from Marcas " +
                " where id_marca = @id_marca; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_marca", id_marca);

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

        public CO_Respuesta InsertMarca(CO_Marcas objMarcas)
        {
            var objRespuesta = new CO_Respuesta();
            var respuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Marcas] " +
                " ([marca],[Descripcion]) " +
                " VALUES " +
                " (@marca, @descripcion)";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("marca", objMarcas.Marca);
                comando.Parameters.AddWithValue("descripcion", objMarcas.Descripcion);                

                try
                {
                    //Se abre la sesion para transaccion
                    conecta.Open();
                    //Ejecuta la consulta
                    comando.ExecuteScalar();

                    respuesta = true;

                    objRespuesta.BoolRespuesta = respuesta;
                }
                catch (Exception e)
                {

                    objRespuesta.MensajeRespuesta = e.Message;
                }

            }

            return objRespuesta;
        }

        public bool UpdateMarca(CO_Marcas objMarcas)
        {
            Boolean respuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[marcas] " +
                    " SET [marca] = @marca " +
                    " ,[descripcion] = @descripcion" +                    
                    " WHERE id_marca = @id_marca";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);                
                comando.Parameters.AddWithValue("id_marca", objMarcas.Id_Marca);
                comando.Parameters.AddWithValue("marca", objMarcas.Marca);
                comando.Parameters.AddWithValue("descripcion", objMarcas.Descripcion);
                
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

        public bool DeleteMarca(int id_marca)
        {
            Boolean respuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[marcas] " +
                    " WHERE id_marca = @id_marca ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_marca", id_marca);

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
