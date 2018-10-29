using Capa_Datos.Catalogos.Repuestos;
using Capa_Objetos.Catalogos.Repuestos;
using Capa_Objetos.General;

namespace Capa_Negocio.Catalogos.Repuestos
{
    public class CN_Productos
    {
        Productos obj_Datos_Productos = new Productos();

        public CO_Respuesta SelectProductos()
        {
            return obj_Datos_Productos.SelectProductos();
        }

        public CO_Respuesta SelectProductos(int id_producto)
        {
            return obj_Datos_Productos.SelectProductos(id_producto);
        }

        public CO_Respuesta InsertProducto(CO_Productos objProducto)
        {
            return obj_Datos_Productos.InsertProducto(objProducto);
        }

        public CO_Respuesta UpdateProducto(CO_Productos objProducto)
        {
            return obj_Datos_Productos.UpdateProducto(objProducto);
        }

        public CO_Respuesta DeleteProducto(int id_producto)
        {
            return obj_Datos_Productos.DeleteProducto(id_producto);
        }

    }
}
