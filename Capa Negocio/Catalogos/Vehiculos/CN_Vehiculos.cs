using Capa_Objetos.Catalogos.Vehiculos;
using Capa_Objetos.General;

namespace Capa_Negocio.Catalogos.Vehiculos
{
    public class CN_Vehiculos
    {
        Capa_Datos.Catalogos.Vehiculos.Vehiculos obj_Datos_Vehiculos = new Capa_Datos.Catalogos.Vehiculos.Vehiculos();
        
        public CO_Respuesta SelectVehiculos(bool combo = false)
        {
            return obj_Datos_Vehiculos.SelectVehiculos(combo);
        }

        public CO_Respuesta SelectVehiculos(int id_vehiculo)
        {
            return obj_Datos_Vehiculos.SelectVehiculos(id_vehiculo);
        }

        public CO_Respuesta InsertVehiculo(CO_Vehiculos objVehiculos)
        {
            return obj_Datos_Vehiculos.InsertVehiculo(objVehiculos);
        }

        public CO_Respuesta UpdateVehiculo(CO_Vehiculos objVehiculos)
        {
            return obj_Datos_Vehiculos.UpdateVehiculo(objVehiculos);
        }
        
        public CO_Respuesta DeleteVehiculo(int id_vehiculo)
        {
            return obj_Datos_Vehiculos.DeleteVehiculo(id_vehiculo);
        }
    }
}
