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
    
    public partial class Chocolate_has_Ingridients
    {
        public System.Guid Chocolazte_ID { get; set; }
        public System.Guid Ingerdient_ID { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual Chocolate Chocolate { get; set; }
        public virtual Ingredients Ingredients { get; set; }
    }
}
