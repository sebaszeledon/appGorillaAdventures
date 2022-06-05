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
    public class CuadraController : Controller
    {
        // GET: Cuadra
        public ActionResult Index()
        {
            IEnumerable<CUADRACICLO> lista = null;
            try
            {
                IServiceCuadra _SeviceCuadra = new ServiceCuadra();
                lista = _SeviceCuadra.GetCuadra();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            ViewBag.titulo = "Cuadraciclos";
            return View(lista);
        }
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexAdmin()
        {
            IEnumerable<CUADRACICLO> lista = null;
            try
            {
                IServiceCuadra _SeviceCuadra = new ServiceCuadra();
                lista = _SeviceCuadra.GetCuadra();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            ViewBag.titulo = "Cuadraciclos";
            return View(lista);
        }

        // GET: Cuadra/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int? id)
        {
            ServiceCuadra _ServiceCuadra = new ServiceCuadra();
            CUADRACICLO oCuadra = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                oCuadra = _ServiceCuadra.GetCuadraById(id.Value);
                if (oCuadra == null)
                {
                    TempData["Message"] = "No existe el cuadraciclo solicitado";
                    TempData["Redirect"] = "Cuadra";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                return View(oCuadra);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Cuadra";
                TempData["Redirect-Action"] = "IndexAdmin";
                return RedirectToAction("Default", "Error");
            }
        }

        private SelectList listaMarcas(int idMarca = 1)
        {
            //Lista de autores
            IServiceMarca _ServiceMarca = new ServiceMarca();
            IEnumerable<MARCA> listaMarcas = _ServiceMarca.GetMarca();
            //Autor SelectAutor = listaAutores.Where(c => c.IdAutor == idAutor).FirstOrDefault();
            return new SelectList(listaMarcas, "ID_MARCA", "DESCRIPCION", idMarca);
        }

        private MultiSelectList listaExtras(ICollection<EXTRA> extras)
        {
            //Lista de Categorias
            IServiceExtra _ServiceExtra = new ServiceExtra();
            IEnumerable<EXTRA> listaExtras = _ServiceExtra.GetExtra();
            int[] listaExtrasSelect = null;

            if (extras != null)
            {

                listaExtrasSelect = extras.Select(e => e.ID_EXTRA).ToArray();
            }

            return new MultiSelectList(listaExtras, "ID_EXTRA", "DESCRIPCION", listaExtrasSelect);

        }

        // GET: Cuadra/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            ViewBag.IdMarca = listaMarcas();
            ViewBag.IdExtra = listaExtras(null);
            return View();
        }

        // GET: Cuadra/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            ServiceCuadra _ServiceCuadra = new ServiceCuadra();
            CUADRACICLO oCuadra = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                oCuadra = _ServiceCuadra.GetCuadraById(id.Value);
                if (oCuadra == null)
                {
                    TempData["Message"] = "No existe el cuadraciclo solicitado";
                    TempData["Redirect"] = "Cuadra";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                ViewBag.IdMarca = listaMarcas();
                ViewBag.IdExtra = listaExtras(oCuadra.EXTRA);
                return View(oCuadra);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Cuadra";
                TempData["Redirect-Action"] = "IndexAdmin";
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Cuadra/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        [HttpPost]
        public ActionResult Save(CUADRACICLO cuadra, HttpPostedFileBase ImageFile, string[] selectedExtras)
        {
            MemoryStream target = new MemoryStream();
            ServiceCuadra _ServiceCuadra = new ServiceCuadra();
            try
            {
                if (cuadra.FOTO == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        cuadra.FOTO = target.ToArray();
                        ModelState.Remove("FOTO");
                    }

                }
                if (ModelState.IsValid)
                {
                    CUADRACICLO oCuadra = _ServiceCuadra.Save(cuadra, selectedExtras);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    ViewBag.IdMarca = listaMarcas();
                    ViewBag.IdExtra = listaExtras(cuadra.EXTRA);
                    return View("Create", cuadra);
                }
                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Cuadra";
                TempData["Redirect-Action"] = "IndexAdmin";
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Cuadra/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cuadra/Delete/5
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
