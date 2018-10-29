using System;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos.Vehiculos;
using Capa_Objetos.General;
using System.Data;

namespace Capa_Datos.Catalogos.Vehiculos
{
    public class Modelos
    {
        General.Conexion objConexion = new General.Conexion();

        public CO_Respuesta SelectModelos()
        {
            var objRespuesta = new CO_Respuesta();

            var sql_query = string.Empty;

            sql_query = " select id_modelo, modelo " +
                " from modelos; ";

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

        public CO_Respuesta SelectModelos(int id_linea)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " select a.id_modelo, modelo "+
                " from Modelos A "+
                " join Lineas B on "+
                " A.id_modelo = B.id_modelo "+
                " where B.id_linea = @id_linea; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_linea", id_linea);

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

        public CO_Respuesta InsertModelo(CO_Modelos objModelos)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }

            }

            return objRespuesta;
        }

        public CO_Respuesta UpdateModelo(CO_Modelos objModelos)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }

            }

            return objRespuesta;
        }

        public CO_Respuesta DeleteModelo(int id_modelo)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
