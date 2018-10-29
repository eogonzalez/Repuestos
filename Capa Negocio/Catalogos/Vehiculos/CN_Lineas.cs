using Capa_Datos.Catalogos.Vehiculos;
using Capa_Objetos.Catalogos.Vehiculos;
using Capa_Objetos.General;

namespace Capa_Negocio.Catalogos.Vehiculos
{
    public class CN_Lineas
    {
        Lineas obj_Datos_Lineas = new Lineas();

        public CO_Respuesta SelectLineas()
        {
            return obj_Datos_Lineas.SelectLineas();            
        }

        public CO_Respuesta SelectLineas(int id_linea, bool combo = false)
        {
            return obj_Datos_Lineas.SelectLineas(id_linea, combo);
        }

        public CO_Respuesta SelectLineas(int id_marca)
        {
            return obj_Datos_Lineas.SelectLineas(id_marca);
        }

        public CO_Respuesta InsertLinea(CO_Lineas objLineas)
        {
            return obj_Datos_Lineas.InsertLinea(objLineas);
        }

        public CO_Respuesta UpdateLinea(CO_Lineas objLineas)
        {
            return obj_Datos_Lineas.UpdateLinea(objLineas);
        }

        public CO_Respuesta DeleteLinea(int id_linea)
        {
            return obj_Datos_Lineas.DeleteLinea(id_linea);
        }

    }
}
