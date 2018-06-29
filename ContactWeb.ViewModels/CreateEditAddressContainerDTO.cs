using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContactWeb.ViewModels
{
    public class CreateEditAddressContainerDTO
    {
        private List<AddressListDto> _contactAddresses;
        
        public CreateEditAddressContainerDTO()
        {
            //have to have this or get a page error
            //take it out and try to submit to see the error.
        }

        public CreateEditAddressContainerDTO(int contactId, SelectList states, string contactName)
        {
            _contactAddresses = new List<AddressListDto>();
            ContactId = contactId;
            var newAddress = new CreateEditAddressDTO();
            newAddress.ContactId = contactId;
            newAddress.States = states;
            NewEditAddress = newAddress;
            ContactDisplayName = contactName;
        }
        
        public int ContactId { get; }

        public string ContactDisplayName { get; }
        
        public List<AddressListDto> Addresses { get; set; }
        public CreateEditAddressDTO NewEditAddress { get; set; }
    }
}
