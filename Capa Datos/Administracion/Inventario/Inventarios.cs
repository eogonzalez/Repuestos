using Capa_Objetos.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Administracion.Inventario
{
    public class Inventarios
    {
        General.Conexion objConexion = new General.Conexion();

        public CO_Respuesta SelectProductoInventario(int id_producto)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " select sum(cantidad) as disponible, avg(precio_venta) as precio "+
                " from Inventarios "+
                " where id_producto = @id_producto; ";

            using (var conexion = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conexion);
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

        public CO_Respuesta SelectProductosInventario()
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = "select distinct AA.id_producto, concat(aa.nombre,' ',aa.marca,' -',b.Marca,' ',d.Linea,' ',e.Tipo,' ',c.modelo) as valor_combo "+
                " from Inventarios II "+
                " join Produtos AA  on "+
                " ii.id_producto = aa.id_producto "+
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
                " a.id_tipo_vehiculo = e.id_tipo_vehiculo "+
                " order by id_producto asc; ";

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

        public CO_Respuesta SelectInventarios()
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " Select inv.id_producto,p.nombre,sum(inv.cantidad) as disponible, convert(numeric(28,2),MAX(precio_venta)) as Precio_maximo " +
                        " from Inventarios inv " +
                        " inner join Produtos p " +
                        " on inv.id_producto = p.id_producto " +
                        " Group by inv.id_producto,p.nombre ";

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
    }
}
