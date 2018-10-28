using Capa_Datos.Catalogos;
using System.Data;
using Capa_Objetos.Catalogos;

namespace Capa_Negocio.Catalogos
{


    public class CN_VehiculosClientes
    {
        VehiculosClientes obj_Negocio_VehiculoClientes = new VehiculosClientes();

        public DataTable SelectVehiculosClientes(int id_cliente)
        {
            return obj_Negocio_VehiculoClientes.SelectVehiculosClientes(id_cliente);
        }

        public DataTable SelectVehiculoClienteDetalle(int id_vehiculoCliente)
        {
            return obj_Negocio_VehiculoClientes.SelectVehiculoClienteDetalle(id_vehiculoCliente);
        }

        public bool InsertVehiculoCliente(CO_VehiculosClientes objVehiculoCliente)
        {
            return obj_Negocio_VehiculoClientes.InsertVehiculoCliente(objVehiculoCliente);
        }

        public bool UpdateVehiculoCliente(CO_VehiculosClientes objVehiculoCliente)
        {
            return obj_Negocio_VehiculoClientes.UpdateVehiculoCliente(objVehiculoCliente);
        }

        public bool DeleteVehiculoCliente(int id_vehiculoCliente)
        {
            return obj_Negocio_VehiculoClientes.DeleteVehiculoCliente(id_vehiculoCliente);
        }
    }
}
