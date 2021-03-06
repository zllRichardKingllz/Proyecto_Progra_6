//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetMovimiento
    {
        public int idMovimiento { get; set; }
        public int idProducto { get; set; }
        public int idProveedor { get; set; }
        public int idSucursal { get; set; }
        public int idUsuario { get; set; }
        public int cantidad { get; set; }
        public string observacion { get; set; }
    
        public virtual Movimiento Movimiento { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
