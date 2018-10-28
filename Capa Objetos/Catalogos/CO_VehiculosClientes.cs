using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Objetos.Catalogos
{
    public class CO_VehiculosClientes
    {
        public int Id_Vehiculo_Cliente { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Vehiculo { get; set; }
        public string Placa { get; set; }
        public string Color { get; set; }
        public int Kilometraje { get; set; }
    }
}
