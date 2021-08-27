using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using web.Utils;
using Web.Security;

namespace web.Controllers
{
    public class AgenteController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult listadoAgentes(int idAgente = 0)
        {
            ViewBag.CONFIRMADO = TempData["CONFIRMADO"] as string;
            IServiceAgente _ServiceAgente = new ServiceAgente();
            IEnumerable<Agente> agente = _ServiceAgente.GetAgentes();
            return View(agente);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult listadoAgentesInactivo()
        {
            ViewBag.CONFIRMADO = TempData["CONFIRMADO"] as string;
            IServiceAgente _ServiceAgente = new ServiceAgente();
            List<Agente> agente = (List<Agente>)_ServiceAgente.GetAgentes();
            return View(agente);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult mantenimientoAgente()
        {
            ViewBag.EXISTE = TempData["EXISTE"] as string;
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult _MantenimientoAgenteEditar(int? id)
        {

            IServiceAgente _ServiceAgente = new ServiceAgente();
            Agente a = new Agente();

            a = _ServiceAgente.GetAgentesByID(id.Value);            
            return View(a);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult asociar()
        {
            ViewBag.CONFIRMADO = TempData["CONFIRMADO"] as string;
            ViewBag.IDAGENTE = listaAgentes();
            ViewBag.IDPROVEEDOR = listaProveedores();
            return View();
        }
        //listas
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaProveedores(int idProveedor = 0)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            IEnumerable<Proveedor> listaProveedores = _ServiceProveedor.GetProveedores();
            return new SelectList(listaProveedores, "idProveedor", "nombre", idProveedor);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaAgentes(int idAgente = 0)
        {
            IServiceAgente _ServiceAgente = new ServiceAgente();
            IEnumerable<Agente> listaAgentes = _ServiceAgente.GetAgentes();
            return new SelectList(listaAgentes, "idAgente", "nombre", idAgente);
        }
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult GuardarAgente(Agente agente, int editar)
        {
            IServiceAgente _ServiceAgente = new ServiceAgente();
            Agente a = new Agente();
            Agente validar = _ServiceAgente.GetAgentesByID(agente.idAgente);

            a.idAgente = agente.idAgente;
            a.idProveedor = agente.idProveedor;
            a.nombre = agente.nombre;
            a.telefono = agente.telefono;
            a.email = agente.email;
            a.activo = agente.activo;

            try
            {
                if (editar == 0)
                {
                    if (validar == null)
                    {
                        if (ModelState.IsValid)
                        {
                            TempData["CONFIRMADO"] = "true";
                            Agente oAgenteI = _ServiceAgente.guardarAgente(a);
                        }
                        else
                        {
                            // Valida Errores si Javascript está deshabilitado
                            Util.ValidateErrors(this);
                            return View("mantenimientoAgente", agente);
                        }
                    }
                    else
                    {
                        TempData["EXISTE"] = "true";
                        return RedirectToAction("mantenimientoAgente");
                    }
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        TempData["CONFIRMADO"] = "true";
                        Agente oAgenteI = _ServiceAgente.guardarAgente(a);
                    }
                    else
                    {
                        // Valida Errores si Javascript está deshabilitado
                        Util.ValidateErrors(this);
                        return View("mantenimientoAgente", agente);
                    }
                }

                return RedirectToAction("listadoAgentes");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "mantenimientoAgente";
                TempData["Redirect-Action"] = "mantenimientoAgente";
                // Redireccion a la captura del Error
                return RedirectToAction("mantenimientoAgente", "Agente");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult Borrar(int? idBorrar)
        {
            IServiceAgente ServiceAgente = new ServiceAgente();
            Agente a = new Agente();
            a = ServiceAgente.GetAgentesByID(idBorrar.Value);
            a.activo = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Agente oAgenteI = ServiceAgente.guardarAgente(a);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("listadoAgentes", a);
                }

                return RedirectToAction("listadoAgentes", "Agente");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "listadoAgentes";
                TempData["Redirect-Action"] = "listadoAgentes";
                // Redireccion a la captura del Error
                return RedirectToAction("listadoAgentes", "Agente");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult Activar(int? id)
        {
            IServiceAgente ServiceAgente = new ServiceAgente();
            Agente a = new Agente();
            a = ServiceAgente.GetAgentesByID(id.Value);
            a.activo = 1;

            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Agente oAgenteI = ServiceAgente.guardarAgente(a);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("listadoAgentesInactivo", a);
                }

                return RedirectToAction("listadoAgentesInactivo", "Agente");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "listadoAgentesInactivo";
                TempData["Redirect-Action"] = "listadoAgentesInactivo";
                // Redireccion a la captura del Error
                return RedirectToAction("listadoAgentesInactivo", "Agente");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public JsonResult asociarAgente(Proveedor objProveedor, Agente objAgente)
        {
            IServiceAgente _ServiceAgente = new ServiceAgente();
            Agente pr = new Agente();
            pr = _ServiceAgente.GetAgentesByID((int)objAgente.idAgente);
            pr.idProveedor = (int)objProveedor.idProveedor;
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Agente oAgenteI = _ServiceAgente.guardarAgente(pr);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                }
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "asociar";
                TempData["Redirect-Action"] = "asociar";
                // Redireccion a la captura del Error
                return new JsonResult { Data = new { idAgente = 0, idProveedor = 0 } };
            }
            return new JsonResult { Data = new { idAgente = 0, idProveedor = 0 } };
        }
    }
}