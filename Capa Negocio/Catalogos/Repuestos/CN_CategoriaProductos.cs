using Capa_Datos.Catalogos.Repuestos;
using Capa_Objetos.Catalogos.Repuestos;
using Capa_Objetos.General;

namespace Capa_Negocio.Catalogos.Repuestos
{
    public class CN_CategoriaProductos
    {
        CategoriaProductos obj_Datos_Categoria = new CategoriaProductos();
        public CO_Respuesta SelectCategorias()
        {
            return obj_Datos_Categoria.SelectCategorias();
        }

        public CO_Respuesta SelectCategorias(int id_categoria)
        {
            return obj_Datos_Categoria.SelectCategorias(id_categoria);
        }

        public CO_Respuesta InsertCategoria(CO_CategoriaProductos objCategoria)
        {
            return obj_Datos_Categoria.InsertCategoria(objCategoria);
        }

        public CO_Respuesta UpdateCategoria(CO_CategoriaProductos objCategoria)
        {
            return obj_Datos_Categoria.UpdateCategoria(objCategoria);
        }

        public CO_Respuesta DeleteCategoria(int id_categoria)
        {
            return obj_Datos_Categoria.DeleteCategoria(id_categoria);
        }

    }
}
