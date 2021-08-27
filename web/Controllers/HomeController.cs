using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Roles.Reportes, (int)Habilitado.SI)]
        public ActionResult Index()
        {
            ViewBag.logExito = TempData["logExito"] as string;
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Roles.Reportes, (int)Habilitado.SI)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Roles.Reportes, (int)Habilitado.SI)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Roles.Reportes, (int)Habilitado.SI)]
        public ActionResult Inventario()
        {
            return View();
        }
    }
}