using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace web.Controllers
{
    public class MovimientosController : Controller
    {
        // GET: Movimientos
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Roles.Reportes, (int)Habilitado.SI)]
        public ActionResult Movimientos()
        {
            IEnumerable<DetMovimiento> lista = null;
            try
            {
                IServiceMovimiento _SeviceMovimiento = new ServiceMovimiento();
                lista = _SeviceMovimiento.GetMovimientos();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 

                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }
    }    
}