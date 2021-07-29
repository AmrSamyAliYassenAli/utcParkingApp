using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class AdminUsers : BaseModel
    {
        public AdminUsers()
        {
            PrivilageGroupId = 0;
            UserPrivilage = new HashSet<UserPrivilage>();
        }
        //[Required]
        public string Fullname { get; set; }
        //[Required]
        public string Email { get; set; }
        //[Required]
        public string Mobile { get; set; }
        //[Required]
        public string Username { get; set; }
        //[Required]
        public string Password { get; set; }
        public string Image { get; set; }
        

        //[NotMapped] // Does not effect with your database
        //[Compare("Password")]
        //public string ConfirmPassword { get; set; }
        public int? ParkingId { get; set; }
        public int? UserTypeId { get; set; }
        public int? PrivilageGroupId { get; set; }

        public virtual Parking Parking { get; set; }
        public virtual UserType UserType { get; set; }

        public virtual ICollection<UserPrivilage> UserPrivilage { get; set; }
    }
}
