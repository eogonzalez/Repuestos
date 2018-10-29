using System;
using System.Data.SqlClient;
using Capa_Objetos.Catalogos.Vehiculos;
using Capa_Objetos.General;
using System.Data;

namespace Capa_Datos.Catalogos.Vehiculos
{
    public class Vehiculos
    {
        General.Conexion objConexion = new General.Conexion();

        public CO_Respuesta SelectVehiculos(bool combo = false)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            if (!combo)
            {
                sql_query = " select A.Id_Vehiculo, B.Marca, C.modelo, D.Linea, E.Tipo " +
                    " from vehiculos A " +
                    " inner join marcas B on " +
                    " A.id_marca = B.id_marca " +
                    " inner join modelos C on " +
                    " A.id_modelo = c.id_modelo " +
                    " inner join lineas D on " +
                    " A.id_linea = D.id_linea " +
                    " inner join tipo_vehiculo E on " +
                    " A.id_tipo_vehiculo = E.id_tipo_vehiculo; ";
            }
            else
            {
                sql_query = " select A.Id_Vehiculo, CONCAT(B.Marca,' ',D.Linea,' ',C.modelo,' ', E.Tipo ) as vehiculo "+
                    " from vehiculos A "+
                    " inner join marcas B on "+
                    " A.id_marca = B.id_marca "+
                    " inner join modelos C on "+
                    " A.id_modelo = c.id_modelo "+
                    " inner join lineas D on "+
                    " A.id_linea = D.id_linea "+
                    " inner join tipo_vehiculo E on "+
                    " A.id_tipo_vehiculo = E.id_tipo_vehiculo; ";
            }



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

        public CO_Respuesta SelectVehiculos(int id_vehiculo)
        {
            var objRespuesta = new CO_Respuesta();
            var sql_query = string.Empty;

            sql_query = "select id_marca, id_modelo, id_linea, id_tipo_vehiculo, descripcion " +
                " from vehiculos " +
                " where id_vehiculo = @id_vehiculo; ";

            using (var conecta = objConexion.Conectar())
            {
                try
                {
                    var comando = new SqlCommand(sql_query, conecta);
                    comando.Parameters.AddWithValue("id_vehiculo", id_vehiculo);

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

        public CO_Respuesta InsertVehiculo(CO_Vehiculos objVehiculos)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " INSERT INTO [dbo].[Vehiculos] " +
                " ([id_marca],[id_modelo],[id_linea],[id_tipo_vehiculo],[descripcion]) " +
                " VALUES " +
                " (@id_marca, @id_modelo, @id_linea, @id_tipo_vehiculo, @descripcion)";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_marca", objVehiculos.Id_Marca);
                comando.Parameters.AddWithValue("id_modelo", objVehiculos.Id_Modelo);
                comando.Parameters.AddWithValue("id_linea", objVehiculos.Id_Linea);
                comando.Parameters.AddWithValue("id_tipo_vehiculo", objVehiculos.Id_Tipo_Vehiculo);
                comando.Parameters.AddWithValue("descripcion", objVehiculos.Descripcion);

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

        public CO_Respuesta UpdateVehiculo(CO_Vehiculos objVehiculos)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " UPDATE [dbo].[vehiculos] " +
                    " SET [id_marca] = @id_marca " +
                    " ,[id_modelo] = @id_modelo" +
                    " ,[id_linea] = @id_linea" +
                    " ,[id_tipo_vehiculo] = @id_tipo_vehiculo" +
                    " ,[descripcion] = @descripcion" +
                    " WHERE id_vehiculo = @id_vehiculo";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_marca", objVehiculos.Id_Marca);
                comando.Parameters.AddWithValue("id_modelo", objVehiculos.Id_Modelo);
                comando.Parameters.AddWithValue("id_linea", objVehiculos.Id_Linea);
                comando.Parameters.AddWithValue("id_tipo_vehiculo", objVehiculos.Id_Tipo_Vehiculo);
                comando.Parameters.AddWithValue("descripcion", objVehiculos.Descripcion);
                comando.Parameters.AddWithValue("id_vehiculo", objVehiculos.Id_Vehiculo);

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

        public CO_Respuesta DeleteVehiculo(int id_vehiculo)
        {
            var objRespuesta = new CO_Respuesta();
            objRespuesta.BoolRespuesta = false;
            var sql_query = string.Empty;

            sql_query = " DELETE FROM [dbo].[vehiculos] " +
                    " WHERE id_vehiculo = @id_vehiculo ";

            using (var conecta = objConexion.Conectar())
            {
                var comando = new SqlCommand(sql_query, conecta);
                comando.Parameters.AddWithValue("id_vehiculo", id_vehiculo);

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
