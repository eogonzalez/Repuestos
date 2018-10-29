using Capa_Datos.Catalogos;
using Capa_Objetos.Catalogos;
using Capa_Objetos.General;

namespace Capa_Negocio.Catalogos
{

    public class CN_Proveedores
    {
        Proveedores obj_Datos_Proveedores = new Proveedores();

        public CO_Respuesta SelectProveedores()
        {
            return obj_Datos_Proveedores.SelectProveedores();
        }

        public CO_Respuesta SelectProveedores(int id_proveedor)
        {
            return obj_Datos_Proveedores.SelectProveedores(id_proveedor);
        }

        public CO_Respuesta GuardarFormulario(CO_Proveedores objProveedores)
        {
            return obj_Datos_Proveedores.GuardarFormulario(objProveedores);
        }

        public CO_Respuesta ActualizarProveedor(CO_Proveedores objProveedores)
        {
            return obj_Datos_Proveedores.ActualizarProveedor(objProveedores);
        }

        public CO_Respuesta DeleteProveedor(int id_proveedor)
        {
            return obj_Datos_Proveedores.DeleteProveedor(id_proveedor);
        }
    }
}
