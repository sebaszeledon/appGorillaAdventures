using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelReservaDetalle
    {
        public int IdOrden { get; set; }
        public int IdCuadra { get; set; }
        public byte[] Imagen { get; set; }
        public int Cantidad { get; set; }
        public int CantidadHoras { get; set; }

        public decimal Tarifa
        {
            get { return CUADRACICLO.TARIFA; }

        }
        public int? Disponible
        {
            get { return CUADRACICLO.INVENTARIO; }
        }
        public virtual CUADRACICLO CUADRACICLO { get; set; }
        //public virtual ViewModelTourDetalle ViewModelTourDetalle { get; set; }

        public decimal SubTotal
        {
            get
            {
                return calculoSubtotal();
            }
        }
        private decimal calculoSubtotal()
        {
            
            decimal subtotal = this.Tarifa * this.Cantidad * this.CantidadHoras;

            return subtotal;
        }

        public ViewModelReservaDetalle(int IdCuadra)
        {
            IServiceCuadra _ServiceCuadra = new ServiceCuadra();
            this.IdCuadra = IdCuadra;
            this.CUADRACICLO = _ServiceCuadra.GetCuadraById(IdCuadra);
        }

        public ViewModelReservaDetalle()
        {

        }



    }
}