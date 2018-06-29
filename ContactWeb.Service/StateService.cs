using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ContactWeb.Models;

namespace ContactWeb.Service
{
    public class StateService
    {
        private readonly ContactWebContext _context;
        public StateService(ContactWebContext context)
        {
            _context = context;
        }

        public SelectList GetStates()
        {
            var dbStates = _context.States.ToList();
            var states = new List<SelectListItem>();
            foreach (var state in dbStates)
            {
                var item = new SelectListItem()
                {
                    Value = state.Id.ToString(),
                    Text = state.Name
                };
                states.Add(item);
            }

            return new SelectList(states);
        }

        public SelectList GetStatesWithChooseOption()
        {
            var list = (List<SelectListItem>)GetStates().Items;
            list.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "-- Select --"
            });
            return new SelectList(list);
        }

        public SelectListItem GetStateById(int id)
        {
            var state = _context.States.FirstOrDefault(x => x.Id == id);
            if (state == null) return null;

            return new SelectListItem()
            {
                Value = state.Id.ToString(),
                Text = state.Name
            };
        }
    }
}
