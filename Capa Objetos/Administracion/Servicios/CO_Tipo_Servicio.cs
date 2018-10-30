using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Objetos.Administracion.Servicios
{
    public class CO_Tipo_Servicio
    {
        public int Id_Tipo_Servicio { get; set; }
        public string TipoServicio { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public decimal Porcentaje_Ganancia { get; set; }
    }
}
