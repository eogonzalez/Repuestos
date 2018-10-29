using Capa_Datos.Catalogos.Vehiculos;
using Capa_Objetos.Catalogos.Vehiculos;
using Capa_Objetos.General;

namespace Capa_Negocio.Catalogos.Vehiculos
{
    public class CN_Modelos
    {
        Modelos obj_Datos_Modelos = new Modelos();

        public CO_Respuesta SelectModelos()
        {
            return obj_Datos_Modelos.SelectModelos();
        }

        public CO_Respuesta SelectModelos(int id_linea)
        {
            return obj_Datos_Modelos.SelectModelos(id_linea);
        }

        public CO_Respuesta InsertModelo(CO_Modelos objModelos)
        {
            return obj_Datos_Modelos.InsertModelo(objModelos);
        }

        public CO_Respuesta UpdateModelo(CO_Modelos objModelos)
        {
            return obj_Datos_Modelos.UpdateModelo(objModelos);
        }

        public CO_Respuesta DeleteModelo(int id_modelo)
        {
            return obj_Datos_Modelos.DeleteModelo(id_modelo);
        }

    }
}
