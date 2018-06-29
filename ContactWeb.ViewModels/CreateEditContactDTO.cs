using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ContactWeb.ViewModels
{
    public class CreateEditContactDTO
    {
        public CreateEditContactDTO()
        {
            Addresses = new List<AddressListDto>();
        }

        public int? Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public List<AddressListDto> Addresses { get; set; }

        public string ContactDisplayName => FirstName + ' ' + LastName;
    }
}
