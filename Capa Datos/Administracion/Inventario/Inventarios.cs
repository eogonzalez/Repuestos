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
    }
}
