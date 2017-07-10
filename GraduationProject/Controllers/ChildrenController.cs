using System;
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
using GraduationProject.Models;
using AutoMapper;
using System.Collections;

namespace GraduationProject.Controllers
{
    public class ChildrenController : ApiController
    {
        ChildService _dbService = new ChildService();
        // GET: api/Children
        public IEnumerable GetChildren()
        {
            return _dbService.GetAll();
        }

        // get children for assigning in asession
        // GET: api/Children
        [ResponseType(typeof(IEnumerable))]
        [Route("api/Children/GetAvailableChildren")]
        public IEnumerable GetChildrenForSpecificSession(string id)
        {
            return _dbService.GetAvailableChildren(id);
        }


        // GET: api/Children/5
        [ResponseType(typeof(Child))]
        public IHttpActionResult GetChild(string id)
        {

            ChildDto child = _dbService.GetById(id);
            if (child == null)
            {
                return NotFound();
            }

            return Ok(child);
        }

        // PUT: api/Children/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChild(string id, ChildDto child)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int UpdatedChild = _dbService.Update(child, id);
            if (UpdatedChild == 1)
                return Ok(new { Message = "Updated" });
            return NotFound();
        }

        // POST: api/Children
        [ResponseType(typeof(Child))]
        public IHttpActionResult PostChild(ChildDto child)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            int NewChild = _dbService.create(child);
            if (NewChild == 0)
                return Conflict();
            return Ok(new { Message = "Created",Model=child });
        }

        // DELETE: api/Children/5
        [ResponseType(typeof(Child))]
        public IHttpActionResult DeleteChild(string id)
        {
            int DeletedChild = _dbService.Delete(id);
            //return (DeletedChild == 0? NotFound() : StatusCode(HttpStatusCode.NoContent));
            if (DeletedChild == 0)
                return NotFound();
            return Ok(new { Message = "Deleted" });
        }
        //test

        [Route("api/Children/PutChildRetrieve")]
        [ResponseType(typeof(Child))]
        public IHttpActionResult ChildRetrieve(string id)
        {
            int DeletedChild = _dbService.RetrieveChild(id);
            if (DeletedChild == 0)
                return NotFound();
            return Ok(new { Message = "Updated" });
        }


        [Route("api/Children/GetRetrieveChildren")]
        [ResponseType(typeof(Child))]
        public IHttpActionResult GetRetrieveChildren()
        {
            List<ChildDto> child = _dbService.RetrieveList();        
            return Ok(child);
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