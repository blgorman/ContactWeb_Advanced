using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactWeb.Models;
using ContactWeb.ViewModels;

namespace ContactWeb.Service
{
    public class AddressService
    {
        private readonly ContactWebContext _context;
        public AddressService(ContactWebContext context)
        {
            _context = context;
        }

        public IEnumerable<AddressListDto> GetAddressesByContactId(int contactId)
        {
            var states = _context.States.ToList();
            var addresses =  _context.Addresses.Where(x => x.ContactId == contactId);
            var addressDtos = new List<AddressListDto>();

            foreach (var address in addresses)
            {
                var dto = new AddressListDto();
                dto.ContactId = address.Id;
                dto.City = address.City;
                dto.Email = address.Email;
                dto.Id = address.Id;
                dto.PhonePrimary = address.PhonePrimary;
                dto.PhoneSecondary = address.PhoneSecondary;
                dto.StateId = address.StateId ?? 0;
                if (dto.StateId > 0)
                {
                    var theState = states.FirstOrDefault(x => x.Id == dto.StateId);
                    dto.StateName = theState?.Name;
                }
                dto.StreetAddress1 = address.StreetAddress1;
                dto.StreetAddress2 = address.StreetAddress2;
                dto.Zip = address.Zip;
                addressDtos.Add(dto);
            }

            return addressDtos;
        }

        public CreateEditAddressDTO GetAddressById(int id)
        {
            var addr = _context.Addresses.FirstOrDefault(x => x.Id == id);
            if (addr == null) return null;

            var addressDto = new CreateEditAddressDTO()
            {
                Id = addr.Id,
                StateId = addr.StateId ?? 0,
                ContactId = addr.ContactId,
                PhoneSecondary = addr.PhoneSecondary,
                PhonePrimary = addr.PhonePrimary,
                City = addr.City,
                Zip = addr.Zip,
                Email = addr.Email,
                StreetAddress2 = addr.StreetAddress2,
                StreetAddress1 = addr.StreetAddress1
            };

            return addressDto;
        }

        public int AddOrUpdateAddress(CreateEditAddressDTO address)
        {
            if (address.Id > 0)
            {
                return EditAddress(address);
            }
            return InsertAddress(address);
        }

        public int EditAddress(CreateEditAddressDTO address)
        {
            if (address.Id <= 0) return -1;

            var addr = _context.Addresses.FirstOrDefault(x => x.Id == address.Id);

            //update all the fields
            addr.ContactId = address.ContactId;
            addr.Email = address.Email;
            addr.PhonePrimary = address.PhonePrimary;
            addr.PhoneSecondary = address.PhoneSecondary;
            addr.StreetAddress1 = address.StreetAddress1;
            addr.StreetAddress2 = address.StreetAddress2;
            addr.City = address.City;
            addr.StateId = address.StateId;
            addr.Zip = address.Zip;

            _context.SaveChanges();

            return addr.Id;
        }

        public int InsertAddress(CreateEditAddressDTO address)
        {
            var a = new Address();
            a.Id = address.Id;
            a.ContactId = address.ContactId;
            a.Email = address.Email;
            a.PhonePrimary = address.PhonePrimary;
            a.PhoneSecondary = address.PhoneSecondary;
            a.StateId = address.StateId;
            a.StreetAddress1 = address.StreetAddress1;
            a.StreetAddress2 = address.StreetAddress2;
            a.City = address.City;
            a.StateId = address.StateId;
            a.Zip = address.Zip;

            _context.Addresses.Add(a);
            _context.SaveChanges();
            return a.Id;
        }

        public bool DeleteAddress(int id)
        {
            var addr = _context.Addresses.FirstOrDefault(x => x.Id == id);
            if (addr == null) return false;
            _context.Addresses.Remove(addr);
            return _context.SaveChanges() > 0;
        }
    }
}
