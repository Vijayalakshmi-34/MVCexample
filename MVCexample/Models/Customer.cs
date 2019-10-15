using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCexample.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Name cannot be empty")]
        [Column(TypeName ="varchar")]
        [Display(Name ="Name")]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "DOB cannot be empty")]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Gender cannot be empty")]
        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Mobile Number cannot be empty")]
        [Display(Name = "Mobile Number")]
        public long? MobileNumber { get; set; }

        [Required(ErrorMessage ="City cannot be empty")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Your City")]
        [StringLength(100)]
        public string City { get; set; }

        //Reference table
        public MembershipType MembershipType { get; set; }

        //Reference column(foreign key)

        [Required]
        public int? MembershipTypeID { get; set; }

    }
}