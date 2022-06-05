using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Utils;

namespace Web.ViewModel
{
    public class Carrito
    {
        public List<ViewModelReservaDetalle> Items { get; private set; }
        public List<ViewModelTourDetalle> ItemsTour { get; private set; }

        private const double imp = 0.13;
        public static readonly Carrito Instancia;

        static Carrito()
        {
            // Si el carrito no está en la sesión, cree uno y guarde los items.
            if (HttpContext.Current.Session["carrito"] == null)
            {
                Instancia = new Carrito();
                Instancia.Items = new List<ViewModelReservaDetalle>();
                Instancia.ItemsTour = new List<ViewModelTourDetalle>();
                HttpContext.Current.Session["carrito"] = Instancia;
            }
            else
            {
                // De lo contrario, obténgalo de la sesión.
                Instancia = (Carrito)HttpContext.Current.Session["carrito"];
            }
        }

        protected Carrito() { }

        public String AgregarItem(int cuadraId)
        {
            String mensaje = "";
            // Crear un nuevo artículo para agregar al carrito
            ViewModelReservaDetalle nuevoItem = new ViewModelReservaDetalle(cuadraId);
            // Si este artículo ya existe en lista de cuadras, aumente la Cantidad
            // De lo contrario, agregue el nuevo elemento a la lista
            if (nuevoItem != null)
            {
                if (Items.Exists(x => x.IdCuadra == cuadraId))
                {
                    ViewModelReservaDetalle item = Items.Find(x => x.IdCuadra == cuadraId);
                    item.Cantidad++;
                    item.CantidadHoras = 1;
                }
                else
                {
                    nuevoItem.Cantidad = 1;
                    nuevoItem.CantidadHoras = 1;
                    Items.Add(nuevoItem);
                }
                mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "Cuadra agregado a la orden", SweetAlertMessageType.success);

            }
            else
            {
                mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "El cuadra solicitado no existe", SweetAlertMessageType.warning);
            }
            return mensaje;
        }

        public String AgregarTour(int tourId)
        {
            String mensaje = "";
            // Crear un nuevo artículo para agregar al carrito
            ViewModelTourDetalle nuevoItem = new ViewModelTourDetalle(tourId);
            // Si este artículo ya existe en lista de cuadras, aumente la Cantidad
            // De lo contrario, agregue el nuevo elemento a la lista
            if (nuevoItem != null)
            {
                if (ItemsTour.Exists(x => x.IdTour == tourId))
                {
                    ViewModelTourDetalle item = ItemsTour.Find(x => x.IdTour == tourId);
                    item.Cantidad++;
                }
                else
                {
                    nuevoItem.Cantidad = 1;
                    ItemsTour.Add(nuevoItem);
                }
                mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "Tour agregado a la orden", SweetAlertMessageType.success);

            }
            else
            {
                mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "El tour solicitado no existe", SweetAlertMessageType.warning);
            }
            return mensaje;
        }
        public String SetItemCantidad(int cuadraId, int Cantidad)
        {
            String mensaje = "";
            // Si estamos configurando la Cantidad a 0, elimine el artículo por completo
            if (Cantidad == 0)
            {
                EliminarItem(cuadraId);
                mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "Cuadra eliminado", SweetAlertMessageType.success);

            }
            else
            {
                // Encuentra el artículo y actualiza la Cantidad
                ViewModelReservaDetalle actualizarItem = new ViewModelReservaDetalle(cuadraId);
                if (Items.Exists(x => x.IdCuadra == cuadraId))
                {
                    ViewModelReservaDetalle item = Items.Find(x => x.IdCuadra == cuadraId);
                    item.Cantidad = Cantidad;
                    if (item.Disponible < Cantidad)
                    {
                        mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "Debe reducir la cantidad, inventario limitado", SweetAlertMessageType.warning);
                        item.Cantidad = Cantidad - 1;
                    }
                }
            }
            return mensaje;

        }

        public String SetItemCantidadHoras(int cuadraId, int CantidadHoras)
        {
            String mensaje = "";
            ViewModelReservaDetalle item = Items.Find(x => x.IdCuadra == cuadraId);
            item.CantidadHoras = CantidadHoras;
            if (item.CantidadHoras < 1)
            {
                item.CantidadHoras = 1;
                mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "La cantidad de horas debe ser igual o mayor a 1", SweetAlertMessageType.warning);
            }
            else
            {
                // Encuentra el artículo y actualiza la Cantidad de horas
                if (item.CantidadHoras > 8)
                {
                    mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "La cantidad máxima de horas de reserva es 8", SweetAlertMessageType.warning);
                    item.CantidadHoras = 8;
                }
            }
            return mensaje;

        }
        public String SetItemTourCantidad(int tourId, int Cantidad)
        {
            String mensaje = "";
            ViewModelTourDetalle item = ItemsTour.Find(x => x.IdTour == tourId);
            item.Cantidad = Cantidad;
            if (item.Cantidad < 1)
            {
                EliminarItemTour(tourId);
                mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "Tour eliminado", SweetAlertMessageType.success);
            }
            else
            {
                // Encuentra el artículo y actualiza la Cantidad de horas
                if (item.Cantidad > 15)
                {
                    mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "La cantidad máxima de personas es 15", SweetAlertMessageType.warning);
                    item.Cantidad = 15;
                }
            }
            return mensaje;

        }

        public String EliminarItem(int cuadraId)
        {
            String mensaje = "El cuadraciclo no existe";
            if (Items.Exists(x => x.IdCuadra == cuadraId))
            {
                var itemEliminar = Items.Single(x => x.IdCuadra == cuadraId);
                Items.Remove(itemEliminar);
                mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "Cuadra eliminado", SweetAlertMessageType.success);
            }
            return mensaje;

        }

        public String EliminarItemTour(int tourId)
        {
            String mensaje = "El tour no existe";
            if (ItemsTour.Exists(x => x.IdTour == tourId))
            {
                var itemEliminar = ItemsTour.Single(x => x.IdTour == tourId);
                ItemsTour.Remove(itemEliminar);
                mensaje = SweetAlertHelper.Mensaje("Reserva Cuadraciclo", "Tour eliminado", SweetAlertMessageType.success);
            }
            return mensaje;

        }

        public decimal GetSubTotal()
        {
            decimal total = 0;
            total = (Items.Sum(x => x.SubTotal)) + (ItemsTour.Sum(x => x.SubTotal));

            return total;
        }

        public double GetImpuesto()
        {
            double impuesto = decimal.ToDouble(GetSubTotal());
            double porcentaje = impuesto * Carrito.imp;

            return porcentaje;
        }

        public double GetTotal()
        {
            double subtotal = decimal.ToDouble(GetSubTotal());
            double imp = this.GetImpuesto();
            double total = subtotal + imp;
            return total;
        }

        public int GetCountItems()
        {
            int total = 0;
            total = Items.Sum(x => x.Cantidad);

            return total;
        }

        public void eliminarCarrito()
        {
            Items.Clear();
            ItemsTour.Clear();
        }
    }
}