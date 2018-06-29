using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactWeb.Models;
using ContactWeb.Service;
using ContactWeb.ViewModels;
using Microsoft.AspNet.Identity;

namespace ContactWeb.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactWebContext db;
        private readonly ContactService _contactService;
        private readonly StateService _statesService;
        private readonly AddressService _addressService;

        public ContactsController()
        {
            db = new ContactWebContext();
            _contactService = new ContactService(db);
            _statesService = new StateService(db);
            _addressService = new AddressService(db);
        }

        // GET: Contacts
        [Authorize]
        public ActionResult Index()
        {
            var userId = GetCurrentUserId();
            return View(_contactService.GetAllContactsByUserId(userId));
        }

        // GET: Contacts/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contact = _contactService.GetContactById(id.Value);
            if (contact == null || !EnsureIsUserContact(contact))
            {
                return HttpNotFound();
            }

            contact.UserId = GetCurrentUserId();
            return View(contact);
        }

        // GET: Contacts/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.UserId = GetCurrentUserId();
            return View(new CreateEditContactDTO());
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(CreateEditContactDTO newContact)
        {
            if (ModelState.IsValid)
            {
                _contactService.AddOrUpdateContact(newContact);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = GetCurrentUserId();
            return View(newContact);
        }

        // GET: Contacts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contact = _contactService.GetContactById(id.Value);
            if (contact == null || !EnsureIsUserContact(contact))
            {
                return HttpNotFound();
            }

            var states = _statesService.GetStatesWithChooseOption();
            
            ViewBag.UserId = GetCurrentUserId();
            ViewBag.States = states;

            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(CreateEditContactDTO existingContact)
        {
            if (ModelState.IsValid)
            {
                _contactService.AddOrUpdateContact(existingContact);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = GetCurrentUserId();
            return View(existingContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateOrUpdateContactAddress(CreateEditAddressContainerDTO address)
        {
            var id = _addressService.AddOrUpdateAddress(address.NewEditAddress);
            //could do something with the ID if needed...
            return RedirectToAction("Edit", new {id = address.NewEditAddress.ContactId});
        }


        [Authorize]
        public ActionResult DeleteContactAddress(DeleteContactAddressDTO deleteContactAddress)
        {
            _addressService.DeleteAddress(deleteContactAddress.AddressId);
            return RedirectToAction("Edit", new { id = deleteContactAddress.ContactId });
        }

        // GET: Contacts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contact = _contactService.GetContactById(id.Value);
            if (contact == null || !EnsureIsUserContact(contact))
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            _contactService.DeleteContact(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public Guid GetCurrentUserId()
        {
            return new Guid(User.Identity.GetUserId());
        }

        private bool EnsureIsUserContact(CreateEditContactDTO contact)
        {
            return contact.UserId == GetCurrentUserId();
        }
    }
}
