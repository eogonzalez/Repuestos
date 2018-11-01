using Capa_Objetos.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                " A.id_tipo_servicio = D.tipo_servicio; ";

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

    }
}
