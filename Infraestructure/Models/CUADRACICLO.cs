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
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(CuadraMetadata))]

    public partial class CUADRACICLO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CUADRACICLO()
        {
            this.DETALLE_CUADRACICLO = new HashSet<DETALLE_CUADRACICLO>();
            this.EXTRA = new HashSet<EXTRA>();
        }
    
        public int ID_CUADRACICLO { get; set; }
        public Nullable<int> ID_MARCA { get; set; }
        public decimal TARIFA { get; set; }
        public Nullable<int> INVENTARIO { get; set; }
        public byte[] FOTO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_CUADRACICLO> DETALLE_CUADRACICLO { get; set; }
        public virtual MARCA MARCA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXTRA> EXTRA { get; set; }
    }
}
