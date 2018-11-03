using Capa_Objetos.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Objetos.Administracion.Servicios;
using Capa_Objetos.Administracion.Facturacion;

namespace Capa_Datos.Administracion.Servicios
{
    public class Servicios
    {
        General.Conexion objConexion = new General.Conexion();

        public CO_Respuesta SelectServicios()
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = "select "+
                " A.id_servicio,  b.nombres,  "+
	            " CONCAT(BB.Marca, ' ', DD.Linea, ' ', CC.modelo, ' ', EE.Tipo) as vehiculo,  "+
	            " D.tipo_servicio, A.fecha_ingreso, A.costo_total, A.estado "+
                " from "+
                " Servicio_Encabezado A "+
                " inner join clientes B on "+
                " A.id_cliente = B.id_cliente "+
                " inner join Vehiculos_Clientes C on "+
                " a.id_vehiculo_cliente = c.id_vehiculo_cliente "+
                " inner join Vehiculos AA on "+
                " C.id_vehiculo = aa.id_vehiculo "+
                " inner join Marcas BB on "+
                " AA.id_marca = BB.id_marca "+
                " inner join modelos CC on "+
                " AA.id_modelo = CC.id_modelo "+
                " inner join lineas DD on "+
                " AA.id_linea = DD.id_linea "+
                " inner join tipo_vehiculo EE on "+
                " AA.id_tipo_vehiculo = EE.id_tipo_vehiculo "+
                " inner join Tipo_Servicio D on "+
                " A.id_tipo_servicio = D.id_tipo_servicio; ";

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

        public CO_Respuesta DeleteServicio(int id_servicio)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM compras_externo_detalle " +
                " where id_servicio = @id_servicio ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_servicio", id_servicio);

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

                sql_query = " DELETE FROM compras_repuesto_detalle " +
                " where id_servicio = @id_servicio ";

                var comando_rep = new SqlCommand(sql_query, conecta);
                comando_rep.Parameters.AddWithValue("id_servicio", id_servicio);

                try
                {
                    comando_rep.ExecuteScalar();
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
                }

                sql_query = " DELETE FROM servicio_encabezado " +
                    " where id_servicio = @id_servicio ";
                var comando_enc = new SqlCommand(sql_query, conecta);
                comando_enc.Parameters.AddWithValue("id_servicio", id_servicio);

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

