using Capa_Datos.Catalogos.Administracion.Usuarios;
using Capa_Objetos.Administracion.Usuarios;
using System;
using System.Data;

namespace Capa_Negocio.Administracion.Usuarios
{
    public class CN_TipoUsuarios
    {
        TipoUsuarios objCDTipoUsuarios = new TipoUsuarios();
        public DataSet SelectTipoUsuarios()
        {
            return objCDTipoUsuarios.SelectTipoUsuarios();
        }

        public Boolean InsertTipoUsuarios(CO_TipoUsuarios objetoEntidad)
        {
            return objCDTipoUsuarios.InsertTipoUsuarios(objetoEntidad);
        }

        public DataTable SelectTipoUsuario(int id_tipousuario)
        {
            return objCDTipoUsuarios.SelectTipoUsuario(id_tipousuario);
        }

        public Boolean UpdateTipoUsuario(CO_TipoUsuarios objTipoUsuario)
        {
            return objCDTipoUsuarios.UpdateTipoUsuario(objTipoUsuario);
        }

        public Boolean DeleteTipoUsuario(CO_TipoUsuarios objTipoUsuario)
        {
            return objCDTipoUsuarios.DeleteTipoUsuario(objTipoUsuario);
        }
    }
}
