using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GraduationProject.Models;
using System.Collections;
using System.Web.Http.Description;

namespace GraduationProject.Controllers
{
    public class ParentEmailController : ApiController
    {
        ParentEmailService _dbService = new ParentEmailService();
        // GET: api/ParentEmail
        [Route("api/ParentEmail/GetAll")]
        public IHttpActionResult Get()
        {
            var EmailList = _dbService.GetAll();
            if (EmailList != null)
            {
                return Ok(EmailList);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
        [ResponseType(typeof(ParentEmail))]
        // GET: api/ParentEmail/5
        [Route("api/ParentEmail/GetById")]
        public IHttpActionResult Get(string id)
        {
            var email = _dbService.GetById(id);
            if (email != null)
            {
                return Ok(email);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get All parentEmails for specific parent
        /// </summary>
        /// <param name="parentid">parentid</param>
        /// <returns></returns>
        [ResponseType(typeof(ParentEmail))]
        [Route("api/ParentEmail/GetByparent")]
        public IHttpActionResult Getbyparent(string parentid)
        {
            var email = _dbService.GetByParent(parentid);
            if (email != null)
            {
                return Ok(email);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        // PUT: api/ParentEmail
        public IHttpActionResult Put(ParentEmailDto model, string id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _dbService.Update(model, id);
            if (result == 0)
            {
                return NotFound();
            }
            else if (result == -1)
            {
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Updated" });
        }

        // POST: api/ParentEmail/5
        public IHttpActionResult Post(ParentEmailDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _dbService.Create(model);
            if (result == 1)
                return Ok(new { Message = "Created", Model = model });
            return BadRequest(ModelState);
        }

        // DELETE: api/ParentEmail/5
        public IHttpActionResult Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                var result = _dbService.Delete(id);
                if (result == 1)
                {
                    return Ok(new { Message = "Deleted" });
                }
                else
                {
                    return NotFound();
                }
            }
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
