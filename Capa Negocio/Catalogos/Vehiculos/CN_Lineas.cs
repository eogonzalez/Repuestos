using Capa_Datos.Catalogos.Vehiculos;
using System.Data;
using Capa_Objetos.Catalogos.Vehiculos;

namespace Capa_Negocio.Catalogos.Vehiculos
{
    public class CN_Lineas
    {
        Lineas obj_Datos_Lineas = new Lineas();

        public DataTable SelectLineas()
        {
            return obj_Datos_Lineas.SelectLineas();            
        }

        public DataTable SelectLineas(int id_linea, bool combo = false)
        {
            return obj_Datos_Lineas.SelectLineas(id_linea, combo);
        }

        public DataTable SelectLineas(int id_marca)
        {
            return obj_Datos_Lineas.SelectLineas(id_marca);
        }

        public bool InsertLinea(CO_Lineas objLineas)
        {
            return obj_Datos_Lineas.InsertLinea(objLineas);
        }

        public bool UpdateLinea(CO_Lineas objLineas)
        {
            return obj_Datos_Lineas.UpdateLinea(objLineas);
        }

        public bool DeleteLinea(int id_linea)
        {
            return obj_Datos_Lineas.DeleteLinea(id_linea);
        }

    }
}
