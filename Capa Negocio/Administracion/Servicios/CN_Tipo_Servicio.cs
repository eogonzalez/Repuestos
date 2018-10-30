using Capa_Objetos.General;
using Capa_Datos.Administracion.Servicios;
using Capa_Objetos.Administracion.Servicios;

namespace Capa_Negocio.Administracion.Servicios
{
    public class CN_Tipo_Servicio
    {
        Tipo_Servicio obj_Negocio_TipoServicio = new Tipo_Servicio();

        public CO_Respuesta SelectTipoServicio()
        {
            return obj_Negocio_TipoServicio.SelectTipoServicio();
        }

        public CO_Respuesta SelectTipoServicio(int id_tipo_servicio)
        {
            return obj_Negocio_TipoServicio.SelectTipoServicio(id_tipo_servicio);
        }

        public CO_Respuesta InsertTipoServicio(CO_Tipo_Servicio objTipoServicio)
        {
            return obj_Negocio_TipoServicio.InsertTipoServicio(objTipoServicio);
        }

        public CO_Respuesta UpdateTipoServicio(CO_Tipo_Servicio objTipoServicio)
        {
            return obj_Negocio_TipoServicio.UpdateTipoServicio(objTipoServicio);
        }

        public CO_Respuesta DeleteTipoServicio(int id_tipo_servicio)
        {
            return obj_Negocio_TipoServicio.DeleteTipoServicio(id_tipo_servicio);
        }
    }
}
