using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProductos();
        Producto GetProductoByID(int id);
        IEnumerable<Botanica> GetBotanica();
        Producto guardarProducto(Producto producto);
        IEnumerable<Sucursal> GetSucursales();
        Producto_proveedor GetProveedorByID(int idProducto, int idProveedor);
        Ubicacion GetUbicacionByID(int idProducto, int idSucursal);
        Producto_proveedor asociarProveedor(Producto_proveedor proveedor);
        Ubicacion asociarUbicacion(Ubicacion ubicacion);
    }
}
