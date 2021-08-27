using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryProveedor: IRepositoryProveedor
    {
        public IEnumerable<Proveedor> GetProveedores()
        {
            try
            {

                IEnumerable<Proveedor> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Proveedor.Include(x => x.Agente).Include(x => x.Producto_proveedor).ToList();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
        public Proveedor GetProveedorByID(int id)
        {
            Proveedor oProveedor = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProveedor = ctx.Proveedor.
                    Where(l => l.idProveedor == id).
                    Include(a => a.Agente).
                    Include(t => t.Producto_proveedor).
                    FirstOrDefault();
            }
            return oProveedor;
        }
        public Proveedor guardarProveedor(Proveedor proveedor)
        {
            int retorno = 0;
            Proveedor oProveedor = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProveedor = GetProveedorByID((int)proveedor.idProveedor);

                if (oProveedor == null)
                {
                    ctx.Proveedor.Add(proveedor);
                }
                else
                {
                    ctx.Entry(proveedor).State = EntityState.Modified;
                }
                retorno = ctx.SaveChanges();
            }

            if (retorno >= 0)
                oProveedor = GetProveedorByID((int)proveedor.idProveedor);

            return oProveedor;
        }        
    }
}
