using Capa_Objetos.General;

namespace Capa_Negocio.Administracion.Facturacion
{
    public class CN_Facturacion
    {
        Capa_Datos.Administracion.Facturacion.Facturacion obj_Negocio_Facturacion = new Capa_Datos.Administracion.Facturacion.Facturacion();
        public CO_Respuesta SelectFactura()
        {
            return obj_Negocio_Facturacion.SelectFactura();
        }

        public CO_Respuesta SelectDetalleFactura(int id_factura)
        {
            return obj_Negocio_Facturacion.SelectDetalleFactura(id_factura);
        }

        public CO_Respuesta SelectFactura(int id_factura)
        {
            return obj_Negocio_Facturacion.SelectFactura(id_factura);
        }
    }
}
