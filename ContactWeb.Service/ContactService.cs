using ContactWeb.Models;
using ContactWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ContactWeb.Service
{
    public class ContactService
    {
        private readonly ContactWebContext _context;

        public ContactService(ContactWebContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Getting all contacts as a list of list dtos from the db by UserId
        /// </summary>
        /// <param name="userId">The user Id to match</param>
        /// <returns>List of DTOs for contacts for the user</returns>
        public IEnumerable<ContactListDTO> GetAllContactsByUserId(Guid userId)
        {
            var states = _context.States.AsNoTracking().ToList();
            var contacts = _context.Contacts.AsNoTracking()
                                .Include("Addresses")
                                .Where(x => x.UserId == userId);

            var contactListDtos = new List<ContactListDTO>();
            foreach (var contact in contacts)
            {
                var email = contact.Addresses?.FirstOrDefault()?.Email ?? string.Empty;
                var phone = contact.Addresses?.FirstOrDefault()?.PhonePrimary ?? string.Empty;
                var state = contact.Addresses?.FirstOrDefault()?.StateId != null
                            && contact.Addresses?.FirstOrDefault()?.StateId > 0
                                ? states?.FirstOrDefault(x => x.Id == (contact.Addresses?.FirstOrDefault()?.StateId))?.Name
                                : string.Empty;
                var cityState = $"{contact.Addresses?.FirstOrDefault()?.City}, {state}";
                if (cityState.Trim().Equals(","))
                {
                    cityState = String.Empty;
                }

                var newDto = new ContactListDTO()
                {
                    Id = contact.Id,
                    Birthday = contact.Birthday,
                    Email = email,
                    CityState = cityState,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Phone = phone,
                    UserId = contact.UserId
                };
                contactListDtos.Add(newDto);
            }
            return contactListDtos;
        }

        public CreateEditContactDTO GetContactById(int id)
        {
            var states = _context.States.ToList();

            var contact = _context.Contacts.Include("Addresses").FirstOrDefault(x => x.Id == id);
            if (contact == null) return null;

            var contactDTO = new CreateEditContactDTO()
            {
                Id = contact.Id,
                Birthday = contact.Birthday,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                UserId = contact.UserId
            };

            var addresses = new List<AddressListDto>();

            if (contact.Addresses != null && contact.Addresses.Any())
            {
                foreach (var address in contact.Addresses)
                {
                    var addressDto = new AddressListDto(){
                        Id = address.Id
                        , ContactId = address.ContactId
                        , Email = address.Email
                        , PhonePrimary = address.PhonePrimary
                        , PhoneSecondary = address.PhoneSecondary
                        , StreetAddress1 = address.StreetAddress1
                        , StreetAddress2 = address.StreetAddress2
                        , City = address.City
                        , Zip = address.Zip
                        , StateId = address.StateId
                    };

                    if (addressDto.StateId.HasValue && addressDto.StateId.Value > 0)
                    {
                        addressDto.StateName = states.FirstOrDefault(x => x.Id == addressDto.StateId.Value)?.Name;
                    }

                    addresses.Add(addressDto);
                }
            }
            contactDTO.Addresses = addresses;
            return contactDTO;
        }

        private Contact GetContactFromContextById(int id)
        {
            return _context.Contacts.FirstOrDefault(x => x.Id == id);
        }

        //create/edit
        public int AddOrUpdateContact(CreateEditContactDTO c)
        {
            if (!c.Id.HasValue || c.Id <= 0)
            {
                return AddContact(c);
            }
            return EditContact(c);
        }

        private int AddContact(CreateEditContactDTO c)
        {
            //map the dto to the contact
            var contact = new Contact()
            {
                Id = 0,
                Addresses = null,
                UserId = c.UserId,
                Birthday = c.Birthday.HasValue ? Convert.ToDateTime(c.Birthday.Value) : new DateTime(1900, 1, 1),
                FirstName = c.FirstName,
                LastName = c.LastName
            };

            _context.Contacts.AddOrUpdate(contact);
            _context.SaveChanges();
            return contact.Id;
        }

        private int EditContact(CreateEditContactDTO c)
        {
            if (!c.Id.HasValue || c.Id.Value == 0)
                throw new ArgumentException("Cannot edit contact without a valid contact id");

            //get the actual contact to update
            var contact = GetContactFromContextById(c.Id.Value);

            if (contact == null) throw new Exception("could not find contact to edit by selected value.  Please try again");

            //map the contact that came in but only for the info that we want to allow (for example, can't change the userid)
            contact.Birthday = c.Birthday.HasValue ? Convert.ToDateTime(c.Birthday.Value) : contact.Birthday;
            contact.FirstName = c.FirstName;
            contact.LastName = c.LastName;

            _context.SaveChanges();
            
            return c.Id.Value;
        }

        //delete
        public bool DeleteContact(int id)
        {
            var contact = GetContactFromContextById(id);
            if (contact == null) return false;

            _context.Contacts.Remove(contact);
            return _context.SaveChanges() > 0;
        }
    }
}
