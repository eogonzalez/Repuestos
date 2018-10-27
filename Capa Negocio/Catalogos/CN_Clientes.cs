using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos.Catalogos;
using System.Data;
using Capa_Objetos.Catalogos;

namespace Capa_Negocio.Catalogos
{
    
    public class CN_Clientes
    {
        Clientes obj_Datos_Clientes = new Clientes();

        public DataTable SelectClientes()
        {
            return obj_Datos_Clientes.SelectClientes();
        }

        public DataTable SelectClientes(int id_cliente)
        {
            return obj_Datos_Clientes.SelectClientes(id_cliente);
        }

        public Boolean GuardarFormulario(CO_Clientes objClientes)
        {
            return obj_Datos_Clientes.GuardarFormulario(objClientes);
        }

        public bool UpdateCliente(CO_Clientes objClientes)
        {
            return obj_Datos_Clientes.UpdateCliente(objClientes);
        }

        public bool DeleteCliente(int id_cliente)
        {
            return obj_Datos_Clientes.DeleteCliente(id_cliente);
        }

    }
}
