using Capa_Datos.Administracion;
using Capa_Objetos.Administracion;
using System;
using System.Data;

namespace Capa_Negocio.Administracion
{
    public class CNMenu
    {
        Menu objCDMenu = new Menu();

        public DataTable SelectMenu(int id_padre = 0)
        {
            return objCDMenu.SelectMenu(id_padre);
        }

        public Boolean SaveMenu(CEMenu objCEMenu)
        {
            return objCDMenu.SaveMenu(objCEMenu);
        }

        public DataSet MenuPrincipal(int idUsuario = 0)
        {
            return objCDMenu.MenuPrincipal(idUsuario);
        }

        public DataTable SelectOpcionMenu(int id_opcion)
        {
            return objCDMenu.SelectOpcionMenu(id_opcion);
        }

        public Boolean UpdateMenuOpcion(CEMenu objCEMenu)
        {
            return objCDMenu.UpdateMenuOpcion(objCEMenu);
        }

        public Boolean DeleteMenuOpcion(CEMenu objCEMenu)
        {
            return objCDMenu.DeleteMenuOpcion(objCEMenu);
        }
    }
}
