using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class CuadraMetadata
    {
        [Display(Name = "ID Cuadra")]
        public int ID_CUADRACICLO { get; set; }

        [Display(Name = "ID Marca")]
        public Nullable<int> ID_MARCA { get; set; }

        [Display(Name = "Tarifa por hora")]
        public Nullable<decimal> TARIFA { get; set; }

        [Display(Name = "Cantidad disponible")]
        public Nullable<int> INVENTARIO { get; set; }

        [Display(Name = "Fotografía")]
        public byte[] FOTO { get; set; }

        public virtual ICollection<DETALLE_CUADRACICLO> DETALLE_CUADRACICLO { get; set; }

        [Display(Name = "Marca")]
        public virtual MARCA MARCA { get; set; }

        [Display(Name = "Extras")]
        public virtual ICollection<EXTRA> EXTRA { get; set; }
    }
    internal partial class MarcaMetadata
    {
        [Display(Name = "Marca")]
        public int ID_MARCA { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPCION { get; set; }

        public virtual ICollection<CUADRACICLO> CUADRACICLO { get; set; }
    }

    internal partial class ExtraMetadata
    {
        [Display(Name = "Extras")]
        public string DESCRIPCION { get; set; }
    }
    internal partial class TipoUsuarioMetadata
    {
        [Display(Name = "Descripción")]
        public string DESCRIPCION { get; set; }
    }

    internal partial class ServicioMetadata
    {
        [Display(Name = "Descripción")]
        public string DESCRIPCION { get; set; }

        [Display(Name = "Precio")]
        public Nullable<decimal> PRECIO { get; set; }

        [Display(Name = "Duración")]
        public Nullable<int> DURACION { get; set; }

        [Display(Name = "Fotografía")]
        public byte[] FOTO { get; set; }
    }

    internal partial class UsuarioMetadata
    {
        [Display(Name = "ID Usuario")]
        public int ID_USUARIO { get; set; }

        [Display(Name = "Tipo de Usuario")]
        public int TIPO_USUARIO { get; set; }
        [Display(Name = "Identificación")]
        public string NUM_IDENTIFICACION { get; set; }

        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }

        [Display(Name = "Primer apellido")]
        public string PRIMER_APELLIDO { get; set; }

        [Display(Name = "Segundo apellido")]
        public string SEGUNDO_APELLIDO { get; set; }

        [Display(Name = "Teléfono")]
        public string TELEFONO { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} no tiene formato válido")]
        [Display(Name = "Correo")]
        public string CORREO { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Contraseña")]
        public string CONTRASENA { get; set; }

        [Display(Name = "Estado")]
        public Nullable<bool> ESTADO { get; set; }
    }

    internal partial class ReservaMetadata
    {
        [Display(Name = "ID Reserva")]
        public int ID_RESERVA { get; set; }

        [Display(Name = "Usuario")]
        public int ID_USUARIO { get; set; }

        [Display(Name = "Tipo de tarjeta")]
        public Nullable<int> ID_TARJETA { get; set; }

        [Display(Name = "Número de tarjeta")]
        public string NUM_TARJETA { get; set; }
        public Nullable<decimal> SUBTOTAL { get; set; }
        public Nullable<decimal> IMPUESTO { get; set; }
        public Nullable<decimal> TOTAL { get; set; }

        [Display(Name = "Fecha de pago")]
        public Nullable<System.DateTime> FECHA_PAGO { get; set; }

        [Display(Name = "Fecha de reservación")]
        public Nullable<System.DateTime> FECHA_RESERVA { get; set; }
    }
}
