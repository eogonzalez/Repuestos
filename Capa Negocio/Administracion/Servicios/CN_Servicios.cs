using Capa_Objetos.General;

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
    }
}
