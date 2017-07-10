using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GraduationProject.BL;
using GraduationProject.Models;

namespace GraduationProject.Controllers
{
    public class ContactsController : ApiController
    {
        private ContactService _dbService;

        public ContactsController()
        {
            _dbService = new ContactService();
        }

        // GET: api/Contacts
        [Route("api/Contacts/GetAll")]
        public IEnumerable GetContacts()
        {
            return _dbService.GetAll();
        }

        // GET: api/Contacts/5
        [ResponseType(typeof(Contact))]
        [Route("api/Contacts/GetById")]
        public IHttpActionResult GetContact(string id)
        {
            var contact = _dbService.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }
        // GET: api/Contacts/5
        [ResponseType(typeof(Contact))]
        [Route("api/Contacts/GetByParent")]
        public IHttpActionResult GetByParent(string id)
        {
            var contact = _dbService.GetByParentId(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContact(string id, ContactDto contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }
            var result = _dbService.Update(contact, id);
            if (result == -1)
                return BadRequest();
            return Ok(new { Message = "Updated" });
        }

        // POST: api/Contacts
        [ResponseType(typeof(Contact))]
        public IHttpActionResult PostContact(ContactDto contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _dbService.Create(contact);
            if (result == 1)
                return Ok(new { Message = "Created", Model = contact });
            else
                return BadRequest();
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(string id)
        {
            var result = _dbService.Delete(id);
            if (result == -1)
            {
                return NotFound();
            }
            return Ok(new { Message = "Deleted" });
        }

        [Route("api/Contacts/PutContactRetrieve")]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult PutContactRetrieve(string id)
        {
            var result = _dbService.RetrieveContact(id);
            if (result == -1)
            {
                return NotFound();
            }
            return Ok(new { Message = "Updated" });
        }

        [Route("api/Contacts/GetRetrieveContact")]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult GetRetrieveContact()
        {
            var contact = _dbService.RetrieveList();
            return Ok(contact);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbService.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}