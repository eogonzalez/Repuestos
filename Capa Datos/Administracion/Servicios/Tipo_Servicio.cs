using Capa_Objetos.Administracion.Servicios;
using Capa_Objetos.General;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos.Administracion.Servicios
{
    public class Tipo_Servicio
    {

        General.Conexion objConexion = new General.Conexion();

        public CO_Respuesta SelectTipoServicio()
        {
            CO_Respuesta objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " SELECT [id_tipo_servicio],[tipo_servicio] "+
                " ,[Descripcion],[costo],[porcentaje_ganancia] "+
                " FROM[dbo].[Tipo_Servicio] ";

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

        public CO_Respuesta SelectTipoServicio(int id_tipo_servicio)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " SELECT [tipo_servicio] "+
                " ,[Descripcion],[costo],[porcentaje_ganancia] "+
                " FROM[dbo].[Tipo_Servicio] "+
                " WHERE id_tipo_servicio = @id_tipo_servicio; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_tipo_servicio", id_tipo_servicio);

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

        public CO_Respuesta InsertTipoServicio(CO_Tipo_Servicio objTipoServicio)
        {
            var objRespuesta = new CO_Respuesta();
            var respuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Tipo_Servicio] "+
                " ([tipo_servicio],[Descripcion] "+
                " ,[costo],[porcentaje_ganancia]) "+
                " VALUES "+
                " (@tipo_servicio, @Descripcion "+
                " , @costo, @porcentaje_ganancia)";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("tipo_servicio", objTipoServicio.TipoServicio);
                comando.Parameters.AddWithValue("descripcion", objTipoServicio.Descripcion);
                comando.Parameters.AddWithValue("costo", objTipoServicio.Costo);
                comando.Parameters.AddWithValue("porcentaje_ganancia", objTipoServicio.Porcentaje_Ganancia);

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

        public CO_Respuesta UpdateTipoServicio(CO_Tipo_Servicio objTipoServicio)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[Tipo_Servicio] "+
                " SET [tipo_servicio] = @tipo_servicio "+
                " ,[Descripcion] = @Descripcion "+
                " ,[costo] = @costo "+
                " ,[porcentaje_ganancia] = @porcentaje_ganancia "+
                " WHERE id_tipo_servicio = @id_tipo_servicio ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("tipo_servicio", objTipoServicio.TipoServicio);
                comando.Parameters.AddWithValue("descripcion", objTipoServicio.Descripcion);
                comando.Parameters.AddWithValue("costo", objTipoServicio.Costo);
                comando.Parameters.AddWithValue("porcentaje_ganancia", objTipoServicio.Porcentaje_Ganancia);
                comando.Parameters.AddWithValue("id_tipo_servicio", objTipoServicio.Id_Tipo_Servicio);

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

        public CO_Respuesta DeleteTipoServicio(int id_tipo_servicio)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[tipo_servicio] " +
                    " WHERE id_tipo_servicio = @id_tipo_servicio ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_tipo_servicio", id_tipo_servicio);

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
