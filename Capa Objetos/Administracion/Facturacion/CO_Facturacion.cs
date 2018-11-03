using System;

namespace Capa_Objetos.Administracion.Facturacion
{
    public class CO_Facturacion
    {
        public int Id_Factura { get; set; }
        public int Numero_Factura { get; set; }
        public string Serie { get; set; }
        public int Id_Cliente { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public int Correlativo { get; set; }
        public string Tipo { get; set; }
        public int Id_Producto_Servicio { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public decimal CostoServicio { get; set; }
    }
}
