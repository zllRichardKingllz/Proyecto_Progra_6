using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto: IServiceProducto
    {
        public IEnumerable<Producto> GetProductos()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductos();
        }
        public Producto GetProductoByID(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByID(id);
        }
        public IEnumerable<Botanica> GetBotanica()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetBotanica();
        }
        public Producto guardarProducto(Producto producto)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.guardarProducto(producto);
        }
        public IEnumerable<Sucursal> GetSucursales()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetSucursales();
        }
        public Producto_proveedor GetProveedorByID(int idProducto, int idProveedor)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProveedorByID(idProducto, idProveedor);
        }
        public Ubicacion GetUbicacionByID(int idProducto, int idSucursal)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetUbicacionByID(idProducto, idSucursal);
        }
        public Producto_proveedor asociarProveedor(Producto_proveedor proveedor)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.asociarProveedor(proveedor);
        }
        public Ubicacion asociarUbicacion(Ubicacion ubicacion)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.asociarUbicacion(ubicacion);
        }
    }
}
