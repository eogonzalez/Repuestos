using Capa_Datos.Catalogos.Vehiculos;
using Capa_Objetos.Catalogos.Vehiculos;
using Capa_Objetos.General;
using System.Data;

namespace Capa_Negocio.Catalogos.Vehiculos
{
    public class CN_Marcas
    {
        Marcas obj_Datos_Marcas = new Marcas();

        public DataTable SelectMarcas()
        {
            return obj_Datos_Marcas.SelectMarcas();
        }

        public DataTable SelectMarcas(int id_marca)
        {
            return obj_Datos_Marcas.SelectMarcas(id_marca);
        }

        public CO_Respuesta InsertMarca(CO_Marcas objMarcas)
        {
            return obj_Datos_Marcas.InsertMarca(objMarcas);
        }

        public bool UpdateMarca(CO_Marcas objMarcas)
        {
            return obj_Datos_Marcas.UpdateMarca(objMarcas);
        }

        public bool DeleteMarca(int id_marca)
        {
            return obj_Datos_Marcas.DeleteMarca(id_marca);
        }
    }
}
