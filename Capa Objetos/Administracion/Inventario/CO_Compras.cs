using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Objetos.Administracion.Inventario
{
    public class CO_Compras
    {
        public int Id_Correlativo { get; set; }
        public int Id_Compra { get; set; }
        public int NumeroCompra { get; set; }
        public string Serie { get; set; }
        public int Id_Proveedor { get; set; }
        public DateTime Fecha_Compra { get; set; }
        public decimal Total { get; set; }
        public int Id_Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }

    }
}
