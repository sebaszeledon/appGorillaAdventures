using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class ServicioController : Controller
    {
        // GET: Servicio
        public ActionResult Index()
        {
            IEnumerable<SERVICIO> lista = null;
            try
            {
                IServiceServicio _SeviceServicio = new ServiceServicio();
                lista = _SeviceServicio.GetServicio();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            ViewBag.titulo = "Tours";
            return View(lista);

        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexAdmin()
        {
            IEnumerable<SERVICIO> lista = null;
            try
            {
                IServiceServicio _SeviceServicio = new ServiceServicio();
                lista = _SeviceServicio.GetServicio();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            ViewBag.titulo = "Tours";
            return View(lista);
        }

        // GET: Servicio/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int? id)
        {
            ServiceServicio _ServiceServicio = new ServiceServicio();
            SERVICIO oServicio = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                oServicio = _ServiceServicio.GetServicioByID(id.Value);
                if (oServicio == null)
                {
                    TempData["Message"] = "No existe el cuadraciclo solicitado";
                    TempData["Redirect"] = "Servicio";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                return View(oServicio);
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

        // GET: Servicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicio/Create
        [CustomAuthorize((int)Roles.Administrador)]
        [HttpPost]
        public ActionResult Save(SERVICIO servicio, HttpPostedFileBase ImageFile)
        {
            MemoryStream target = new MemoryStream();
            ServiceServicio _ServiceServicio = new ServiceServicio();
            try
            {
                if (servicio.FOTO == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        servicio.FOTO = target.ToArray();
                        ModelState.Remove("FOTO");
                    }

                }
                if (ModelState.IsValid)
                {
                    SERVICIO oCuadra = _ServiceServicio.Save(servicio);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("Create", servicio);
                }
                return RedirectToAction("IndexAdmin");
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

        // GET: Servicio/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            ServiceServicio _ServiceServicio = new ServiceServicio();
            SERVICIO oServicio = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                oServicio = _ServiceServicio.GetServicioByID(id.Value);
                if (oServicio == null)
                {
                    TempData["Message"] = "No existe el cuadraciclo solicitado";
                    TempData["Redirect"] = "Servicio";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                return View(oServicio);
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

        // POST: Servicio/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Servicio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Servicio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
