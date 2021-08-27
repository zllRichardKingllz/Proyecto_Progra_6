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
    public class ProcesosController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        public ActionResult Entrada()
        {
            ViewBag.IDPRODUCTO = listaProductos();
            ViewBag.IDPROVEDOR = listaProveedores();
            ViewBag.IDSUCURSAL = listaSucursales();
            ViewBag.IDTIPOMOVIMIENTOENTRADA = listaTipoMovimientoEntrada();
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        public ActionResult Salida()
        {
            ViewBag.IDPRODUCTO = listaProductos();
            ViewBag.IDPROVEDOR = listaProveedores();
            ViewBag.IDSUCURSAL = listaSucursales();
            ViewBag.IDTIPOMOVIMIENTOSALIDA = listaTipoMovimientoSalida();
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        private SelectList listaProveedores(int idProveedor = 0)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            IEnumerable<Proveedor> listaProveedores = _ServiceProveedor.GetProveedores();
            return new SelectList(listaProveedores, "idProveedor", "nombre", idProveedor);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        private SelectList listaSucursales(int idSucursal = 0)
        {
            IServiceProducto _ServiceSucursal = new ServiceProducto();
            IEnumerable<Sucursal> listaSucursales = _ServiceSucursal.GetSucursales();
            return new SelectList(listaSucursales, "idSucursal", "descripcion", idSucursal);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        private SelectList listaProductos(int idProducto = 0)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IEnumerable<Producto> listaProductos = _ServiceProducto.GetProductos();
            return new SelectList(listaProductos, "idProducto", "nombre", idProducto);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        private SelectList listaTipoMovimientoEntrada(int idTipo = 0)
        {
            int tipo = 1;
            IServiceMovimiento _ServiceMovimiento = new ServiceMovimiento();
            IEnumerable<TipoMovimiento> listaMovimientos = _ServiceMovimiento.GetTipoMovimiento(tipo);
            return new SelectList(listaMovimientos, "idConcepto", "descripcion", idTipo);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        private SelectList listaTipoMovimientoSalida(int idTipo = 0)
        {
            int tipo = 2;
            IServiceMovimiento _ServiceMovimiento = new ServiceMovimiento();
            IEnumerable<TipoMovimiento> listaMovimientos = _ServiceMovimiento.GetTipoMovimiento(tipo);
            return new SelectList(listaMovimientos, "idConcepto", "descripcion", idTipo);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        public ActionResult Compra()
        {
            ViewBag.IDPRODUCTO = listaProductos();
            ViewBag.IDPROVEDOR = listaProveedores();
            ViewBag.IDSUCURSAL = listaSucursales();
            ViewBag.IDTIPOMOVIMIENTOENTRADA = listaTipoMovimientoEntrada();
            return View();
        }
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        public JsonResult crearMovimiento(Movimiento objMovimiento)
        {
            IServiceMovimiento _ServiceMovimiento = new ServiceMovimiento();
            Movimiento m = new Movimiento();
            Movimiento cargarViewBag = new Movimiento();
            m.idTipoMovimiento = (int)objMovimiento.idTipoMovimiento;
            m.idConcepto = (int)objMovimiento.idConcepto;
            m.fecha = objMovimiento.fecha;
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Movimiento oMovimientoI = _ServiceMovimiento.guardarMovimiento(m);
                    cargarViewBag = _ServiceMovimiento.UltimoMovimiento();
                    ViewBag.ULTIMOMOVIMIENTO = cargarViewBag;
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
                TempData["Redirect"] = "Compra";
                TempData["Redirect-Action"] = "Compra";
                // Redireccion a la captura del Error
                return new JsonResult { Data = new { idTipoMovimiento = 0, idConcepto = 0, fecha = "" } };
            }
            return new JsonResult { Data = new { idTipoMovimiento = 0, idConcepto = 0, fecha = "" } };
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Bodeguero, (int)Habilitado.SI)]
        public ActionResult GuardarMovimiento(DetMovimiento detMovimiento, int numMov)
        {
            IServiceMovimiento _ServiceMovimiento = new ServiceMovimiento();
            DetMovimiento d = new DetMovimiento();

            d.idMovimiento = numMov;
            d.idProducto = detMovimiento.idProducto;
            d.idProveedor = detMovimiento.idProveedor;
            d.idSucursal = detMovimiento.idSucursal;
            d.idUsuario = detMovimiento.idUsuario;
            d.cantidad = detMovimiento.cantidad;
            d.observacion = detMovimiento.observacion;

            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    DetMovimiento oDetMovimientoI = _ServiceMovimiento.guardarDetMovimiento(d);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("Compra", detMovimiento);
                }

                return RedirectToAction("Compra");
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
    }
}