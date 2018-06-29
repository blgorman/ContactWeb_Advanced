using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactWeb.ViewModels
{
    public class AddressListDto
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string Email { get; set; }
        public string PhonePrimary { get; set; }
        public string PhoneSecondary { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public string Zip { get; set; }
    }
}
