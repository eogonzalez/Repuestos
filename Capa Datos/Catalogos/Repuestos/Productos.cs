using System;
using Capa_Objetos.Catalogos.Repuestos;
using System.Data.SqlClient;
using Capa_Objetos.General;
using System.Data;

namespace Capa_Datos.Catalogos.Repuestos
{
    public class Productos
    {
        General.Conexion objConexion = new General.Conexion();

        public CO_Respuesta SelectProductos()
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " select AA.id_producto, Bb.nombre as categoria, CONCAT(b.Marca,' ',d.Linea,' ',e.Tipo,' ',c.modelo) as vehiculo, aa.nombre as repuesto, aa.marca, aa.descripcion, "+
                " concat(aa.nombre,' ',aa.marca,' -',b.Marca,' ',d.Linea,' ',e.Tipo,' ',c.modelo) as valor_combo" +
                " from Produtos AA " +
                " join Categoria_Productos BB on "+
                " aa.id_categoria = bb.id_categoria "+
                " join Vehiculos A on "+
                " aa.id_vehiculo = a.id_vehiculo "+
                " join Marcas B on "+
                " a.id_marca = b.id_marca "+
                " join Modelos C on "+
                " a.id_modelo = c.id_modelo "+
                " join Lineas D on "+
                " a.id_linea = D.id_linea "+
                " join Tipo_Vehiculo E on "+
                " a.id_tipo_vehiculo = e.id_tipo_vehiculo; ";

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

        public CO_Respuesta SelectProductos(int id_producto)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = "select id_categoria, id_vehiculo, nombre, marca, descripcion "+
                " from Produtos "+
                " where id_producto = @id_producto; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_producto", id_producto);

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

        public CO_Respuesta InsertProducto(CO_Productos objProducto)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Produtos] "+
                " ([id_categoria],[id_vehiculo] "+
                " ,[nombre],[marca],[descripcion]) "+
                " VALUES "+
                " (@id_categoria,@id_vehiculo "+
                " ,@nombre,@marca,@descripcion)";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_categoria", objProducto.Id_Categoria);
                comando.Parameters.AddWithValue("id_vehiculo", objProducto.Id_Vehiculo);
                comando.Parameters.AddWithValue("nombre", objProducto.Nombre);
                comando.Parameters.AddWithValue("marca", objProducto.Marca);
                comando.Parameters.AddWithValue("descripcion", objProducto.Descripcion);

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

        public CO_Respuesta UpdateProducto(CO_Productos objProducto)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[Produtos] "+
                " SET [id_categoria] = @id_categoria "+
                " ,[id_vehiculo] = @id_vehiculo "+
                " ,[nombre] = @nombre "+
                " ,[marca] = @marca "+
                " ,[descripcion] = @descripcion "+
                " WHERE id_producto = @id_producto";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_categoria", objProducto.Id_Categoria);
                comando.Parameters.AddWithValue("id_vehiculo", objProducto.Id_Vehiculo);
                comando.Parameters.AddWithValue("nombre", objProducto.Nombre);
                comando.Parameters.AddWithValue("marca", objProducto.Marca);
                comando.Parameters.AddWithValue("descripcion", objProducto.Descripcion);
                comando.Parameters.AddWithValue("id_producto", objProducto.Id_Producto);

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

        public CO_Respuesta DeleteProducto(int id_producto)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[productos] " +
                    " WHERE id_producto = @id_producto ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_producto", id_producto);

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
