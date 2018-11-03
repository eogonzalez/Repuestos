using Capa_Datos.Administracion.Inventario;
using Capa_Objetos.General;

namespace Capa_Negocio.Administracion.Inventario
{
    public class CN_Inventarios
    {
        Inventarios obj_Negocio_Inventarios = new Inventarios();
        public CO_Respuesta SelectProductoInventario(int id_producto)
        {
            return obj_Negocio_Inventarios.SelectProductoInventario(id_producto);
        }

        public CO_Respuesta SelectProductosInventario()
        {
            return obj_Negocio_Inventarios.SelectProductosInventario();
        }

        public CO_Respuesta SelectInventarios()
        {
            return obj_Negocio_Inventarios.SelectInventarios();
        }
    }
}
