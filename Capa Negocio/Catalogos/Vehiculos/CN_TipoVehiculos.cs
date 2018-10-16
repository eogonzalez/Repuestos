using Capa_Datos.Catalogos.Vehiculos;
using Capa_Objetos.Catalogos.Vehiculos;
using System.Data;

namespace Capa_Negocio.Catalogos.Vehiculos
{
    public class CN_TipoVehiculos
    {
        TipoVehiculos obj_Datos_TipoVehiculos = new TipoVehiculos();

        public DataTable SelectTipoVehiculos()
        {
            return obj_Datos_TipoVehiculos.SelectTipoVehiculos();
        }

        public DataTable SelectTipoVehiculos(int id_tipo_vehiculo)
        {
            return obj_Datos_TipoVehiculos.SelectTipoVehiculos(id_tipo_vehiculo);
        }

        public bool InsertIpoVehiculo(CO_TipoVehiculos objTipoVehiculo)
        {
            return obj_Datos_TipoVehiculos.InsertTipoVehiculo(objTipoVehiculo);
        }

        public bool UpdateTipoVehiculo(CO_TipoVehiculos objTipoVehiculo)
        {
            return obj_Datos_TipoVehiculos.UpdateTipoVehiculo(objTipoVehiculo);
        }

        public bool DeleteTipoVehiculo(int id_tipo_vehiculo)
        {
            return obj_Datos_TipoVehiculos.DeleteTipoVehiculo(id_tipo_vehiculo);
        }

    }
}
