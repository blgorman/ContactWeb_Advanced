using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactWeb.Models
{
    public class Contact
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public virtual IList<Address> Addresses { get; set; }
    }
}