//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDQWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Routine
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public System.TimeSpan Time { get; set; }
        public int CourseId { get; set; }
        public int Room { get; set; }
    
        public virtual Coursse Coursse { get; set; }
    }
}
