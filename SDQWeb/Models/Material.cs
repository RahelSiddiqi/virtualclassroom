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
    
    public partial class Material
    {
        public Material()
        {
            this.Sessions = new HashSet<Session>();
        }
    
        public int ID { get; set; }
        public string LectureSlide { get; set; }
        public string VideoLink { get; set; }
        public string PresentationFile { get; set; }
        public string Notes { get; set; }
    
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
