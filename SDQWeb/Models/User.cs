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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        public User()
        {
            this.Discussions = new HashSet<Discussion>();
            this.Notes = new HashSet<Note>();
            this.Posts = new HashSet<Post>();
            this.Students = new HashSet<Student>();
            this.Teachers = new HashSet<Teacher>();
            this.UserInstitutes = new HashSet<UserInstitute>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int MobileNo { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Password { get; set; }
        public Nullable<int> PresentAddressId { get; set; }
        public Nullable<int> AddressID { get; set; }
        public string Profile { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual PresentAddress PresentAddress { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<UserInstitute> UserInstitutes { get; set; }
    }
}
