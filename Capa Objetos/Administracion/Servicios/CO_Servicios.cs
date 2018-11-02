using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Objetos.Administracion.Servicios
{
    public class CO_Servicios
    {
        public int Id_Servicio { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Vehiculo_Cliente { get; set; }
        public int Id_TipoServicio { get; set; }
        public DateTime Fecha_Ingreso_Servicio { get; set; }
        public decimal Kilometraje_Ingreso_Servicio { get; set; }
        public decimal CostoServicio { get; set; }
        public decimal CostoTotal { get; set; }
        public string Estado { get; set; }
        public int Corr_ServicioRepuesto { get; set; }
        public int Id_Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal SubTotal { get; set; }
        public int Corr_ServicioExterno { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioServicio { get; set; }

    }
}
