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

            sql_query = " UPDATE[dbo].[compras_detalle] " +
                " SET[numero_compra] = @numero_compra " +
                " ,[serie] = @serie " +
                " WHERE id_compra = @id_compra; ";



            using (var conecta = objConexion.Conectar())
            {
                var comando1 = new SqlCommand(sql_query, conecta);
                comando1.Parameters.AddWithValue("numero_compra", objCompras.NumeroCompra);
                comando1.Parameters.AddWithValue("serie", objCompras.Serie);
                comando1.Parameters.AddWithValue("id_compra", objCompras.Id_Compra);

                try
                {
                    conecta.Open();
                    comando1.ExecuteScalar();
                    respuesta = true;
                }
                catch (Exception)
                {

                    throw;
                }


                sql_query = " UPDATE [dbo].[compras_encabezado] " +
                " SET[numero_compra] = @numero_compra " +
                " ,[serie] = @serie " +
                " ,[id_proveedor] = @id_proveedor " +
                " ,[fecha_compra] = @fecha_compra " +
                " WHERE id_compra = @id_compra;";


                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("numero_compra", objCompras.NumeroCompra);
                comando.Parameters.AddWithValue("serie", objCompras.Serie);
                comando.Parameters.AddWithValue("id_proveedor", objCompras.Id_Proveedor);
                comando.Parameters.AddWithValue("fecha_compra", objCompras.Fecha_Compra);
                comando.Parameters.AddWithValue("id_compra", objCompras.Id_Compra);

                try
                {
                    //Se abre la sesion para transaccion
                    //conecta.Open();
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

        public bool CerrarCompra(int id_compra)
        {
            var respuesta = false;
            var sql_query = string.Empty;

            /*Selecciono productos de compra*/
            sql_query = " select id_producto, cantidad, precio "+
                " from compras_detalle "+
                " where id_compra = @id_compra; ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_compra", id_compra);

                var TablaProductos = new DataTable();
                var dataAdapter = new SqlDataAdapter(comando);
                dataAdapter.Fill(TablaProductos);


                conecta.Open();

                foreach (DataRow row in TablaProductos.Rows)
                {
                    sql_query = " INSERT INTO [dbo].[Inventarios] "+
                        " ([tipo_movimiento],[id_compra_servicio],[id_producto] "+
                        " ,[cantidad],[precio_costo],[porcentaje_ganancia],[precio_venta]) "+
                        " VALUES "+
                        " (@tipo_movimiento, @id_compra_servicio, @id_producto "+
                        " , @cantidad, @precio_costo, @porcentaje_ganancia, @precio_venta) ";

                    var comando_insert = new SqlCommand(sql_query, conecta);
                    comando_insert.Parameters.AddWithValue("tipo_movimiento", 1);
                    comando_insert.Parameters.AddWithValue("id_compra_servicio", id_compra);

                    int id_producto = Convert.ToInt32(row["id_producto"].ToString());
                    int cantidad = Convert.ToInt32(row["cantidad"].ToString());
                    double precio_costo = Convert.ToDouble(row["precio"].ToString());
                    double porcentaje_ganacia = 0.10;
                    double precio_venta = (precio_costo * porcentaje_ganacia) + precio_costo;

                    comando_insert.Parameters.AddWithValue("id_producto", id_producto);
                    comando_insert.Parameters.AddWithValue("cantidad", cantidad);
                    comando_insert.Parameters.AddWithValue("precio_costo", precio_costo);
                    comando_insert.Parameters.AddWithValue("porcentaje_ganancia", porcentaje_ganacia);
                    comando_insert.Parameters.AddWithValue("precio_venta", precio_venta);

                    try
                    {

                        comando_insert.ExecuteScalar();
                        respuesta = true;
                    }
                    catch (Exception)
                    {
                        respuesta = false;
                        throw;
                    }

                }

                /*Actualizo Encabezado*/
                sql_query = "UPDATE [dbo].[compras_encabezado]" +
                    " SET [estado] = @estado " +
                    " WHERE id_compra = @id_compra; ";
                var comando_up = new SqlCommand(sql_query, conecta);
                comando_up.Parameters.AddWithValue("estado", "CERRADO");
                comando_up.Parameters.AddWithValue("id_compra", id_compra);

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

        public bool DeleteCompra(int id_compra)
        {
            var respuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM compras_detalle "+
                " where id_compra = @id_compra ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_compra", id_compra);

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

                sql_query = " DELETE FROM compras_encabezado " +
                    " where id_compra = @id_compra ";
                var comando_enc = new SqlCommand(sql_query, conecta);
                comando_enc.Parameters.AddWithValue("id_compra", id_compra);

                try
                {
                    comando_enc.ExecuteScalar();
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

        public bool UpdateDetalleCompra(CO_Compras objCompras)
        {
            var respuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[compras_detalle] "+
                " SET [numero_compra] = @numero_compra "+
                " ,[serie] = @serie "+
                " ,[id_producto] = @id_producto "+
                " ,[cantidad] = @cantidad "+
                " ,[precio] = @precio "+
                " ,[subtotal] = @subtotal "+
                " WHERE correlativo = @correlativo; ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("correlativo", objCompras.Id_Correlativo);
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

        public DataTable SelectDetalleCompraProducto(int id_correlativo)
        {
            var respuesta = new DataTable();
            var sql_query = string.Empty;

            sql_query = " SELECT [id_producto],[cantidad],[precio] " +
                " FROM[dbo].[compras_detalle] " +
                " where correlativo = @correlativo ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("correlativo", id_correlativo);

                try
                {
                    
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

        public bool DeleteDetalleCompra(int id_correlativo, int id_compra)
        {
            var respuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE [dbo].[compras_detalle] " +                
                " WHERE correlativo = @correlativo; ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("correlativo", id_correlativo);
                
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
                comando_total.Parameters.AddWithValue("id_compra", id_correlativo);
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
                comando_up.Parameters.AddWithValue("id_compra", id_compra);

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
    }
}