        public CO_Respuesta InsertEncabezadoServicio(CO_Servicios objServicio)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.IntRespuesta = 0;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Servicio_Encabezado] "+
                " ([id_cliente],[id_vehiculo_cliente],[id_tipo_servicio] "+
                " ,[fecha_ingreso],[kilometraje_servicio],[costo_servicio] "+
                " ,[costo_total],[estado]) "+
                " VALUES "+
                " (@id_cliente, @id_vehiculo_cliente, @id_tipo_servicio "+
                ", @fecha_ingreso, @kilometraje_servicio, @costo_servicio "+
                " , @costo_total, @estado); "+
                " select SCOPE_IDENTITY();";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_cliente", objServicio.Id_Cliente);
                comando.Parameters.AddWithValue("id_vehiculo_cliente", objServicio.Id_Vehiculo_Cliente);
                comando.Parameters.AddWithValue("id_tipo_servicio", objServicio.Id_TipoServicio);
                comando.Parameters.AddWithValue("fecha_ingreso", objServicio.Fecha_Ingreso_Servicio);
                comando.Parameters.AddWithValue("kilometraje_servicio", objServicio.Kilometraje_Ingreso_Servicio);
                comando.Parameters.AddWithValue("costo_servicio", objServicio.CostoServicio);
                comando.Parameters.AddWithValue("costo_total", objServicio.CostoTotal);
                comando.Parameters.AddWithValue("estado", "ABIERTO");

                try
                {
                    conecta.Open();
                    int id_servicio = 0;
                    id_servicio = Convert.ToInt32(comando.ExecuteScalar());
                    objRespuesta.IntRespuesta = id_servicio;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }

            return objRespuesta;
        }

        public CO_Respuesta SelectServicios(int id_servicio)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = "SELECT [id_cliente] ,[id_vehiculo_cliente] "+
                " ,[id_tipo_servicio],[fecha_ingreso],[kilometraje_servicio] "+
                " ,[costo_servicio],[costo_total],[estado] "+
                " FROM[dbo].[Servicio_Encabezado] "+
                " where id_servicio = @id_servicio ";

            using (var conexion = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conexion);
                    comando.Parameters.AddWithValue("id_servicio", id_servicio);

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

        public CO_Respuesta UpdateEncabezadoServicios(CO_Servicios objServicio)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[Servicio_Encabezado] " +
                " SET [id_cliente] = @id_cliente "+
                " ,[id_vehiculo_cliente] = @id_vehiculo_cliente "+
                " ,[id_tipo_servicio] = @id_tipo_servicio "+
                " ,[fecha_ingreso] = @fecha_ingreso "+
                " ,[kilometraje_servicio] = @kilometraje_servicio "+
                " ,[costo_servicio] = @costo_servicio "+
                " ,[costo_total] = @costo_total "+
                " WHERE id_servicio = @id_servicio; ";

            using (var conecta = objConexion.Conectar())
            {
                var comando1 = new SqlCommand(sql_query, conecta);
                comando1.Parameters.AddWithValue("id_cliente", objServicio.Id_Cliente);
                comando1.Parameters.AddWithValue("id_vehiculo_cliente", objServicio.Id_Vehiculo_Cliente);
                comando1.Parameters.AddWithValue("id_tipo_servicio", objServicio.Id_TipoServicio);
                comando1.Parameters.AddWithValue("fecha_ingreso", objServicio.Fecha_Ingreso_Servicio);
                comando1.Parameters.AddWithValue("kilometraje_servicio", objServicio.Kilometraje_Ingreso_Servicio);
                comando1.Parameters.AddWithValue("costo_servicio", objServicio.CostoServicio);
                comando1.Parameters.AddWithValue("costo_total", objServicio.CostoTotal);
                comando1.Parameters.AddWithValue("id_servicio", objServicio.Id_Servicio);

                try
                {
                    conecta.Open();
                    comando1.ExecuteScalar();
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }

            return objRespuesta;
        }

        public CO_Respuesta InsertDetalleRepuesto(CO_Servicios objServicio)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Servicio_Repuesto_Detalle] "+
                " ([id_servicio],[id_producto],[cantidad] "+
                " ,[precio_venta],[sub_total]) "+
                " VALUES "+
                " (@id_servicio, @id_producto, @cantidad "+
                " , @precio_venta, @sub_total);";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_servicio", objServicio.Id_Servicio);
                comando.Parameters.AddWithValue("id_producto", objServicio.Id_Producto);
                comando.Parameters.AddWithValue("cantidad", objServicio.Cantidad);
                comando.Parameters.AddWithValue("precio_venta", objServicio.PrecioVenta);
                comando.Parameters.AddWithValue("sub_total", objServicio.SubTotal);
               
                try
                {
                    conecta.Open();                    
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

        public CO_Respuesta InsertDetalleServicio(CO_Servicios objServicio)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Servicio_Externo_Detalle] "+
                " ([id_servicio],[descripcion],[precio]) "+
                " VALUES "+
                " (@id_servicio, @descripcion, @precio);";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_servicio", objServicio.Id_Servicio);
                comando.Parameters.AddWithValue("descripcion", objServicio.Descripcion);
                comando.Parameters.AddWithValue("precio", objServicio.PrecioServicio);
                
                try
                {
                    conecta.Open();
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

        public CO_Respuesta SelectDetalleRepuestos(int id_servicio, bool panel = false)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            if (!panel)
            {
                sql_query = " select A.corr_servicio_repuesto, Concat(b.nombre,' - ',b.marca) as producto, " +
                    " cantidad, precio_venta, sub_total " +
                    " from Servicio_Repuesto_Detalle A " +
                    " join Produtos B on " +
                    " A.id_producto = B.id_producto " +
                    " WHERE " +
                    " A.id_servicio = @id_servicio; ";
            }
            else
            {
                sql_query = " select id_producto, cantidad, precio_venta "+
                    " from Servicio_Repuesto_Detalle "+
                    " where corr_servicio_repuesto = @id_servicio; ";
            }


            using (var conexion = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conexion);
                    comando.Parameters.AddWithValue("id_servicio", id_servicio);

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

        public CO_Respuesta SelectDetalleServicios(int id_servicio, bool panel = false)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            if (!panel)
            {
                sql_query = " select corr_servicio_externo, descripcion, precio " +
                " from Servicio_Externo_Detalle " +
                " where id_servicio = @id_servicio; ";
            }
            else
            {
                sql_query = " SELECT [descripcion],[precio] "+
                    " FROM[dbo].[Servicio_Externo_Detalle] "+
                    " where corr_servicio_externo = @id_servicio ";
            }
            

            using (var conexion = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conexion);
                    comando.Parameters.AddWithValue("id_servicio", id_servicio);

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

        public CO_Respuesta UpdateDetalleRepuestos(CO_Servicios objServicio)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[Servicio_Repuesto_Detalle] "+
                " SET [id_producto] = @id_producto "+
                " ,[cantidad] = @cantidad "+
                " ,[precio_venta] = @precio_venta "+
                " ,[sub_total] = @sub_total "+
                " WHERE corr_servicio_repuesto = @corr_servicio_repuesto ; ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_producto", objServicio.Id_Producto);
                comando.Parameters.AddWithValue("cantidad", objServicio.Cantidad);
                comando.Parameters.AddWithValue("precio_venta", objServicio.PrecioVenta);
                comando.Parameters.AddWithValue("sub_total", objServicio.SubTotal);
                
                try
                {
                    conecta.Open();
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

        public CO_Respuesta UpdateDetalleServicioExterno(CO_Servicios objServicio)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[Servicio_Externo_Detalle] "+
                " SET [descripcion] = @descripcion "+
                " ,[precio] = @precio "+
                " WHERE corr_servicio_externo = @corr_servicio_externo; ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("descripcion", objServicio.Descripcion);
                comando.Parameters.AddWithValue("precio", objServicio.PrecioServicio);
                comando.Parameters.AddWithValue("corr_servicio_externo", objServicio.Corr_ServicioExterno);
                
                try
                {
                    conecta.Open();
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

        public CO_Respuesta SelectSubTotalRepuesto(int id_servicio)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " select SUM(sub_total) as subtotal "+
                " from Servicio_Repuesto_Detalle "+
                " where id_servicio = @id_servicio; ";

            using (var conexion = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conexion);
                    comando.Parameters.AddWithValue("id_servicio", id_servicio);
                    conexion.Open();
                    objRespuesta.DecimalRespuesta= (decimal)comando.ExecuteScalar();                                        
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }

            }

            return objRespuesta;
        }

        public CO_Respuesta SelectSubTotalServicioExterno(int id_servicio)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " select sum(precio) as subtotal "+
                " from Servicio_Externo_Detalle "+
                " where id_servicio = @id_servicio; ";

            using (var conexion = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conexion);
                    comando.Parameters.AddWithValue("id_servicio", id_servicio);
                    conexion.Open();
                    objRespuesta.DecimalRespuesta = (decimal)comando.ExecuteScalar();
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }

            }

            return objRespuesta;
        }

        public CO_Respuesta DeleteDetalleRepuesto(int correlativo_repuesto)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[Servicio_Repuesto_Detalle] "+
                " WHERE corr_servicio_repuesto = @corr_servicio_repuesto ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("corr_servicio_repuesto", correlativo_repuesto);

                try
                {
                    conecta.Open();
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

        public CO_Respuesta DeleteDetalleServicioExterno(int correlativo_servicio)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[Servicio_Externo_Detalle] " +
                " WHERE corr_servicio_externo = @corr_servicio_externo ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("corr_servicio_externo", correlativo_servicio);

                try
                {
                    conecta.Open();
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

        public CO_Respuesta CerrarServicio(CO_Facturacion objFacturacion, CO_Servicios objServicio)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            var obj_Datos_Factura = new Administracion.Facturacion.Facturacion();
          
            /*Selecciono repuestos y servicios*/
            sql_query = " select 'R' as tipo, corr_servicio_repuesto, id_producto, cantidad, precio_venta, sub_total " +
                " from Servicio_Repuesto_Detalle "+
                " where id_servicio = @id_servicio1 "+
                " union "+
                " select 'S' as tipo, corr_servicio_externo, corr_servicio_externo, 1 as cantidad, precio, precio " +
                " from Servicio_Externo_Detalle "+
                " where id_servicio = @id_servicio2; ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_servicio1", objServicio.Id_Servicio);
                comando.Parameters.AddWithValue("id_servicio2", objServicio.Id_Servicio);

                var TablaProductos = new DataTable();
                var dataAdapter = new SqlDataAdapter(comando);
                dataAdapter.Fill(TablaProductos);

                try
                {
                    /*Creo encabezado de factura*/
                    objRespuesta = obj_Datos_Factura.InsertEncabezadoFactura(objFacturacion);
                    objFacturacion.Id_Factura = objRespuesta.IntRespuesta;
                }
                catch (Exception e)
                {

                    objRespuesta.MensajeRespuesta = e.Message;
                }


                conecta.Open();

                //Recorro productos y servicios y los agrego al inventario y al detalle de factura
                foreach (DataRow row in TablaProductos.Rows)
                {

                    int id_producto = Convert.ToInt32(row["id_producto"].ToString());
                    int cantidad = Convert.ToInt32(row["cantidad"].ToString());
                    decimal precio_costo = Convert.ToDecimal(row["precio_venta"].ToString());
                    decimal porcentaje_ganacia = 0.00m;
                    decimal precio_venta = Convert.ToDecimal(row["precio_venta"].ToString());

                    if (row["tipo"].ToString() == "R")
                    {
                        sql_query = " INSERT INTO [dbo].[Inventarios] " +
                        " ([tipo_movimiento],[id_compra_servicio],[id_producto] " +
                        " ,[cantidad],[precio_costo],[porcentaje_ganancia],[precio_venta]) " +
                        " VALUES " +
                        " (@tipo_movimiento, @id_compra_servicio, @id_producto " +
                        " , @cantidad, @precio_costo, @porcentaje_ganancia, @precio_venta) ";

                        var comando_insert = new SqlCommand(sql_query, conecta);
                        comando_insert.Parameters.AddWithValue("tipo_movimiento", 2);
                        comando_insert.Parameters.AddWithValue("id_compra_servicio", objServicio.Id_Servicio);
                        comando_insert.Parameters.AddWithValue("id_producto", id_producto);
                        comando_insert.Parameters.AddWithValue("cantidad", cantidad*-1);
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

                    objFacturacion.Tipo = row["tipo"].ToString();
                    objFacturacion.Id_Producto_Servicio = id_producto;
                    objFacturacion.Cantidad = cantidad;
                    objFacturacion.Precio = precio_venta;
                    objFacturacion.SubTotal = Convert.ToDecimal(row["sub_total"].ToString());

                    try
                    {
                        objRespuesta = obj_Datos_Factura.InsertDetalleFactura(objFacturacion);
                    }
                    catch (Exception e)
                    {
                        objRespuesta.MensajeRespuesta = e.Message;
                    }

                }

                /*Actualizo Encabezado de Servicio*/
                sql_query = "UPDATE [dbo].[servicio_encabezado]" +
                    " SET [estado] = @estado " +
                    " WHERE id_servicio = @id_servicio; ";
                var comando_up = new SqlCommand(sql_query, conecta);
                comando_up.Parameters.AddWithValue("estado", "CERRADO");
                comando_up.Parameters.AddWithValue("id_servicio", objServicio.Id_Servicio);

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
