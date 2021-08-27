using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
        internal partial class UsuarioMetadata
        {
            public int idUsuario { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            [DataType(DataType.Text, ErrorMessage = "{0} no tiene formato válido")]
            public int idRol { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string clave { get; set; }
            public string nombre { get; set; }
            public int habilitado { get; set; }
            public virtual Rol Rol { get; set; }
            public virtual ICollection<DetMovimiento> DetMovimiento { get; set; }
    }
    internal partial class ProductoMetadata
    {
        [Display(Name = "CodigoProducto")]
        public int idProducto { get; set; }
        [Display(Name = "Botánica")]
        public int idBotanica { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
        public int costoUnitario { get; set; }
        public int cantidadMinima { get; set; }
        [Display(Name = "Total inventario")]
        public int cantidadTotal { get; set; }
        public int activo { get; set; }

        public virtual Botanica Botanica { get; set; }
        public virtual ICollection<DetMovimiento> DetMovimiento { get; set; }
        public virtual ICollection<Producto_proveedor> Producto_proveedor { get; set; }
        public virtual ICollection<Ubicacion> Ubicacion { get; set; }
    }
}

