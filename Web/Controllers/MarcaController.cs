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
    public class MarcaController : Controller
    {
        // GET: Marca
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<MARCA> lista = null;
            try
            {
                IServiceMarca _SeviceMarca = new ServiceMarca();
                lista = _SeviceMarca.GetMarca();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }

        // GET: Marca/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Marca/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marca/Create
        [CustomAuthorize((int)Roles.Administrador)]
        [HttpPost]
        public ActionResult Save(MARCA marca)
        {
            MemoryStream target = new MemoryStream();
            ServiceMarca _ServiceMarca = new ServiceMarca();
            try
            {

                if (ModelState.IsValid)
                {
                    MARCA oMarca = _ServiceMarca.Save(marca);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    return View("Create", marca);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Marca";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Marca/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            ServiceMarca _ServiceMarca = new ServiceMarca();
            MARCA oMarca = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oMarca = _ServiceMarca.GetMarcaByID(id.Value);
                if (oMarca == null)
                {
                    TempData["Message"] = "No existe el cuadraciclo solicitado";
                    TempData["Redirect"] = "Marca";
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }
                return View(oMarca);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Marca";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Marca/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
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

        // GET: Marca/Delete/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Marca/Delete/5
        [CustomAuthorize((int)Roles.Administrador)]
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
