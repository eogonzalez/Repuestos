using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Objetos.Catalogos.Repuestos;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos.Catalogos.Repuestos
{
    public class Productos
    {
        General.Conexion objConexion = new General.Conexion();

        public DataTable SelectProductos()
        {
            var respuesta = new DataTable();

            var sql_query = string.Empty;

            sql_query = " select AA.id_producto, Bb.nombre as categoria, CONCAT(b.Marca,' ',d.Linea,' ',e.Tipo,' ',c.modelo) as vehiculo, aa.nombre as repuesto, aa.marca, aa.descripcion "+
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
                    dataAdapter.Fill(respuesta);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return respuesta;
        }

        public DataTable SelectProductos(int id_producto)
        {
            var respuesta = new DataTable();

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
                    dataAdapter.Fill(respuesta);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return respuesta;
        }

        public bool InsertProducto(CO_Productos objProducto)
        {
            var respuesta = false;
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
                    respuesta = true;
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return respuesta;
        }

        public bool UpdateProducto(CO_Productos objProducto)
        {
            Boolean respuesta = false;
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
                    respuesta = true;
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return respuesta;
        }

        public bool DeleteProducto(int id_producto)
        {
            Boolean respuesta = false;
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
