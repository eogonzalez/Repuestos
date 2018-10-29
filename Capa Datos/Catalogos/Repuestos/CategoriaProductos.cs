using System;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos.Repuestos;
using Capa_Objetos.General;
using System.Data;

namespace Capa_Datos.Catalogos.Repuestos
{
    public class CategoriaProductos
    {
        General.Conexion objConexion = new General.Conexion();

        public CO_Respuesta SelectCategorias()
        {
            var objRespuesta = new CO_Respuesta();            
            var sql_query = string.Empty;

            sql_query = " select id_categoria, nombre, Descripcion " +
                " from categoria_productos; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    var dataTable = new DataTable();
                    var dataAdapter = new SqlDataAdapter(comando);
                    dataAdapter.Fill(dataTable);
                    objRespuesta.DataTableRespuesta = dataTable;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;                    
                }
            }

            return objRespuesta;
        }

        public CO_Respuesta SelectCategorias(int id_categoria)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = "select nombre, Descripcion " +
                " from categoria_productos " +
                " where id_categoria = @id_categoria; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_categoria", id_categoria);

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

        public CO_Respuesta InsertCategoria(CO_CategoriaProductos objCategoria)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Categoria_Productos] " +
                " ([nombre],[Descripcion]) " +
                " VALUES " +
                " (@nombre, @descripcion)";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("nombre", objCategoria.Categoria);
                comando.Parameters.AddWithValue("descripcion", objCategoria.Descripcion);

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

        public CO_Respuesta UpdateCategoria(CO_CategoriaProductos objCategoria)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[categoria_productos] " +
                    " SET [nombre] = @nombre " +
                    " ,[descripcion] = @descripcion" +
                    " WHERE id_categoria = @id_categoria";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_categoria", objCategoria.Id_Categoria);
                comando.Parameters.AddWithValue("nombre", objCategoria.Categoria);
                comando.Parameters.AddWithValue("descripcion", objCategoria.Descripcion);

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

        public CO_Respuesta DeleteCategoria(int id_categoria)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[categoria_productos] " +
                    " WHERE id_categoria = @id_categoria ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_categoria", id_categoria);

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
