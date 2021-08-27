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
    public class RepositoryMovimiento : IRepositoryMovimiento
    {
        public Movimiento GetMovimientoId(int id)
        {
            Movimiento oMovimiento = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oMovimiento = ctx.Movimiento.
                        Where(l => l.idMovimiento == id).
                        FirstOrDefault();
            }
            return oMovimiento;
        }

        public IEnumerable<DetMovimiento> GetMovimientos()
        {
            try
            {

                IEnumerable<DetMovimiento> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.DetMovimiento.Include(x => x.Movimiento).ToList();
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
        public IEnumerable<TipoMovimiento> GetTipoMovimiento(int tipo)
        {
            try
            {

                IEnumerable<TipoMovimiento> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.TipoMovimiento.Where(x => x.idTipoMovimiento == tipo).ToList();
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
        public Movimiento guardarMovimiento(Movimiento movimiento)
        {
            int retorno = 0;
            Movimiento oMovimiento = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oMovimiento = GetMovimientoId((int)movimiento.idMovimiento);

                if (oMovimiento == null)
                {
                    ctx.Movimiento.Add(movimiento);
                }
                else
                {
                    ctx.Entry(movimiento).State = EntityState.Modified;
                }
                retorno = ctx.SaveChanges();
            }

            if (retorno >= 0)
                oMovimiento = GetMovimientoId((int)movimiento.idMovimiento);

            return oMovimiento;
        }
        public DetMovimiento guardarDetMovimiento(DetMovimiento detMovimiento)
        {
            int retorno = 0;
            DetMovimiento oDetMovimiento = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                if (oDetMovimiento == null)
                {
                    ctx.DetMovimiento.Add(detMovimiento);
                }
                else
                {
                    ctx.Entry(detMovimiento).State = EntityState.Modified;
                }
                retorno = ctx.SaveChanges();
            }
            return oDetMovimiento;
        }
        public Movimiento UltimoMovimiento()
        {
            try
            {

                Movimiento lista = null;
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Movimiento.OrderByDescending(p => new { p.idMovimiento }).FirstOrDefault();
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
