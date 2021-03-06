﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Objetos.Administracion.Inventario;
using System.Data.SqlClient;
using Capa_Objetos.General;

namespace Capa_Datos.Administracion.Inventario
{
    public class Compras
    {
        General.Conexion objConexion = new General.Conexion();

        public CO_Respuesta SelectDetalleCompras(int id_compra = 0)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " SELECT a.[correlativo],a.[id_compra],a.[numero_compra],a.[serie],"+
                " b.nombre as repuesto,[cantidad],convert(numeric(28,2),[precio]) as precio ,convert(numeric(28,2),[subtotal] ) as subtotal " +
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

        public CO_Respuesta InsertEncabezadoCompra(CO_Compras objCompras)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.IntRespuesta = 0;
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
                    objRespuesta.IntRespuesta = id_compra;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }

            return objRespuesta;
        }

        public CO_Respuesta InsertDetalleCompra(CO_Compras objCompras)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
                }                
            }

            return objRespuesta;
        }

        public CO_Respuesta SelectCompra()
        {
            var objRespuesta = new CO_Respuesta();
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

        public CO_Respuesta SelectCompra(int id_compra)
        {
            var objRespuesta = new CO_Respuesta();
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

        public CO_Respuesta UpdateEncabezadoCompra(CO_Compras objCompras)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }

            return objRespuesta;
        }

        public CO_Respuesta CerrarCompra(int id_compra)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
                        objRespuesta.BoolRespuesta = true;
                    }
                    catch (Exception e)
                    {
                        objRespuesta.BoolRespuesta = false;
                        objRespuesta.MensajeRespuesta = e.Message;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }

            return objRespuesta;
        }

        public CO_Respuesta DeleteCompra(int id_compra)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
                }

                sql_query = " DELETE FROM compras_encabezado " +
                    " where id_compra = @id_compra ";
                var comando_enc = new SqlCommand(sql_query, conecta);
                comando_enc.Parameters.AddWithValue("id_compra", id_compra);

                try
                {
                    comando_enc.ExecuteScalar();
                    objRespuesta.BoolRespuesta = true;    
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }

            return objRespuesta;
        }

        public CO_Respuesta UpdateDetalleCompra(CO_Compras objCompras)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }

            return objRespuesta;
        }

        public CO_Respuesta SelectDetalleCompraProducto(int id_correlativo)
        {
            var objRespuesta = new CO_Respuesta();
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

        public CO_Respuesta DeleteDetalleCompra(int id_correlativo, int id_compra)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
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
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }
            return objRespuesta;
        }
    }
}
