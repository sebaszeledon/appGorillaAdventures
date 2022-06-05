using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;
using Web.ViewModel;

namespace Web.Controllers
{
    public class ReservaController : Controller
    {
        // GET: Reserva
        public ActionResult Index()
        {
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }
            ViewBag.idUsuario = listaUsuarios();

            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            ViewBag.DetalleOrdenTour = Carrito.Instancia.ItemsTour;

            return View();
        }
        private SelectList listaUsuarios()
        {
            //Lista de Clientes
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<USUARIO> listaUsuarios = _ServiceUsuario.GetUsuarioReserva();

            return new SelectList(listaUsuarios, "ID_USUARIO", "NOMBRE");
        }

        private ActionResult DetalleCarrito()
        {

            return PartialView("_DetalleReserva", Carrito.Instancia.Items);
        }
        private ActionResult DetalleCarritoTour()
        {

            return PartialView("_DetalleTour", Carrito.Instancia.ItemsTour);
        }
        public ActionResult actualizarCantidad(int idCuadra, int cantidad)
        {
            ViewBag.DetalleReserva = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidad(idCuadra, cantidad);
            TempData.Keep();
            return PartialView("_DetalleReserva", Carrito.Instancia.Items);

        }

        public ActionResult actualizarCantidadHoras(int idCuadra, int cantidadHoras)
        {
            ViewBag.DetalleReserva = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidadHoras(idCuadra, cantidadHoras);
            TempData.Keep();
            return PartialView("_DetalleReserva", Carrito.Instancia.Items);

        }

        public ActionResult actualizarCantidadTours(int idTour, int cantidad)
        {
            ViewBag.DetalleReserva = Carrito.Instancia.ItemsTour;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemTourCantidad(idTour, cantidad);
            TempData.Keep();
            return PartialView("_DetalleTour", Carrito.Instancia.ItemsTour);

        }

        public ActionResult ordenarCuadra(int? idCuadra)
        {
            int cantidadCuadras = Carrito.Instancia.Items.Count();
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem((int)idCuadra);
            return PartialView("_OrdenCantidad");

        }

        public ActionResult ordenarTour(int? idTour)
        {
            int cantidadCuadras = Carrito.Instancia.ItemsTour.Count();
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarTour((int)idTour);
            return PartialView("_OrdenCantidad");

        }

        public ActionResult actualizarOrdenCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantidadCuadras = Carrito.Instancia.Items.Count();
            return PartialView("_OrdenCantidad");

        }
        public ActionResult eliminarCuadra(int? idCuadra)
        {
            ViewBag.NotificationMessage = Carrito.Instancia.EliminarItem((int)idCuadra);
            return PartialView("_DetalleReserva", Carrito.Instancia.Items);
        }

        public ActionResult eliminarTour(int? idTour)
        {
            ViewBag.NotificationMessage = Carrito.Instancia.EliminarItemTour((int)idTour);
            return PartialView("_DetalleTour", Carrito.Instancia.ItemsTour);
        }

        public ActionResult actualizarDesglose()
        {
            Carrito.Instancia.GetSubTotal();
            return PartialView("_Desglose", Carrito.Instancia.Items);
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexAdmin()
        {
            IEnumerable<RESERVA> lista = null;

            try
            {
                IServiceReserva _ServiceReserva = new ServiceReserva();
                lista = _ServiceReserva.GetReserva();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult MisReservas(int? id)
        {
            IEnumerable<RESERVA> lista = null;

            try
            {
                IServiceReserva _ServiceReserva = new ServiceReserva();
                lista = _ServiceReserva.GetReservaByUser(id);
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int? id)
        {
            ServiceReserva _ServiceReserva = new ServiceReserva();
            RESERVA reserva = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                reserva = _ServiceReserva.GetReservaByID(id.Value);
                if (reserva == null)
                {
                    TempData["Message"] = "No existe la reserva solicitada";
                    TempData["Redirect"] = "Reserva";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    //TempData.Keep();
                    return RedirectToAction("Default", "Error");
                }
                return View(reserva);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Reserva";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult DetailsClient(int? id)
        {
            ServiceReserva _ServiceReserva = new ServiceReserva();
            RESERVA reserva = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("_NoReservas");
                }

                reserva = _ServiceReserva.GetReservaByID(id.Value);
                if (reserva == null)
                {
                    TempData["Message"] = "No existe la reserva solicitada";
                    TempData["Redirect"] = "Reserva";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    //TempData.Keep();
                    return RedirectToAction("Default", "Error");
                }
                return View(reserva);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Reserva";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        [HttpPost]
        //[CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Save(RESERVA reserva)
        {
            try
            {
                
                // Si no existe la sesión no hay nada
                if (Carrito.Instancia.Items.Count() <= 0)
                {
                    
                    // Validaciones de datos requeridos.
                    TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Reserva", "Seleccione los cuadras a ordenar", SweetAlertMessageType.warning);
                    return RedirectToAction("Index");

                }

                else
                {

                    var listaDetalle = Carrito.Instancia.Items;
                    var listaDetalleTour = Carrito.Instancia.ItemsTour;
                    decimal? suma = 0;
                    decimal? total = 0;
                    decimal? sumaTour = 0;
                    decimal? totalTour = 0;

                    foreach (var item in listaDetalle)
                    {
                        DETALLE_CUADRACICLO ordenDetalle = new DETALLE_CUADRACICLO();
                        ordenDetalle.ID_CUADRACICLO = item.IdCuadra;
                        ordenDetalle.SUBTOTAL = item.Tarifa;
                        ordenDetalle.CANTIDAD_CUADRACICLO = item.Cantidad;
                        ordenDetalle.CANTIDAD_HORAS = item.CantidadHoras;
                        reserva.DETALLE_CUADRACICLO.Add(ordenDetalle);
                        
                        suma = ordenDetalle.SUBTOTAL * ordenDetalle.CANTIDAD_CUADRACICLO * ordenDetalle.CANTIDAD_HORAS;
                        total = suma + total;
                    }

                    foreach (var item in listaDetalleTour)
                    {
                        DETALLE_SERVICIO ordenDetalleTour = new DETALLE_SERVICIO();
                        ordenDetalleTour.ID_SERVICIO = item.IdTour;
                        ordenDetalleTour.SUBTOTAL = item.Precio;
                        ordenDetalleTour.CANTIDAD_SERVICIO = item.Cantidad;
                        reserva.DETALLE_SERVICIO.Add(ordenDetalleTour);

                        sumaTour = ordenDetalleTour.SUBTOTAL * ordenDetalleTour.CANTIDAD_SERVICIO;
                        totalTour = sumaTour + totalTour;
                    }

                    reserva.SUBTOTAL = total + totalTour;
                    reserva.IMPUESTO = ((total + totalTour) * 13) / 100;
                    reserva.TOTAL = reserva.SUBTOTAL + reserva.IMPUESTO;
                    
                }

                IServiceReserva _ServiceReserva = new ServiceReserva();
                RESERVA ordenSave = _ServiceReserva.Save(reserva);

                // Limpia el Carrito de compras
                Carrito.Instancia.eliminarCarrito();
                TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Reserva", "Reserva agregada satisfactoriamente!", SweetAlertMessageType.success);
                // Reporte orden
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error  
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}