using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GraduationProject.Controllers
{
    public class NotificationsController : ApiController
    {
        NotificationsService _dbService = new NotificationsService();
        public IHttpActionResult GetAll()
        {
            return Ok(_dbService.GetAll());
        }

        public IHttpActionResult GetById(string id)
        {
            return Ok(_dbService.GetById(id));
        }

    }
}
