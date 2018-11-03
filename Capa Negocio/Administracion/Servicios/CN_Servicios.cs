using Capa_Objetos.General;
using Capa_Objetos.Administracion.Servicios;
using Capa_Objetos.Administracion.Facturacion;

namespace Capa_Negocio.Administracion.Servicios
{
    public class CN_Servicios
    {
        Capa_Datos.Administracion.Servicios.Servicios obj_Negocio_Servicios = new Capa_Datos.Administracion.Servicios.Servicios();
         
        public CO_Respuesta SelectServicios()
        {
            return obj_Negocio_Servicios.SelectServicios();
        }

        public CO_Respuesta DeleteServicio(int id_servicio)
        {
            return obj_Negocio_Servicios.DeleteServicio(id_servicio);
        }

        public CO_Respuesta InsertEncabezadoServicio(CO_Servicios objServicio)
        {
            return obj_Negocio_Servicios.InsertEncabezadoServicio(objServicio);
        }

        public CO_Respuesta SelectServicios(int id_servicio)
        {
            return obj_Negocio_Servicios.SelectServicios(id_servicio);
        }

        public CO_Respuesta UpdateEncabezadoServicios(CO_Servicios objServicio)
        {
            return obj_Negocio_Servicios.UpdateEncabezadoServicios(objServicio);
        }

        public CO_Respuesta InsertDetalleRepuesto(CO_Servicios objServicio)
        {
            return obj_Negocio_Servicios.InsertDetalleRepuesto(objServicio);
        }

        public CO_Respuesta InsertDetalleServicio(CO_Servicios objServicio)
        {
            return obj_Negocio_Servicios.InsertDetalleServicio(objServicio);
        }

        public CO_Respuesta SelectDetalleRepuestos(int id_servicio, bool panel = false)
        {
            return obj_Negocio_Servicios.SelectDetalleRepuestos(id_servicio, panel);
        }

        public CO_Respuesta SelectDetalleServicios(int id_servicio, bool panel = false)
        {
            return obj_Negocio_Servicios.SelectDetalleServicios(id_servicio, panel);
        }

        public CO_Respuesta UpdateDetalleRepuestos(CO_Servicios objServicio)
        {
            return obj_Negocio_Servicios.UpdateDetalleRepuestos(objServicio);
        }

        public CO_Respuesta UpdateDetalleServicioExterno(CO_Servicios objServicio)
        {
            return obj_Negocio_Servicios.UpdateDetalleServicioExterno(objServicio);
        }

        public CO_Respuesta SelectSubTotalRepuesto(int id_servicio)
        {
            return obj_Negocio_Servicios.SelectSubTotalRepuesto(id_servicio);
        }

        public CO_Respuesta SelectSubTotalServicioExterno(int id_servicio)
        {
            return obj_Negocio_Servicios.SelectSubTotalServicioExterno(id_servicio);
        }

        public CO_Respuesta DeleteDetalleRepuesto(int correlativo_repuesto)
        {
            return obj_Negocio_Servicios.DeleteDetalleRepuesto(correlativo_repuesto);
        }

        public CO_Respuesta DeleteDetalleServicioExterno(int correlativo_servicio)
        {
            return obj_Negocio_Servicios.DeleteDetalleServicioExterno(correlativo_servicio);
        }

        public CO_Respuesta CerrarServicio(CO_Facturacion objFacturacion, CO_Servicios objServicio)
        {
            return obj_Negocio_Servicios.CerrarServicio(objFacturacion, objServicio);
        }
    }
}
