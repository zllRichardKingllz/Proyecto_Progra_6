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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            ViewBag.REGISTRADO = TempData["REGISTRADO"] as string;
            ViewBag.logOut = TempData["logOut"] as string;
            ViewBag.EXISTE = TempData["EXISTE"] as string;
            return View();
        }
        public ActionResult Login(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = null;
            try
            {
                if (ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.GetUsuario(usuario.idUsuario, usuario.clave);

                    if (oUsuario != null)
                    {
                        TempData["logExito"] = "true";
                        Session["User"] = oUsuario;
                        Log.Info($"Accede {oUsuario.nombre} con el rol {oUsuario.Rol.idRol}-{oUsuario.Rol.descripcion}");                        
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"{usuario.nombre} se intentó conectar  y falló");
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Login", "Error al autenticarse", SweetAlertMessageType.warning);

                    }
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult UnAuthorized()
        {
            try
            {
                ViewBag.Message = "No autorizado";

                if (Session["User"] != null)
                {
                    Usuario oUsuario = Session["User"] as Usuario;
                    Log.Warn($"El usuario {oUsuario.nombre} con el rol {oUsuario.Rol.idRol}-{oUsuario.Rol.descripcion}, intentó acceder una página sin permisos  ");
                }

                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Logout()
        {
            try
            {
                TempData["logOut"] = "true";
                Log.Info("Usuario desconectado!");
                Session["User"] = null;
                return RedirectToAction("Index", "Usuario");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaRoles(int idRol = 0)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Rol> listaRol = _ServiceUsuario.GetRoles();
            return new SelectList(listaRol, "idRol", "descripcion", idRol);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaUsuarios(int idUsuario = 0)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuario> listaUsuarios = _ServiceUsuario.listaUsuarios();
            return new SelectList(listaUsuarios, "idUsuario", "nombre", idUsuario);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        private SelectList listaHabilitar(int habilitado = 3)
        {
            List<Usuario> u = new List<Usuario>();

            u = new List<Usuario>()
            {
                new Usuario(){habilitado=0, nombre = "Deshabilitar"},
                new Usuario(){habilitado=1, nombre = "Habilitar"}
            };
            return new SelectList(u, "habilitado", "nombre", habilitado);
        }
        public PartialViewResult _Registro()
        {            
            return PartialView();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult listadoUsuarios()
        {
            ViewBag.CONFIRMADO = TempData["CONFIRMADO"] as string;
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuario> usuario = _ServiceUsuario.listaUsuarios();
            return View(usuario);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult controlUsuarios()
        {
            ViewBag.CONFIRMADO = TempData["CONFIRMADO"] as string;
            ViewBag.IDUSUARIOS = listaUsuarios();
            ViewBag.HABILITAR = listaHabilitar();
            ViewBag.IDROL = listaRoles();
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult registroUsuariosAdmin()
        {
            ViewBag.EXISTE = TempData["EXISTE"] as string;
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult _EditarUsuariosAdmin(int? id)
        {

            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario u = new Usuario();

            u = _ServiceUsuario.GetUsuarioByID(id.Value);
            return View(u);
        }
        [HttpPost]
        public ActionResult GuardarUsuario(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario u = new Usuario();
            Usuario validar = _ServiceUsuario.GetUsuarioByIDSINPASSWORD(usuario.idUsuario);

            u.idUsuario = usuario.idUsuario;
            u.idRol = usuario.idRol;
            u.nombre = usuario.nombre;
            u.clave = usuario.clave;
            u.habilitado = usuario.habilitado;

            try
            {
                if (validar == null)
                {
                    if (ModelState.IsValid)
                    {
                        Usuario oUsuarioI = _ServiceUsuario.Save(u);
                        TempData["REGISTRADO"] = "true";
                    }
                    else
                    {
                        // Valida Errores si Javascript está deshabilitado
                        Util.ValidateErrors(this);
                        return View("Index", usuario);
                    }
                }
                else
                {
                    TempData["EXISTE"] = "true";
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Usuario");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Index";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Index", "Usuario");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public ActionResult GuardarUsuarioAdmin(Usuario usuario, int editar)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario u = new Usuario();
            Usuario validar = _ServiceUsuario.GetUsuarioByIDSINPASSWORD(usuario.idUsuario);

            u.idUsuario = usuario.idUsuario;
            u.idRol = usuario.idRol;
            u.nombre = usuario.nombre;
            u.clave = usuario.clave;
            u.habilitado = usuario.habilitado;

            try
            {
                if (editar == 0)
                {
                    if (validar == null)
                    {
                        if (ModelState.IsValid)
                        {
                            TempData["CONFIRMADO"] = "true";
                            Usuario oUsuarioI = _ServiceUsuario.Save(u);                            
                        }
                        else
                        {
                            // Valida Errores si Javascript está deshabilitado
                            Util.ValidateErrors(this);
                            return View("registroUsuariosAdmin", usuario);
                        }
                    }
                    else
                    {
                        TempData["EXISTE"] = "true";
                        return RedirectToAction("registroUsuariosAdmin");
                    }
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        TempData["CONFIRMADO"] = "true";
                        Usuario oUsuarioI = _ServiceUsuario.Save(u);
                    }
                    else
                    {
                        // Valida Errores si Javascript está deshabilitado
                        Util.ValidateErrors(this);
                        return View("_EditarUsuariosAdmin", usuario);
                    }
                }

                return RedirectToAction("listadoUsuarios");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "registroUsuariosAdmin";
                TempData["Redirect-Action"] = "registroUsuariosAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("registroUsuariosAdmin", "Usuario");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Habilitado.SI)]
        public JsonResult Asociar(Usuario objUsuario, Usuario objRol, Usuario objHabilitado)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario pr = new Usuario();
            Usuario data = new Usuario();
            data = _ServiceUsuario.GetUsuarioByID(objUsuario.idUsuario);
            
            pr.idRol = (int)objRol.idRol;
            pr.habilitado = (int)objHabilitado.habilitado;
            pr.idUsuario = data.idUsuario;
            pr.nombre = data.nombre;
            pr.clave = data.clave;

            try
            {
                if (ModelState.IsValid)
                {
                    TempData["CONFIRMADO"] = "true";
                    Usuario oUsuarioI = _ServiceUsuario.AsociarHR(pr);
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
                TempData["Redirect"] = "controlUsuarios";
                TempData["Redirect-Action"] = "controlUsuarios";
                // Redireccion a la captura del Error
                return new JsonResult { Data = new { idUsuario = 0, idRol = 0, habilitado = 0 } };
            }
            return new JsonResult { Data = new { idUsuario = 0, idRol = 0, habilitado = 0 } };
        }        
    }
}