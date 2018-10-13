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

        public Boolean GuardarFormulario(CO_Clientes objClientes)
        {
            return obj_Datos_Clientes.GuardarFormulario(objClientes);
        }
    }
}
