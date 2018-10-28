using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Objetos.General
{
    public class CO_Respuesta
    {
        public bool BoolRespuesta { get; set; }
        public DataTable DataTableRespuesta { get; set; }
        public string MensajeRespuesta { get; set; }
        public int IntRespuesta { get; set; }
    }
}
