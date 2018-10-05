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
    
    public class CN_Proveedores
    {
        Proveedores obj_Datos_Proveedores = new Proveedores();

        public DataTable SelectProveedores()
        {
            return obj_Datos_Proveedores.SelectProveedores();
        }

        public DataTable SelectProveedores(int id_proveedor)
        {
            return obj_Datos_Proveedores.SelectProveedores(id_proveedor);
        }

        public Boolean GuardarFormulario(CO_Proveedores objProveedores)
        {
            return obj_Datos_Proveedores.GuardarFormulario(objProveedores);
        }

        public Boolean ActualizarProveedor(CO_Proveedores objProveedores)
        {
            return obj_Datos_Proveedores.ActualizarProveedor(objProveedores);
        }

        public Boolean DeleteProveedor(int id_proveedor)
        {
            return obj_Datos_Proveedores.DeleteProveedor(id_proveedor);
        }
    }
}
