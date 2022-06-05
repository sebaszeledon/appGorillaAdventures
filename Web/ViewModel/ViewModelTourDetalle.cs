using Infraestructure.Models;
using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelTourDetalle
    {
        public int IdOrden { get; set; }
        public int IdTour { get; set; }
        public byte[] Imagen { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio
        {
            get { return SERVICIO.PRECIO; }
        }
        public virtual SERVICIO SERVICIO { get; set; }

        public decimal SubTotal
        {
            get { return calculoSubtotal(); }
        }

        private decimal calculoSubtotal()
        {
            decimal subtotal = this.Precio * this.Cantidad;
            return subtotal;
        }

        public ViewModelTourDetalle(int IdTour)
        {
            IServiceServicio _ServiceTour = new ServiceServicio();
            this.IdTour = IdTour;
            this.SERVICIO = _ServiceTour.GetServicioByID(IdTour);
        }

        public ViewModelTourDetalle()
        {

        }

    }
}