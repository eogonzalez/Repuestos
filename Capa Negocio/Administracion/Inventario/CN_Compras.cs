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

        public bool CerrarCompra(int id_compra)
        {
            return obj_Datos_Compras.CerrarCompra(id_compra);
        }

        public bool DeleteCompra(int id_compra)
        {
            return obj_Datos_Compras.DeleteCompra(id_compra);
        }

        public bool UpdateDetalleCompra(CO_Compras objCompras)
        {
            return obj_Datos_Compras.UpdateDetalleCompra(objCompras);
        }

        public DataTable SelectDetalleCompraProducto(int id_correlativo)
        {
            return obj_Datos_Compras.SelectDetalleCompraProducto(id_correlativo);
        }

        public bool DeleteDetalleCompra(int id_correlativo, int id_compra)
        {
            return obj_Datos_Compras.DeleteDetalleCompra(id_correlativo, id_compra);
        }
    }
}
