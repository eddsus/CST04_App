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
    
    public partial class Ingredients
    {
        public System.Guid ID_Ingredients { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public string UnitType { get; set; }
        public bool Availability { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}