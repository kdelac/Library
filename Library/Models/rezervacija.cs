//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class rezervacija
    {
        public int korisnik_id { get; set; }
        public int knjiga_id { get; set; }
        public System.DateTime datum_do { get; set; }
    
        public virtual knjiga knjiga { get; set; }
        public virtual korisnik korisnik { get; set; }
    }
}