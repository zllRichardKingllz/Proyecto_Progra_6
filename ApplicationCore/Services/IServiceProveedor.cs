using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceProveedor
    {
        IEnumerable<Proveedor> GetProveedores();
        Proveedor GetProveedorByID(int id);
        Proveedor guardarProveedor(Proveedor proveedor);
    }
}
