//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DETALLE_SERVICIO
    {
        public int ID_DETALLE_SERVICIO { get; set; }
        public int ID_RESERVA { get; set; }
        public int ID_SERVICIO { get; set; }
        public Nullable<int> CANTIDAD_SERVICIO { get; set; }
        public Nullable<decimal> SUBTOTAL { get; set; }
    
        public virtual RESERVA RESERVA { get; set; }
        public virtual SERVICIO SERVICIO { get; set; }
    }
}