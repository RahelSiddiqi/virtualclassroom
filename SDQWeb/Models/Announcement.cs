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
    
    public partial class Announcement
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Details { get; set; }
        public System.DateTime Date { get; set; }
        public int CourseId { get; set; }
    
        public virtual Coursse Coursse { get; set; }
    }
}
