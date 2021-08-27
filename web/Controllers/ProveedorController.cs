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
    public class ProveedorController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Roles.Reportes, (int)Habilitado.SI)]
        public ActionResult Proveedores()
        {
            IEnumerable<Proveedor> lista = null;
            try
            {
                IServiceProveedor _ServiceProveedor = new ServiceProveedor();
                lista = _ServiceProveedor.GetProveedores();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 

                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Roles.Reportes, (int)Habilitado.SI)]
        public ActionResult DetalleProveedor(int? id)
        {
            ServiceProveedor _ServiceProveedor = new ServiceProveedor();
            Proveedor proveedor = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Proveedores");
                }
                ViewBag.idPr = listaProductos();
                ViewBag.idAgen = listaAgentes();
                proveedor = _ServiceProveedor.GetProveedorByID(id.Value);
                if (proveedor == null)
                {
                    TempData["Message"] = "No existe el proveedor solicitado";
                    TempData["Redirect"] = "Proveedores";
                    TempData["Redirect-Action"] = "Proveedores";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(proveedor);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Proveedores";
                TempData["Redirect-Action"] = "Proveedores";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaAgentes(int idAgente = 0)
        {
            IServiceAgente _ServiceAgente = new ServiceAgente();
            IEnumerable<Agente> listaAgentes = _ServiceAgente.GetAgentes();
            return new SelectList(listaAgentes, "idAgente", "nombre", idAgente);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaProductos(int idProducto = 0)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IEnumerable<Producto> listaProductos = _ServiceProducto.GetProductos();
            return new SelectList(listaProductos, "idProducto", "nombre", idProducto);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaPaises(int idPais = 0)
        {
            List<Proveedor> p = new List<Proveedor>();

            p = new List<Proveedor>()
            {
                new Proveedor(){pais="Costa Rica", nombre = "Costa Rica"},
                new Proveedor(){pais="Estados Unidos", nombre = "Estados Unidos"},
                new Proveedor(){pais="Mexico", nombre = "Mexico"},
                new Proveedor(){pais="Panama", nombre = "Panama"}
            };
            return new SelectList(p, "pais", "nombre", idPais);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult mantenimientoProveedores()
        {
            ViewBag.EXISTE = TempData["EXISTE"] as string;
            ViewBag.paises = listaPaises();
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        public ActionResult listadoProveedores()
        {
            ViewBag.CONFIRMADO = TempData["CONFIRMADO"] as string;
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            IEnumerable<Proveedor> proveedor = _ServiceProveedor.GetProveedores();
            return View(proveedor);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult listadoProveedorInactivo()
        {
            ViewBag.CONFIRMADO = TempData["CONFIRMADO"] as string;
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            IEnumerable<Proveedor> proveedor = _ServiceProveedor.GetProveedores();
            return View(proveedor);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult _MantenimientoProveedoresEditar(int? id)
        {

            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            Proveedor p = new Proveedor();

            p = _ServiceProveedor.GetProveedorByID(id.Value);
            ViewBag.paises = listaPaises();
            return View(p);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult GuardarProveedor(Proveedor proveedor, int editar)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            Proveedor p = new Proveedor();
            Proveedor validar = _ServiceProveedor.GetProveedorByID(proveedor.idProveedor);

            p.idProveedor = proveedor.idProveedor;
            p.nombre = proveedor.nombre;
            p.direccion = proveedor.direccion;
            p.pais = proveedor.pais;
            p.activo = 1;

            try
            {
                if (editar == 0)
                {
                    if (validar == null)
                    {
                        if (ModelState.IsValid)
                        {
                            TempData["CONFIRMADO"] = "true";
                            Proveedor oProveedorI = _ServiceProveedor.guardarProveedor(p);
                        }
                        else
                        {
                            // Valida Errores si Javascript está deshabilitado
                            Util.ValidateErrors(this);
                            return View("mantenimientoProveedores", proveedor);
                        }
                    }
                    else
                    {
                        TempData["EXISTE"] = "true";
                        return RedirectToAction("mantenimientoProveedores");
                    }
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        TempData["CONFIRMADO"] = "true";
                        Proveedor oProveedorI = _ServiceProveedor.guardarProveedor(p);
                    }
                    else
                    {
                        // Valida Errores si Javascript está deshabilitado
                        Util.ValidateErrors(this);
                        return View("mantenimientoProveedores", proveedor);
                    }
                }

                return RedirectToAction("listadoProveedores");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "mantenimientoProveedores";
                TempData["Redirect-Action"] = "mantenimientoProveedores";
                // Redireccion a la captura del Error
                return RedirectToAction("mantenimientoProveedores", "Proveedor");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult Borrar(int? idBorrar)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            Proveedor p = new Proveedor();
            p = _ServiceProveedor.GetProveedorByID(idBorrar.Value);
            p.activo = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Proveedor oProveedorI = _ServiceProveedor.guardarProveedor(p);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("listadoProveedores", p);
                }

                return RedirectToAction("listadoProveedores", "Proveedor");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "listadoProveedores";
                TempData["Redirect-Action"] = "listadoProveedores";
                // Redireccion a la captura del Error
                return RedirectToAction("listadoProveedores", "Proveedor");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult Activar(int? id)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            Proveedor p = new Proveedor();
            p = _ServiceProveedor.GetProveedorByID(id.Value);
            p.activo = 1;

            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Proveedor oProveedorI = _ServiceProveedor.guardarProveedor(p);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("listadoProveedorInactivo", p);
                }

                return RedirectToAction("listadoProveedorInactivo", "Proveedor");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "listadoProveedorInactivo";
                TempData["Redirect-Action"] = "listadoProveedorInactivo";
                // Redireccion a la captura del Error
                return RedirectToAction("listadoProveedorInactivo", "Proveedor");
            }
        }
    }
}