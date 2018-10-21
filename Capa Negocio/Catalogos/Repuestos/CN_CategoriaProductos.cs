using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos.Catalogos.Repuestos;
using Capa_Objetos.Catalogos.Repuestos;
using System.Data;

namespace Capa_Negocio.Catalogos.Repuestos
{
    public class CN_CategoriaProductos
    {
        CategoriaProductos obj_Datos_Categoria = new CategoriaProductos();
        public DataTable SelectCategorias()
        {
            return obj_Datos_Categoria.SelectCategorias();
        }

        public DataTable SelectCategorias(int id_categoria)
        {
            return obj_Datos_Categoria.SelectCategorias(id_categoria);
        }

        public bool InsertCategoria(CO_CategoriaProductos objCategoria)
        {
            return obj_Datos_Categoria.InsertCategoria(objCategoria);
        }

        public bool UpdateCategoria(CO_CategoriaProductos objCategoria)
        {
            return obj_Datos_Categoria.UpdateCategoria(objCategoria);
        }

        public bool DeleteCategoria(int id_categoria)
        {
            return obj_Datos_Categoria.DeleteCategoria(id_categoria);
        }

    }
}
