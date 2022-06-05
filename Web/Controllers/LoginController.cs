using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(USUARIO usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            USUARIO oUsuario = null;
            try
            {
                if (ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.GetUsuario(usuario.CORREO, usuario.CONTRASENA);

                    if (oUsuario != null && oUsuario.TIPO_USUARIO == 1 && oUsuario.ESTADO == true)
                    {
                        Session["User"] = oUsuario;
                        Log.Info($"Accede {oUsuario.NOMBRE} {oUsuario.PRIMER_APELLIDO} con el rol {oUsuario.TIPO_USUARIO1.ID_TIPO_USUARIO}-{oUsuario.TIPO_USUARIO1.DESCRIPCION}");
                        TempData["mensaje"] = Utils.SweetAlertHelper.Mensaje("Login", "Usario autenticado satisfactoriamente", SweetAlertMessageType.success);
                        return RedirectToAction("IndexAdmin", "Home");
                    }
                    else
                    {
                        if (oUsuario != null && oUsuario.TIPO_USUARIO == 2 && oUsuario.ESTADO == true)
                        {
                            Session["User"] = oUsuario;
                            Log.Info($"Accede {oUsuario.NOMBRE} {oUsuario.PRIMER_APELLIDO} con el rol {oUsuario.TIPO_USUARIO1.ID_TIPO_USUARIO}-{oUsuario.TIPO_USUARIO1.DESCRIPCION}");
                            TempData["mensaje"] = Utils.SweetAlertHelper.Mensaje("Login", "Usario autenticado satisfactoriamente", SweetAlertMessageType.success);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Log.Warn($"{usuario.CORREO} se intentó conectar  y falló");
                            ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Inicio de sesión", "Error al autenticarse", SweetAlertMessageType.warning);

                        }
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
                    USUARIO oUsuario = Session["User"] as USUARIO;
                    Log.Warn($"El usuario {oUsuario.NOMBRE} {oUsuario.PRIMER_APELLIDO} con el rol {oUsuario.TIPO_USUARIO1.ID_TIPO_USUARIO}-{oUsuario.TIPO_USUARIO1.DESCRIPCION}, intentó acceder una página sin permisos  ");
                }

                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Login";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Logout()
        {
            try
            {
                Log.Info("Usuario desconectado!");
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Login";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }
    }
}