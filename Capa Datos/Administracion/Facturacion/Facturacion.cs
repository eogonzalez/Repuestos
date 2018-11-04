using Capa_Objetos.Administracion.Facturacion;
using Capa_Objetos.General;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos.Administracion.Facturacion
{
    public class Facturacion
    {
        General.Conexion objConexion = new General.Conexion();

        public CO_Respuesta InsertEncabezadoFactura(CO_Facturacion objFacturacion)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.IntRespuesta = 0;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Factura_Encabezado] " +
                " ([numero_factura],[serie] " +
                " ,[id_cliente],[fecha_factura],[estado],[costo_servicio]) " +
                " VALUES " +
                " (@numero_factura, @serie " +
                " , @id_cliente, @fecha_factura, @estado,@costo_servicio); " +
                " select SCOPE_IDENTITY(); ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("numero_factura", objFacturacion.Numero_Factura);
                comando.Parameters.AddWithValue("serie", objFacturacion.Serie);
                comando.Parameters.AddWithValue("id_cliente", objFacturacion.Id_Cliente);
                comando.Parameters.AddWithValue("fecha_factura", objFacturacion.FechaFactura);
                comando.Parameters.AddWithValue("costo_servicio", objFacturacion.CostoServicio);
                comando.Parameters.AddWithValue("estado", "CERRADO");

                try
                {
                    conecta.Open();
                    int id_factura = 0;
                    id_factura = Convert.ToInt32(comando.ExecuteScalar());
                    objRespuesta.IntRespuesta = id_factura;
                }
                catch (Exception e)
                {
                    objRespuesta.MensajeRespuesta = e.Message;
                }
            }

            return objRespuesta;
        }

        public CO_Respuesta InsertDetalleFactura(CO_Facturacion objFacturacion)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Factura_Detalle] "+
                " ([id_factura],[numero_factura] "+
                " ,[serie],[tipo],[id_producto_servicio] "+
                " ,[cantidad],[precio],[subtotal]) "+
                " VALUES "+
                " (@id_factura, @numero_factura "+
                " , @serie, @tipo, @id_producto_servicio "+
                " , @cantidad, @precio, @subtotal); ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_factura", objFacturacion.Id_Factura);
                comando.Parameters.AddWithValue("numero_factura", objFacturacion.Numero_Factura);
                comando.Parameters.AddWithValue("serie", objFacturacion.Serie);
                comando.Parameters.AddWithValue("tipo", objFacturacion.Tipo);
                comando.Parameters.AddWithValue("id_producto_servicio", objFacturacion.Id_Producto_Servicio);
                comando.Parameters.AddWithValue("cantidad", objFacturacion.Cantidad);
                comando.Parameters.AddWithValue("precio", objFacturacion.Precio);
                comando.Parameters.AddWithValue("subtotal", objFacturacion.SubTotal);

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
                    " from factura_detalle " +
                    " where id_factura = @id_factura ";

                var total = 0.00m;
                var comando_total = new SqlCommand(sql_query, conecta);
                comando_total.Parameters.AddWithValue("id_factura", objFacturacion.Id_Factura);
                try
                {
                    /*conecta.Open();*/
                    /*Ejecuto Query*/
                    total = Convert.ToDecimal(comando_total.ExecuteScalar());
                    objRespuesta.BoolRespuesta = true;
                }
                catch (Exception e)
                {
                    objRespuesta.BoolRespuesta = false;
                    objRespuesta.MensajeRespuesta = e.Message;
                }


                /*Actualizo Total*/
                sql_query = "UPDATE [dbo].[Factura_Encabezado]" +
                    " SET [total] = @total " +
                    " WHERE id_factura = @id_factura; ";
                var comando_up = new SqlCommand(sql_query, conecta);
                comando_up.Parameters.AddWithValue("total", total+objFacturacion.CostoServicio);
                comando_up.Parameters.AddWithValue("id_factura", objFacturacion.Id_Factura);

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

        public CO_Respuesta SelectFactura()
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = "select id_factura, CONCAT(numero_factura, ' - ', serie) as numero_factura, fecha_factura, total, estado "+
                " from Factura_Encabezado ";

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

        public CO_Respuesta SelectFactura(int id_factura)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = "select numero_factura, serie, id_cliente, fecha_factura, total "+
                " from Factura_Encabezado "+
                " where id_factura = @id_factura; ";

            using (var conexion = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conexion);
                    comando.Parameters.AddWithValue("id_factura", id_factura);

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

        public CO_Respuesta SelectDetalleFactura(int id_factura)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " select fd.correlativo,fd.cantidad,FD.tipo, "+
            " descripcion = case FD.tipo when 'R' then concat(p.nombre, ' - ', p.marca) else SE.descripcion end, "+
            " fd.precio, fd.subtotal , fe.numero_factura, fe.serie, fe.total, fe.fecha_factura, cl.nombres, cl.nit, cl.direccion,FE.costo_servicio " +
            " from Factura_Detalle FD "+
            " left join Produtos P on "+
            " fd.id_producto_servicio = p.id_producto "+
            " left join Servicio_Externo_Detalle SE on "+
            " fd.id_producto_servicio = SE.corr_servicio_externo "+
            " left join Factura_Encabezado FE "+
            " on FD.id_factura = FE.id_factura "+
            " left join clientes CL "+
            " on FE.id_cliente = CL.id_cliente "+
            " where FD.id_factura = @id_factura ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_factura", id_factura);

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

    }
}
