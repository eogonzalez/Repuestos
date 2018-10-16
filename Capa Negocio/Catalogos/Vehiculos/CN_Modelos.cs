using Capa_Datos.Catalogos.Vehiculos;
using Capa_Objetos.Catalogos.Vehiculos;
using System.Data;

namespace Capa_Negocio.Catalogos.Vehiculos
{
    public class CN_Modelos
    {
        Modelos obj_Datos_Modelos = new Modelos();

        public DataTable SelectModelos()
        {
            return obj_Datos_Modelos.SelectModelos();
        }

        public DataTable SelectModelos(int id_modelo)
        {
            return obj_Datos_Modelos.SelectModelos(id_modelo);
        }

        public bool InsertModelo(CO_Modelos objModelos)
        {
            return obj_Datos_Modelos.InsertModelo(objModelos);
        }

        public bool UpdateModelo(CO_Modelos objModelos)
        {
            return obj_Datos_Modelos.UpdateModelo(objModelos);
        }

        public bool DeleteModelo(int id_modelo)
        {
            return obj_Datos_Modelos.DeleteModelo(id_modelo);
        }

    }
}
