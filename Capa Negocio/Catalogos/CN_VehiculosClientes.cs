using Capa_Datos.Catalogos;
using Capa_Objetos.Catalogos;
using Capa_Objetos.General;

namespace Capa_Negocio.Catalogos
{


    public class CN_VehiculosClientes
    {
        VehiculosClientes obj_Negocio_VehiculoClientes = new VehiculosClientes();

        public CO_Respuesta SelectVehiculosClientes(int id_cliente)
        {
            return obj_Negocio_VehiculoClientes.SelectVehiculosClientes(id_cliente);
        }

        public CO_Respuesta SelectVehiculoClienteDetalle(int id_vehiculoCliente)
        {
            return obj_Negocio_VehiculoClientes.SelectVehiculoClienteDetalle(id_vehiculoCliente);
        }

        public CO_Respuesta InsertVehiculoCliente(CO_VehiculosClientes objVehiculoCliente)
        {
            return obj_Negocio_VehiculoClientes.InsertVehiculoCliente(objVehiculoCliente);
        }

        public CO_Respuesta UpdateVehiculoCliente(CO_VehiculosClientes objVehiculoCliente)
        {
            return obj_Negocio_VehiculoClientes.UpdateVehiculoCliente(objVehiculoCliente);
        }

        public CO_Respuesta DeleteVehiculoCliente(int id_vehiculoCliente)
        {
            return obj_Negocio_VehiculoClientes.DeleteVehiculoCliente(id_vehiculoCliente);
        }
    }
}
