using System;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos;
using Capa_Objetos.General;
using System.Data;

namespace Capa_Datos.Catalogos
{
    public class VehiculosClientes
    {
        General.Conexion objConexion = new General.Conexion();

        public CO_Respuesta SelectVehiculosClientes(int id_cliente)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " select A.id_vehiculo_cliente, CONCAT(C.Marca,' ',E.Linea,' ',D.modelo,' ', F.Tipo ) as vehiculo,  "+
                " A.placa, A.color, A.kilometraje "+
                " from Vehiculos_Clientes A "+
                " join Vehiculos B "+
                " on A.id_vehiculo = B.id_vehiculo "+
                " join Marcas C "+
                " on B.id_marca = c.id_marca "+
                " join Modelos D "+
                " on B.id_modelo = d.id_modelo "+
                " join Lineas E "+
                " on B.id_linea = e.id_linea "+
                " join Tipo_Vehiculo F "+
                " on B.id_tipo_vehiculo = f.id_tipo_vehiculo "+
                " WHERE A.id_cliente = @id_cliente; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_cliente", id_cliente);

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

        public CO_Respuesta SelectVehiculoClienteDetalle(int id_vehiculoCliente)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = " SELECT [id_vehiculo_cliente] "+
                " ,[id_cliente],[id_vehiculo],[placa] "+
                " ,[color],[kilometraje] "+
                " FROM[dbo].[Vehiculos_Clientes] "+
                " where id_vehiculo_cliente = @id_vehiculo_cliente ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_vehiculo_cliente", id_vehiculoCliente);

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

        public CO_Respuesta InsertVehiculoCliente(CO_VehiculosClientes objVehiculoCliente)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Vehiculos_Clientes] " +
                " ([id_cliente],[id_vehiculo],[placa] " +
                " ,[color],[kilometraje]) " +
                " VALUES " +
                " (@id_cliente, @id_vehiculo, @placa " +
                " , @color, @kilometraje) ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_cliente", objVehiculoCliente.Id_Cliente);
                comando.Parameters.AddWithValue("id_vehiculo", objVehiculoCliente.Id_Vehiculo);
                comando.Parameters.AddWithValue("placa", objVehiculoCliente.Placa);
                comando.Parameters.AddWithValue("color", objVehiculoCliente.Color);
                comando.Parameters.AddWithValue("kilometraje", objVehiculoCliente.Kilometraje);

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

        public CO_Respuesta UpdateVehiculoCliente(CO_VehiculosClientes objVehiculoCliente)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[Vehiculos_Clientes] "+
                " SET [id_vehiculo] = @id_vehiculo "+
                " ,[placa] = @placa "+
                " ,[color] = @color "+
                " ,[kilometraje] = @kilometraje "+
                " WHERE id_vehiculo_cliente = @id_vehiculo_cliente";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_vehiculo", objVehiculoCliente.Id_Vehiculo);
                comando.Parameters.AddWithValue("placa", objVehiculoCliente.Placa);
                comando.Parameters.AddWithValue("color", objVehiculoCliente.Color);
                comando.Parameters.AddWithValue("kilometraje", objVehiculoCliente.Kilometraje);
                comando.Parameters.AddWithValue("id_vehiculo_cliente", objVehiculoCliente.Id_Vehiculo_Cliente);
                
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

        public CO_Respuesta DeleteVehiculoCliente(int id_vehiculoCliente)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[vehiculos_clientes] " +
                    " WHERE id_vehiculo_cliente = @id_vehiculo_cliente ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_vehiculo_cliente", id_vehiculoCliente);

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
