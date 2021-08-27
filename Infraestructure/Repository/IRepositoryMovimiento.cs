using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryMovimiento
    {
        IEnumerable<DetMovimiento> GetMovimientos();
        Movimiento GetMovimientoId(int id);
        IEnumerable<TipoMovimiento> GetTipoMovimiento(int tipo);
        Movimiento guardarMovimiento(Movimiento movimiento);
        Movimiento UltimoMovimiento();
        DetMovimiento guardarDetMovimiento(DetMovimiento detMovimiento);
    }
}
