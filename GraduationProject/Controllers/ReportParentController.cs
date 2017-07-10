using GraduationProject.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using System.Threading.Tasks;
using GraduationProject.Models;

namespace GraduationProject.Controllers
{
    public class ReportParentController : ApiController
    {
        ParentReport _PReport = new ParentReport();
        [Route("api/ParentReport/GetPatentReport")]
        public IEnumerable GetPatentReport(string childId, DateTime specificDate)
        {
            return _PReport.ReportParent(childId, specificDate);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostImage(GList imageFile, string childId)
        {
            if (imageFile.Value == null || childId == null)
            {
                return BadRequest();
            }
            var res = await _PReport.SendInvoiceToParent(imageFile, childId);
            if (res == 1)
                return Ok();
            return BadRequest();
        }

    }
}
