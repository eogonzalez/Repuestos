using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Objetos.Administracion.Inventario;
using System.Data.SqlClient;

namespace Capa_Datos.Administracion.Inventario
{
    public class Compras
    {
        General.Conexion objConexion = new General.Conexion();

        public DataTable SelectDetalleCompras(int id_compra = 0)
        {
            var respuesta = new DataTable();
            var sql_query = string.Empty;

            sql_query = " SELECT a.[correlativo],a.[id_compra],a.[numero_compra],a.[serie],"+
                " b.nombre as repuesto,[cantidad],[precio],[subtotal] "+
                " FROM[dbo].[compras_detalle] A "+
                " join Produtos B on "+
                " a.id_producto = b.id_producto "+
                " WHERE id_compra = @id_compra ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_compra", id_compra);

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

        public int InsertEncabezadoCompra(CO_Compras objCompras)
        {
            var respuesta = 0;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[compras_encabezado] "+
                " ([numero_compra],[serie],[id_proveedor],[fecha_compra],[estado]) "+
                " VALUES "+
                " (@numero_compra, @serie, @id_proveedor, @fecha_compra,@estado); "+
                " select SCOPE_IDENTITY(); ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("numero_compra", objCompras.NumeroCompra);
                comando.Parameters.AddWithValue("serie", objCompras.Serie);
                comando.Parameters.AddWithValue("id_proveedor", objCompras.Id_Proveedor);
                comando.Parameters.AddWithValue("fecha_compra", objCompras.Fecha_Compra);
                comando.Parameters.AddWithValue("estado","ABIERTO");

                try
                {
                    conecta.Open();
                    int id_compra = 0;
                    id_compra = Convert.ToInt32(comando.ExecuteScalar());
                    respuesta = id_compra;
                }
                catch (Exception)
                {

                    throw;
                }
            }


            return respuesta;
        }

        public bool InsertDetalleCompra(CO_Compras objCompras)
        {
            var respuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[compras_detalle] "+
                " ([id_compra],[numero_compra],[serie] "+
                " ,[id_producto],[cantidad],[precio],[subtotal]) "+
                " VALUES "+
                " (@id_compra, @numero_compra, @serie "+
                " , @id_producto, @cantidad, @precio, @subtotal) ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_compra", objCompras.Id_Compra);
                comando.Parameters.AddWithValue("numero_compra", objCompras.NumeroCompra);
                comando.Parameters.AddWithValue("serie", objCompras.Serie);
                comando.Parameters.AddWithValue("id_producto", objCompras.Id_Producto);
                comando.Parameters.AddWithValue("cantidad", objCompras.Cantidad);
                comando.Parameters.AddWithValue("precio", objCompras.Precio);
                comando.Parameters.AddWithValue("subtotal", objCompras.SubTotal);

                try
                {
                    conecta.Open();
                    comando.ExecuteScalar();
                    respuesta = true;                    
                }
                catch (Exception)
                {
                    respuesta = false;
                    throw;
                }

                /*Sumo Subtotal*/
                sql_query = "select sum(subtotal) as total " +
                    " from compras_detalle " +
                    " where id_compra = @id_compra ";

                var total = 0.00;
                var comando_total = new SqlCommand(sql_query, conecta);
                comando_total.Parameters.AddWithValue("id_compra", objCompras.Id_Compra);
                try
                {
                    /*conecta.Open();*/
                    /*Ejecuto Query*/
                    total = Convert.ToDouble(comando_total.ExecuteScalar());
                    respuesta = true;
                }
                catch (Exception)
                {
                    respuesta = false;
                    throw;
                }
                

                /*Actualizo Total*/
                sql_query = "UPDATE [dbo].[compras_encabezado]" +
                    " SET [total] = @total " +
                    " WHERE id_compra = @id_compra; ";
                var comando_up = new SqlCommand(sql_query, conecta);
                comando_up.Parameters.AddWithValue("total", total);
                comando_up.Parameters.AddWithValue("id_compra", objCompras.Id_Compra);

                try
                {
                    /*Ejecuto Query*/
                    /*conecta.Open();*/
                    comando_up.ExecuteNonQuery();
                    respuesta = true;
                }
                catch (Exception)
                {
                    respuesta = false;
                    throw;
                }
                

            }


            return respuesta;
        }

        public DataTable SelectCompra()
        {
            var respuesta = new DataTable();
            var sql_query = string.Empty;
            sql_query = "select A.id_compra,  CONCAT(a.numero_compra,' - ',A.serie) as compra, b.nombre_proveedor, A.fecha_compra, cast(A.total as decimal(10,2)) as total, A.estado " +
                " from compras_encabezado A " +
                " join proveedores B " +
                " on A.id_proveedor = B.id_proveedor; ";

            using (var conexion = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conexion);                   
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

        public DataTable SelectCompra(int id_compra)
        {
            var respuesta = new DataTable();
            var sql_query = string.Empty;
            sql_query = "select numero_compra, serie, id_proveedor, fecha_compra,cast(total as decimal(10,2)) as total " +
                " from compras_encabezado  " +                
                " where id_compra = @id_compra; ";

            using (var conexion = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conexion);
                    comando.Parameters.AddWithValue("id_compra",id_compra);

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

        public bool UpdateEncabezadoCompra(CO_Compras objCompras)
        {
            var respuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[compras_encabezado] "+
                " SET[numero_compra] = @numero_compra "+
                " ,[serie] = @serie "+
                " ,[id_proveedor] = @id_proveedor "+
                " ,[fecha_compra] = @fecha_compra "+
                " WHERE id_compra = @id_compra;";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("numero_compra", objCompras.NumeroCompra);
                comando.Parameters.AddWithValue("serie", objCompras.Serie);
                comando.Parameters.AddWithValue("id_proveedor", objCompras.Id_Proveedor);
                comando.Parameters.AddWithValue("fecha_compra", objCompras.Fecha_Compra);
                comando.Parameters.AddWithValue("id_compra", objCompras.Id_Compra);                

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
