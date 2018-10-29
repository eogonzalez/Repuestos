using Capa_Datos.Administracion.Inventario;
using Capa_Objetos.Administracion.Inventario;
using Capa_Objetos.General;

namespace Capa_Negocio.Administracion.Inventario
{
    public class CN_Compras
    {
        Compras obj_Datos_Compras = new Compras();

        public CO_Respuesta SelectDetalleCompras(int id_compra = 0)
        {
            return obj_Datos_Compras.SelectDetalleCompras(id_compra);
        }

        public CO_Respuesta InsertEncabezadoCompra(CO_Compras objCompras)
        {
            return obj_Datos_Compras.InsertEncabezadoCompra(objCompras);
        }

        public CO_Respuesta InsertDetalleCompra(CO_Compras objCompras)
        {
            return obj_Datos_Compras.InsertDetalleCompra(objCompras);
        }

        public CO_Respuesta SelectCompra()
        {
            return obj_Datos_Compras.SelectCompra();
        }

        public CO_Respuesta SelectCompra(int id_compra)
        {
            return obj_Datos_Compras.SelectCompra(id_compra);
        }

        public CO_Respuesta UpdateEncabezadoCompra(CO_Compras objCompras)
        {
            return obj_Datos_Compras.UpdateEncabezadoCompra(objCompras);
        }

        public CO_Respuesta CerrarCompra(int id_compra)
        {
            return obj_Datos_Compras.CerrarCompra(id_compra);
        }

        public CO_Respuesta DeleteCompra(int id_compra)
        {
            return obj_Datos_Compras.DeleteCompra(id_compra);
        }

        public CO_Respuesta UpdateDetalleCompra(CO_Compras objCompras)
        {
            return obj_Datos_Compras.UpdateDetalleCompra(objCompras);
        }

        public CO_Respuesta SelectDetalleCompraProducto(int id_correlativo)
        {
            return obj_Datos_Compras.SelectDetalleCompraProducto(id_correlativo);
        }

        public CO_Respuesta DeleteDetalleCompra(int id_correlativo, int id_compra)
        {
            return obj_Datos_Compras.DeleteDetalleCompra(id_correlativo, id_compra);
        }
    }
}
