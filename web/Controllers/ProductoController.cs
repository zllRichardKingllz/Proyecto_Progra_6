using ApplicationCore.Services;
using Infraestructure.Models;
using Newtonsoft.Json;
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
    public class ProductoController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Roles.Reportes, (int)Habilitado.SI)]
        public ActionResult Producto()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProductos();                
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 

                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Roles.Reportes, (int)Habilitado.SI)]
        public ActionResult DetalleProducto(int? id)
        {
            ServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Producto");
                }
                ViewBag.idProve = listaProveedores();
                ViewBag.idUbi = listaSucursales();
                producto = _ServiceProducto.GetProductoByID(id.Value);
                if (producto == null)
                {
                    TempData["Message"] = "No existe el producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "Producto";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(producto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "Producto";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaProductos(int idProducto = 0)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IEnumerable<Producto> listaProductos = _ServiceProducto.GetProductos();
            return new SelectList(listaProductos, "idProducto", "nombre", idProducto);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaProveedores(int idProveedor = 0)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            IEnumerable<Proveedor> listaProveedores = _ServiceProveedor.GetProveedores();
            return new SelectList(listaProveedores, "idProveedor", "nombre", idProveedor);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaSucursales(int idSucursal = 0)
        {
            IServiceProducto _ServiceSucursal = new ServiceProducto();
            IEnumerable<Sucursal> listaSucursales = _ServiceSucursal.GetSucursales();
            return new SelectList(listaSucursales, "idSucursal", "descripcion", idSucursal);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaBotanica(int idBotanica = 0)
        {
            IServiceProducto _ServiceBotanica = new ServiceProducto();
            IEnumerable<Botanica> listaBotanica = _ServiceBotanica.GetBotanica();
            return new SelectList(listaBotanica, "idBotanica", "descripcion", idBotanica);
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult mantenimientoProductos()
        {
            ViewBag.IDBOTANICA = listaBotanica();
            ViewBag.IDPROVEDOR = listaProveedores();
            ViewBag.IDSUCURSAL = listaSucursales();
            ViewBag.EXISTE = TempData["EXISTE"] as string;
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        public ActionResult listadoProducto()
        {
            ViewBag.CONFIRMADO = TempData["CONFIRMADO"] as string;
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IEnumerable<Producto> producto = _ServiceProducto.GetProductos();            
            return View(producto);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult _MantenimientoProductosEditar(int? id)
        {            

            IServiceProducto serviceProducto = new ServiceProducto();
            Producto p = new Producto();

            p = serviceProducto.GetProductoByID(id.Value);
            ViewBag.IDBOTANICA = listaBotanica();
            ViewBag.IDPROVEDOR = listaProveedores();
            ViewBag.IDSUCURSAL = listaSucursales();
            return View(p); 
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult listadoProductoInactivo()
        {
            ViewBag.CONFIRMADO = TempData["CONFIRMADO"] as string;
            IServiceProducto _ServiceProducto = new ServiceProducto();
            List<Producto> producto = (List<Producto>)_ServiceProducto.GetProductos();
            return View(producto);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult asociar()
        {
            ViewBag.CONFIRMADO = TempData["CONFIRMADO"] as string;
            ViewBag.PRODUCTOS = listaProductos();
            ViewBag.IDPROVEDOR = listaProveedores();
            ViewBag.IDSUCURSAL = listaSucursales();
            return View();
        }
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult GuardarProducto(Producto producto, int editar)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto p = new Producto();
            Producto validar = _ServiceProducto.GetProductoByID(producto.idProducto);

            p.idProducto = producto.idProducto;
            p.idBotanica = producto.idBotanica;
            p.nombre = producto.nombre;
            p.descripcion = producto.descripcion;
            p.costoUnitario = producto.costoUnitario;
            p.cantidadMinima = producto.cantidadMinima;
            p.cantidadTotal = producto.cantidadTotal;
            p.activo = producto.activo;
            
            try
            {
                if (editar == 0)
                {
                    if (validar == null)
                    {
                        if (ModelState.IsValid)
                        {
                            TempData["CONFIRMADO"] = "true";
                            Producto oProductoI = _ServiceProducto.guardarProducto(p);                            
                        }
                        else
                        {
                            // Valida Errores si Javascript está deshabilitado
                            Util.ValidateErrors(this);
                            return View("mantenimientoProductos", producto);
                        }
                    }
                    else
                    {
                        TempData["EXISTE"] = "true";
                        return RedirectToAction("mantenimientoProductos");
                    }
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        TempData["CONFIRMADO"] = "true";
                        Producto oProductoI = _ServiceProducto.guardarProducto(p);                        
                    }
                    else
                    {
                        // Valida Errores si Javascript está deshabilitado
                        Util.ValidateErrors(this);
                        return View("mantenimientoProductos", producto);
                    }
                }

                return RedirectToAction("listadoProducto");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "mantenimientoProductos";
                TempData["Redirect-Action"] = "mantenimientoProductos";
                // Redireccion a la captura del Error
                return RedirectToAction("mantenimientoProductos", "Producto");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public JsonResult asociarSucursal(Producto objProducto, Sucursal objSucursal)
        {

            Ubicacion u = new Ubicacion();
            u.idProducto = (int)objProducto.idProducto;
            u.idSucursal = (int)objSucursal.idSucursal;

            IServiceProducto _ServiceProducto = new ServiceProducto();
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Ubicacion oUbicacionI = _ServiceProducto.asociarUbicacion(u);
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
                TempData["Redirect"] = "mantenimientoProductos";
                TempData["Redirect-Action"] = "mantenimientoProductos";
                // Redireccion a la captura del Error
                return new JsonResult { Data = new { idProducto = 0, idProveedor = 0, idSucursal = 0 } };
            }
            return new JsonResult { Data = new { idProducto = 0, idProveedor = 0, idSucursal = 0 } };
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public JsonResult asociarProveedor(Producto objProducto, Proveedor objProveedor)
        {
            Producto_proveedor pr = new Producto_proveedor();
            pr.idProducto = (int)objProducto.idProducto;
            pr.idProveedor = (int)objProveedor.idProveedor;

            IServiceProducto _ServiceProducto = new ServiceProducto();
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Producto_proveedor oProvedorI = _ServiceProducto.asociarProveedor(pr);
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
                TempData["Redirect"] = "mantenimientoProductos";
                TempData["Redirect-Action"] = "mantenimientoProductos";
                // Redireccion a la captura del Error
                return new JsonResult { Data = new { idProducto = 0, idProveedor = 0, idSucursal = 0 } };
            }
            return new JsonResult { Data = new { idProducto = 0, idProveedor = 0, idSucursal = 0 } };
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public JsonResult asociarAmbos(Producto objProducto, Sucursal objSucursal, Proveedor objProveedor)
        {
            Producto_proveedor pr = new Producto_proveedor();
            pr.idProducto = (int)objProducto.idProducto;
            pr.idProveedor = (int)objProveedor.idProveedor;

            Ubicacion u = new Ubicacion();
            u.idProducto = (int)objProducto.idProducto;
            u.idSucursal = (int)objSucursal.idSucursal;

            IServiceProducto _ServiceProducto = new ServiceProducto();
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Producto_proveedor oProvedorI = _ServiceProducto.asociarProveedor(pr);
                    Ubicacion oUbicacionI = _ServiceProducto.asociarUbicacion(u);
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
                TempData["Redirect"] = "mantenimientoProductos";
                TempData["Redirect-Action"] = "mantenimientoProductos";
                // Redireccion a la captura del Error
                return new JsonResult { Data = new { idProducto = 0, idProveedor = 0, idSucursal = 0 } };
            }
            return new JsonResult { Data = new { idProducto = 0, idProveedor = 0, idSucursal = 0 } };
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult Borrar(int? idBorrar)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto p = new Producto();
            p = _ServiceProducto.GetProductoByID(idBorrar.Value);
            p.activo = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Producto oProductoI = _ServiceProducto.guardarProducto(p);                    
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("listadoProducto", p);
                }

                return RedirectToAction("listadoProducto", "Producto");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "listadoProducto";
                TempData["Redirect-Action"] = "listadoProducto";
                // Redireccion a la captura del Error
                return RedirectToAction("listadoProducto", "Producto");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult Activar(int? id)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto p = new Producto();
            p = _ServiceProducto.GetProductoByID(id.Value);
            p.activo = 1;

            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Producto oProductoI = _ServiceProducto.guardarProducto(p);                    
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("listadoProductoInactivo", p);
                }

                return RedirectToAction("listadoProductoInactivo", "Producto");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "listadoProductoInactivo";
                TempData["Redirect-Action"] = "listadoProductoInactivo";
                // Redireccion a la captura del Error
                return RedirectToAction("listadoProductoInactivo", "Producto");
            }
        }
    }
}