using System;
using Capa_Datos.Catalogos.Repuestos;
using Capa_Objetos.Catalogos.Repuestos;
using System.Data;

namespace Capa_Negocio.Catalogos.Repuestos
{
    public class CN_Productos
    {
        Productos obj_Datos_Productos = new Productos();

        public DataTable SelectProductos()
        {
            return obj_Datos_Productos.SelectProductos();
        }

        public DataTable SelectProductos(int id_producto)
        {
            return obj_Datos_Productos.SelectProductos(id_producto);
        }

        public bool InsertProducto(CO_Productos objProducto)
        {
            return obj_Datos_Productos.InsertProducto(objProducto);
        }

        public bool UpdateProducto(CO_Productos objProducto)
        {
            return obj_Datos_Productos.UpdateProducto(objProducto);
        }

        public bool DeleteProducto(int id_producto)
        {
            return obj_Datos_Productos.DeleteProducto(id_producto);
        }

    }
}
