using System;
using System.ComponentModel.DataAnnotations;

namespace ContactWeb.ViewModels
{
    public class ContactListDTO
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CityState { get; set; }
    }
}
