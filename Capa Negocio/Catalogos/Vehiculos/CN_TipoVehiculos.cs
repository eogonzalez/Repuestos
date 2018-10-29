using Capa_Datos.Catalogos.Vehiculos;
using Capa_Objetos.Catalogos.Vehiculos;
using Capa_Objetos.General;


namespace Capa_Negocio.Catalogos.Vehiculos
{
    public class CN_TipoVehiculos
    {
        TipoVehiculos obj_Datos_TipoVehiculos = new TipoVehiculos();

        public CO_Respuesta SelectTipoVehiculos()
        {
            return obj_Datos_TipoVehiculos.SelectTipoVehiculos();
        }

        public CO_Respuesta SelectTipoVehiculos(int id_tipo_vehiculo)
        {
            return obj_Datos_TipoVehiculos.SelectTipoVehiculos(id_tipo_vehiculo);
        }

        public CO_Respuesta InsertIpoVehiculo(CO_TipoVehiculos objTipoVehiculo)
        {
            return obj_Datos_TipoVehiculos.InsertTipoVehiculo(objTipoVehiculo);
        }

        public CO_Respuesta UpdateTipoVehiculo(CO_TipoVehiculos objTipoVehiculo)
        {
            return obj_Datos_TipoVehiculos.UpdateTipoVehiculo(objTipoVehiculo);
        }

        public CO_Respuesta DeleteTipoVehiculo(int id_tipo_vehiculo)
        {
            return obj_Datos_TipoVehiculos.DeleteTipoVehiculo(id_tipo_vehiculo);
        }

    }
}
