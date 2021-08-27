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
    public class RepositoryProducto: IRepositoryProducto
    {
        public IEnumerable<Producto> GetProductos()
        {
            try
            {

                IEnumerable<Producto> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Producto.Include(x => x.Ubicacion).Include(x => x.Producto_proveedor).Include(x => x.Botanica).ToList();
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
        public Producto GetProductoByID(int id)
        {
            Producto oProducto = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProducto = ctx.Producto.
                    Where(l => l.idProducto == id).
                    Include(a => a.Ubicacion).
                    Include(t => t.Producto_proveedor).
                    Include(x => x.Botanica).
                    FirstOrDefault();
            }
            return oProducto;
        }
        public Ubicacion GetUbicacionByID(int idProducto, int idSucursal)
        {
            Ubicacion oUbicacion = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oUbicacion = ctx.Ubicacion.
                    Where(l => l.idProducto == idProducto && l.idSucursal == idSucursal).FirstOrDefault();
            }
            return oUbicacion;
        }
        public Producto_proveedor GetProveedorByID(int idProducto, int idProveedor)
        {
            Producto_proveedor oProveedor = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProveedor = ctx.Producto_proveedor.
                    Where(l => l.idProducto == idProducto && l.idProveedor == idProveedor).FirstOrDefault();
            }
            return oProveedor;
        }
        public IEnumerable<Botanica> GetBotanica()
        {
            try
            {

                IEnumerable<Botanica> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Botanica.ToList();
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
        public Producto guardarProducto(Producto producto)
        {
            int retorno = 0;
            Producto oProducto = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProducto = GetProductoByID((int)producto.idProducto);

                if (oProducto == null)
                {
                    ctx.Producto.Add(producto);                    
                }
                else
                {
                    ctx.Entry(producto).State = EntityState.Modified;
                }
                retorno = ctx.SaveChanges();
            }

            if (retorno >= 0)
                oProducto = GetProductoByID((int)producto.idProducto);

            return oProducto;
        }
        public Ubicacion asociarUbicacion(Ubicacion ubicacion)
        {
            int retorno = 0;
            Ubicacion oUbicacion = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oUbicacion = GetUbicacionByID((int)ubicacion.idProducto, (int)ubicacion.idSucursal);

                if (oUbicacion == null)
                {
                    ctx.Ubicacion.Add(ubicacion);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Ubicacion.Add(ubicacion);
                    ctx.Entry(ubicacion).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
                oUbicacion = GetUbicacionByID((int)ubicacion.idProducto, (int)ubicacion.idSucursal);

            return oUbicacion;
        }
        public Producto_proveedor asociarProveedor(Producto_proveedor proveedor)
        {
            int retorno = 0;
            Producto_proveedor oProveedor = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProveedor = GetProveedorByID((int)proveedor.idProducto, (int)proveedor.idProveedor);

                if (oProveedor == null)
                {
                    ctx.Producto_proveedor.Add(proveedor);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Producto_proveedor.Add(proveedor);
                    ctx.Entry(proveedor).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
                oProveedor = GetProveedorByID((int)proveedor.idProducto, (int)proveedor.idProveedor);

            return oProveedor;
        }
        public IEnumerable<Sucursal> GetSucursales()
        {
            try
            {

                IEnumerable<Sucursal> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Sucursal.ToList();
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
    }
}
