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
    
    public partial class Movimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Movimiento()
        {
            this.DetMovimiento = new HashSet<DetMovimiento>();
            this.RegistroMovimiento = new HashSet<RegistroMovimiento>();
        }
    
        public int idMovimiento { get; set; }
        public int idTipoMovimiento { get; set; }
        public int idConcepto { get; set; }
        public string fecha { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetMovimiento> DetMovimiento { get; set; }
        public virtual TipoMovimiento TipoMovimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroMovimiento> RegistroMovimiento { get; set; }
    }
}