using System;
using System.Data;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos.Repuestos;

namespace Capa_Datos.Catalogos.Repuestos
{
    public class CategoriaProductos
    {
        General.Conexion objConexion = new General.Conexion();

        public DataTable SelectCategorias()
        {
            var respuesta = new DataTable();

            var sql_query = string.Empty;

            sql_query = " select id_categoria, nombre, Descripcion " +
                " from categoria_productos; ";

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

        public DataTable SelectCategorias(int id_categoria)
        {
            var respuesta = new DataTable();

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
                    dataAdapter.Fill(respuesta);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return respuesta;
        }

        public bool InsertCategoria(CO_CategoriaProductos objCategoria)
        {
            var respuesta = false;
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

                    respuesta = true;
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return respuesta;
        }

        public bool UpdateCategoria(CO_CategoriaProductos objCategoria)
        {
            Boolean respuesta = false;
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
                    respuesta = true;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return respuesta;
        }

        public bool DeleteCategoria(int id_categoria)
        {
            Boolean respuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[catetoria_productos] " +
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
