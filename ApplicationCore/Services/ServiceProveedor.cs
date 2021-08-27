using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProveedor: IServiceProveedor
    {
        public IEnumerable<Proveedor> GetProveedores()
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetProveedores();
        }
        public Proveedor GetProveedorByID(int id)
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetProveedorByID(id);
        }
        public Proveedor guardarProveedor(Proveedor proveedor)
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.guardarProveedor(proveedor);
        }
    }
}
