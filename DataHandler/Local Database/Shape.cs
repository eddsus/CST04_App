//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataHandler.Local_Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shape
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shape()
        {
            this.Chocolate = new HashSet<Chocolate>();
        }
    
        public System.Guid ID_Shape { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chocolate> Chocolate { get; set; }
    }
}
