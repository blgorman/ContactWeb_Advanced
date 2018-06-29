using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContactWeb.Models
{
    public class ContactWebContext : DbContext
    {
        public ContactWebContext() : base("name=ContactWebContext")
        {
        }

        public System.Data.Entity.DbSet<ContactWeb.Models.Contact> Contacts { get; set; }
        public System.Data.Entity.DbSet<ContactWeb.Models.State> States { get; set; }
        public System.Data.Entity.DbSet<ContactWeb.Models.Address> Addresses { get; set; }
    }
}
