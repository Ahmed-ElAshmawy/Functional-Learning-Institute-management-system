using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using GraduationProject.Models;

namespace GraduationProject.Controllers
{
    public class ParentsController : ApiController
    {
        private ParentService _dbService { get; }

        public ParentsController()
        {
            _dbService = new ParentService();
        }

        // GET: api/Parents
        public IEnumerable GetUsers()
        {
            return _dbService.GetAll();
        }

        // GET: api/Parents/5
        [ResponseType(typeof(Parent))]
        public async Task<IHttpActionResult> GetParent(string id)
        {
            var parent = _dbService.GetById(id);
            if (parent == null)
                return NotFound();
            else
                return Ok(parent);
        }

        // PUT: api/Parents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutParent(BaseParentDto parent, string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _dbService.Update(parent, id);
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

        // POST: api/Parents
        [ResponseType(typeof(Parent))]
        public async Task<IHttpActionResult> PostParent(CreateParentDto parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await _dbService.Create(parent);
            if (res == 1)
                return Ok(new { Message = "Created", Model = parent });
            return BadRequest(ModelState);
        }

        // DELETE: api/Parents/5
        [ResponseType(typeof(Parent))]
        public async Task<IHttpActionResult> DeleteParent(string id)
        {
            var result = _dbService.Delete(id);
            if (result == 0)
                return NotFound();
            else if (result == -1)
                return BadRequest();
            return Ok(new { Message = "Deleted" });
        }

        [Route("api/Parents/GetParentChild")]
        public async Task<IHttpActionResult> GetParentChild(string id)
        {
            var children = _dbService.GetParentChild(id);
            if (children == null)
                return NotFound();
            else
                return Ok(children);
        }

        [Route("api/Parents/GetRetrieveParent")]
        [ResponseType(typeof(Parent))]
        public IHttpActionResult GetRetrieveParent()
        {
            var parent = _dbService.RetrieveList();
            return Ok(parent);
        }

        [Route("api/Parents/PutParentRetrieve")]
        [ResponseType(typeof(Parent))]
        public IHttpActionResult PutParentRetrieve(string id)
        {
            var result = _dbService.RetrieveParent(id);
            if (result == 0)
                return NotFound();
            else if (result == -1)
                return BadRequest();
            return Ok(new { Message = "Updated" });
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