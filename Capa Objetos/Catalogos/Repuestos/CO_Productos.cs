using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Objetos.Catalogos.Repuestos
{
    public class CO_Productos
    {
        public int Id_Producto { get; set; }
        public int Id_Categoria { get; set; }
        public int Id_Vehiculo { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
    }
}
