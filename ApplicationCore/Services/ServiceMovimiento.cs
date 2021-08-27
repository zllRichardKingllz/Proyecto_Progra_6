using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMovimiento : IServiceMovimiento
    {
        public Movimiento GetMovimientoId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DetMovimiento> GetMovimientos()
        {
            IRepositoryMovimiento repository = new RepositoryMovimiento();
            return repository.GetMovimientos();
        }
        public IEnumerable<TipoMovimiento> GetTipoMovimiento(int tipo)
        {
            IRepositoryMovimiento repository = new RepositoryMovimiento();
            return repository.GetTipoMovimiento(tipo);
        }
        public Movimiento guardarMovimiento(Movimiento movimiento)
        {
            IRepositoryMovimiento repository = new RepositoryMovimiento();
            return repository.guardarMovimiento(movimiento);
        }
        public DetMovimiento guardarDetMovimiento(DetMovimiento detMovimiento)
        {
            IRepositoryMovimiento repository = new RepositoryMovimiento();
            return repository.guardarDetMovimiento(detMovimiento);
        }
        public Movimiento UltimoMovimiento()
        {
            IRepositoryMovimiento repository = new RepositoryMovimiento();
            return repository.UltimoMovimiento();
        }
    }
}
