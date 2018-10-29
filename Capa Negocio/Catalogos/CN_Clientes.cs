using Capa_Datos.Catalogos;
using Capa_Objetos.Catalogos;
using Capa_Objetos.General;

namespace Capa_Negocio.Catalogos
{

    public class CN_Clientes
    {
        Clientes obj_Datos_Clientes = new Clientes();

        public CO_Respuesta SelectClientes()
        {
            return obj_Datos_Clientes.SelectClientes();
        }

        public CO_Respuesta SelectClientes(int id_cliente)
        {
            return obj_Datos_Clientes.SelectClientes(id_cliente);
        }

        public CO_Respuesta GuardarFormulario(CO_Clientes objClientes)
        {
            return obj_Datos_Clientes.GuardarFormulario(objClientes);
        }

        public CO_Respuesta UpdateCliente(CO_Clientes objClientes)
        {
            return obj_Datos_Clientes.UpdateCliente(objClientes);
        }

        public CO_Respuesta DeleteCliente(int id_cliente)
        {
            return obj_Datos_Clientes.DeleteCliente(id_cliente);
        }

    }
}
