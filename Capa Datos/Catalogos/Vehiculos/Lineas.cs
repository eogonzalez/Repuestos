using System;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos.Vehiculos;
using Capa_Objetos.General;
using System.Data;

namespace Capa_Datos.Catalogos.Vehiculos
{

    public class Lineas
    {
        General.Conexion objConexion = new General.Conexion();

        public CO_Respuesta SelectLineas()
        {
            var objRespuesta = new CO_Respuesta();

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

        public CO_Respuesta SelectLineas(int id_linea, bool combo = false)
        {
            var objRespuesta = new CO_Respuesta();

            var sql_query = string.Empty;

            if (combo)
            {
                sql_query = "select id_marca, id_modelo, Linea " +
                " from lineas " +
                " where id_linea = @id_linea; ";
            }
            else
            {
                sql_query = " select A.id_linea, concat(B.Marca,' - ', A.Linea,' - ',C.modelo) as linea " +
                " from lineas A " +
                " inner join marcas B on " +
                " A.id_marca = B.id_marca " +
                " inner join modelos C on " +
                " A.id_modelo = c.id_modelo; ";
            }

            

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);

                    if (combo)
                    {
                        comando.Parameters.AddWithValue("id_linea", id_linea);
                    }
                    

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
        
        public CO_Respuesta SelectLineas(int id_marca)
        {
            var objRespuesta = new CO_Respuesta();

            var sql_query = string.Empty;


            sql_query = "select id_linea, linea "+
            " from Lineas "+
            " where id_marca = @id_marca; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_marca", id_marca);
                    

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

        public CO_Respuesta InsertLinea(CO_Lineas objLineas)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;                    
                }
                
            }

            return objRespuesta;
        }

        public CO_Respuesta UpdateLinea(CO_Lineas objLineas)
        {
            CO_Respuesta objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;                    
                }
                
            }

            return objRespuesta;
        }

        public CO_Respuesta DeleteLinea(int id_linea)
        {
            CO_Respuesta objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
