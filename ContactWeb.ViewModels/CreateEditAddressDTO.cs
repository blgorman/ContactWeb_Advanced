using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContactWeb.ViewModels
{
    public class CreateEditAddressDTO
    {
        public virtual int Id { get; set; }

        [Required]
        public virtual int ContactId { get; set; }

        [StringLength(255)]
        [EmailAddress(ErrorMessage = "Please Enter a valid email")]
        public string Email { get; set; }

        [StringLength(12)]
        public string PhonePrimary { get; set; }
        [StringLength(12)]
        public string PhoneSecondary { get; set; }

        [StringLength(255)]
        public string StreetAddress1 { get; set; }
        [StringLength(255)]
        public string StreetAddress2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        public virtual int StateId { get; set; }
        public string StateName { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        public SelectList States { get; set; }
    }
}
