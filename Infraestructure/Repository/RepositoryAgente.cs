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
    public class RepositoryAgente: IRepositoryAgente
    {
        public IEnumerable<Agente> GetAgentes()
        {
            try
            {

                IEnumerable<Agente> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Agente.Include(x => x.Proveedor).ToList();
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
        public Agente GetAgentesByID(int id)
        {
            Agente oAgente = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oAgente = ctx.Agente.
                    Where(l => l.idAgente == id).
                    FirstOrDefault();
            }
            return oAgente;
        }

        public Agente guardarAgente(Agente agente)
        {
            int retorno = 0;
            Agente oAgente = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oAgente = GetAgentesByID((int)agente.idAgente);

                if (oAgente == null)
                {
                    ctx.Agente.Add(agente);
                }
                else
                {
                    ctx.Entry(agente).State = EntityState.Modified;
                }
                retorno = ctx.SaveChanges();
            }

            if (retorno >= 0)
                oAgente = GetAgentesByID((int)agente.idAgente);

            return oAgente;
        }
    }
}
