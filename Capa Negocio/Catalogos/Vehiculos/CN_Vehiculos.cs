using Capa_Objetos.Catalogos.Vehiculos;
using System.Data;

namespace Capa_Negocio.Catalogos.Vehiculos
{
    public class CN_Vehiculos
    {
        Capa_Datos.Catalogos.Vehiculos.Vehiculos obj_Datos_Vehiculos = new Capa_Datos.Catalogos.Vehiculos.Vehiculos();
        
        public DataTable SelectVehiculos()
        {
            return obj_Datos_Vehiculos.SelectVehiculos();
        }

        public DataTable SelectVehiculos(int id_vehiculo)
        {
            return obj_Datos_Vehiculos.SelectVehiculos(id_vehiculo);
        }

        public bool InsertVehiculo(CO_Vehiculos objVehiculos)
        {
            return obj_Datos_Vehiculos.InsertVehiculo(objVehiculos);
        }

        public bool UpdateVehiculo(CO_Vehiculos objVehiculos)
        {
            return obj_Datos_Vehiculos.UpdateVehiculo(objVehiculos);
        }
        
        public bool DeleteVehiculo(int id_vehiculo)
        {
            return obj_Datos_Vehiculos.DeleteVehiculo(id_vehiculo);
        }
    }
}
