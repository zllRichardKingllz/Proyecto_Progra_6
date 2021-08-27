using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceAgente: IServiceAgente
    {
        public IEnumerable<Agente> GetAgentes()
        {
            IRepositoryAgente repository = new RepositoryAgente();
            return repository.GetAgentes();
        }
        public Agente GetAgentesByID(int id)
        {
            IRepositoryAgente repository = new RepositoryAgente();
            return repository.GetAgentesByID(id);
        }
        public Agente guardarAgente(Agente agente)
        {
            IRepositoryAgente repository = new RepositoryAgente();
            return repository.guardarAgente(agente);
        }
    }
}
