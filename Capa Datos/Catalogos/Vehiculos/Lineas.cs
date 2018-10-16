using System;
using System.Data;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos.Vehiculos;

namespace Capa_Datos.Catalogos.Vehiculos
{

    public class Lineas
    {
        General.Conexion objConexion = new General.Conexion();

        public DataTable SelectLineas()
        {
            var respuesta = new DataTable();

            var sql_query = string.Empty;

            sql_query = " select A.id_linea, B.Marca, C.modelo, A.Linea "+
                " from lineas A "+   
                " inner join marcas B on "+
                " A.id_marca = B.id_marca "+
                " inner join modelos C on "+
                " A.id_modelo = c.id_modelo; ";

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

        public DataTable SelectLineas(int id_linea)
        {
            var respuesta = new DataTable();

            var sql_query = string.Empty;

            sql_query = "select id_marca, id_modelo, Linea "+          
                " from lineas "+
                " where id_linea = @id_linea; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_linea", id_linea);

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

        public DataTable SelectLineasCBO()
        {
            var respuesta = new DataTable();

            var sql_query = string.Empty;

            sql_query = " select A.id_linea, concat(B.Marca,' - ', A.Linea,' - ',C.modelo) as linea " +
                " from lineas A " +
                " inner join marcas B on " +
                " A.id_marca = B.id_marca " +
                " inner join modelos C on " +
                " A.id_modelo = c.id_modelo; ";

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

        public bool InsertLinea(CO_Lineas objLineas)
        {
            var respuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Lineas] "+
                " ([id_marca],[id_modelo],[Linea]) "+
                " VALUES "+
                " (@id_marca, @id_modelo, @Linea)";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_marca", objLineas.Id_Marca);
                comando.Parameters.AddWithValue("id_modelo", objLineas.Id_Modelo);
                comando.Parameters.AddWithValue("linea", objLineas.Linea);

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

        public bool UpdateLinea(CO_Lineas objLineas)
        {
            Boolean respuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[lineas] " +
                    " SET [id_marca] = @id_marca " +
                    " ,[id_modelo] = @id_modelo" +
                    " ,[linea] = @linea" +                    
                    " WHERE id_linea = @id_linea";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_linea", objLineas.Id_Linea);
                comando.Parameters.AddWithValue("id_marca", objLineas.Id_Marca);
                comando.Parameters.AddWithValue("id_modelo", objLineas.Id_Modelo);
                comando.Parameters.AddWithValue("linea", objLineas.Linea);

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

        public bool DeleteLinea(int id_linea)
        {
            Boolean respuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[lineas] " +
                    " WHERE id_linea = @id_linea ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_linea", id_linea);

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
