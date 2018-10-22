using Capa_Datos.Administracion.Inventario;
using System.Data;
using Capa_Objetos.Administracion.Inventario;

namespace Capa_Negocio.Administracion.Inventario
{
    public class CN_Compras
    {
        Compras obj_Datos_Compras = new Compras();

        public DataTable SelectDetalleCompras(int id_compra = 0)
        {
            return obj_Datos_Compras.SelectDetalleCompras(id_compra);
        }

        public int InsertEncabezadoCompra(CO_Compras objCompras)
        {
            return obj_Datos_Compras.InsertEncabezadoCompra(objCompras);
        }

        public bool InsertDetalleCompra(CO_Compras objCompras)
        {
            return obj_Datos_Compras.InsertDetalleCompra(objCompras);
        }

        public DataTable SelectCompra()
        {
            return obj_Datos_Compras.SelectCompra();
        }

        public DataTable SelectCompra(int id_compra)
        {
            return obj_Datos_Compras.SelectCompra(id_compra);
        }

        public bool UpdateEncabezadoCompra(CO_Compras objCompras)
        {
            return obj_Datos_Compras.UpdateEncabezadoCompra(objCompras);
        }
    }
}
