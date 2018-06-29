using System.ComponentModel.DataAnnotations;

namespace ContactWeb.Models
{
    public class Address
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public virtual int ContactId { get; set; }

        [StringLength(255)]
        [EmailAddress(ErrorMessage ="Please Enter a valid email")]
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

        public virtual int? StateId { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

    }
}
