
namespace ContactWeb.Migrations.ContactWebDb
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ContactWebConfiguration : DbMigrationsConfiguration<ContactWeb.Models.ContactWebContext>
    {
        public ContactWebConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ContactWeb.Models.ContactWebContext";
        }

        private Models.State GetNextState(string name, string abbrev, ContactWeb.Models.ContactWebContext context)
        {
            var state = context.States?.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower())
                                                            && x.Abbreviation.ToLower().Equals(abbrev.ToLower()));
            return state ?? new Models.State { Name = name, Abbreviation = abbrev };
        }

        protected override void Seed(ContactWeb.Models.ContactWebContext context)
        {
            context.States.AddOrUpdate(
                p => p.Id,
                GetNextState("Alabama", "AL", context),
                GetNextState("Alaska", "AK", context),
                GetNextState("Arizona", "AZ", context),
                GetNextState("Arkansas", "AR", context),
                GetNextState("California", "CA", context),
                GetNextState("Colorado", "CO", context),
                GetNextState("Connecticut", "CT", context),
                GetNextState("Delaware", "DE", context),
                GetNextState("District of Columbia", "DC", context),
                GetNextState("Florida", "FL", context),
                GetNextState("Georgia", "GA", context),
                GetNextState("Hawaii", "HI", context),
                GetNextState("Idaho", "ID", context),
                GetNextState("Illinois", "IL", context),
                GetNextState("Indiana", "IN", context),
                GetNextState("Iowa", "IA", context),
                GetNextState("Kansas", "KS", context),
                GetNextState("Kentucky", "KY", context),
                GetNextState("Louisiana", "LA", context),
                GetNextState("Maine", "ME", context),
                GetNextState("Maryland", "MD", context),
                GetNextState("Massachusetts", "MS", context),
                GetNextState("Michigan", "MI", context),
                GetNextState("Minnesota", "MN", context),
                GetNextState("Mississippi", "MS", context),
                GetNextState("Missouri", "MO", context),
                GetNextState("Montana", "MT", context),
                GetNextState("Nebraska", "NE", context),
                GetNextState("Nevada", "NV", context),
                GetNextState("New Hampshire", "NH", context),
                GetNextState("New Jersey", "NJ", context),
                GetNextState("New Mexico", "NM", context),
                GetNextState("New York", "NY", context),
                GetNextState("North Carolina", "NC", context),
                GetNextState("North Dakota", "ND", context),
                GetNextState("Ohio", "OH", context),
                GetNextState("Oklahoma", "OK", context),
                GetNextState("Oregon", "OR", context),
                GetNextState("Pennsylvania", "PA", context),
                GetNextState("Rhode Island", "RI", context),
                GetNextState("South Carolina", "SC", context),
                GetNextState("South Dakota", "SD", context),
                GetNextState("Tennessee", "TN", context),
                GetNextState("Texas", "TX", context),
                GetNextState("Utah", "UT", context),
                GetNextState("Vermont", "VT", context),
                GetNextState("Virginia", "VA", context),
                GetNextState("Washington", "WA", context),
                GetNextState("West Virginia", "WV", context),
                GetNextState("Wisconsin", "WI", context),
                GetNextState("Wyoming", "WY", context));
        }
    }
}
