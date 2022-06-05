using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class RegistroController : Controller
    {
        // GET: Registro
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexAdmin()
        {
            IEnumerable <USUARIO> lista = null;
            try
            {
                IServiceUsuario _SeviceUsuario = new ServiceUsuario();
                lista = _SeviceUsuario.GetUsuarioReserva();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            ViewBag.titulo = "Usuarios";
            return View(lista);
        }
        [HttpPost]
        public ActionResult Save(USUARIO usuario)
        {
            MemoryStream target = new MemoryStream();
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();
            try
            {

                if (ModelState.IsValid)
                {
                    USUARIO oUsuario = _ServiceUsuario.Save(usuario);
                    TempData["Message"] = Utils.SweetAlertHelper.Mensaje("Login", "Usuario registrado satisfactoriamente", SweetAlertMessageType.success);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("Index", usuario);
                }
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Registro";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }

        
        public ActionResult SaveAdmin(USUARIO usuario)
        {
            MemoryStream target = new MemoryStream();
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();
            try
            {

                if (ModelState.IsValid)
                {
                    USUARIO oUsuario = _ServiceUsuario.Save(usuario);
                    TempData["Message"] = Utils.SweetAlertHelper.Mensaje("Login", "Usuario registrado satisfactoriamente", SweetAlertMessageType.success);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("Index", usuario);
                }
                return RedirectToAction("IndexAdmin", "Registro");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Registro";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Details(int? id)
        {
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();
            USUARIO oUSUARIO = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                oUSUARIO = _ServiceUsuario.GetUsuarioByID(id.Value);
                if (oUSUARIO == null)
                {
                    TempData["Message"] = "No existe el usuario solicitado";
                    TempData["Redirect"] = "Registro";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                return View(oUSUARIO);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Registro";
                TempData["Redirect-Action"] = "IndexAdmin";
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();
            USUARIO oUsuario = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                oUsuario = _ServiceUsuario.GetUsuarioByID(id.Value);
                if (oUsuario == null)
                {
                    TempData["Message"] = "No existe el usuario solicitado";
                    TempData["Redirect"] = "Registro";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                return View(oUsuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Servicio";
                TempData["Redirect-Action"] = "IndexAdmin";
                return RedirectToAction("Default", "Error");
            }
        }
    }
}