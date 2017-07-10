using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GraduationProject.BL;
using System.Collections;
using System.Threading.Tasks;
using GraduationProject.Models;

namespace GraduationProject.Controllers
{
    public class ReportTherapistController : ApiController
    {
        TherapistReportService _Report = new TherapistReportService();
        [Route("api/ReportTherapist")]
        public IEnumerable GetAllTherapist(DateTime specificDate)
        {
            return _Report.AllTherapistInMonth(specificDate);
        }
        [Route("api/ReportTherapist/BiMonthReport")]
        public IEnumerable GetBiMonthReport(string therapistId, DateTime specificDate,string reportType)
        {
            return _Report.GetBiMonthReporrt(therapistId, specificDate, reportType);
        }
        [Route("api/ReportTherapist/TaskReport")]
        public IEnumerable GetTaskReport(string therapistId, DateTime specificDate)
        {
            return _Report.GetTherapistTask(therapistId, specificDate);
        }
        [Route("api/ReportTherapist/FinalReport")]
        public IEnumerable GetTherapistReport(string therapistId, DateTime spescificDate)
        {
            return _Report.TherapistReport(therapistId, spescificDate);
        }
        [Route("api/ReportTherapist/SendMail")]
        [HttpPost]
        public async Task<IHttpActionResult> PostImage(GList imageFile, string therapistId)
        {
            if (imageFile.Value == null || therapistId == null)
            {
                return BadRequest();
            }
            var res = await _Report.SendReport(imageFile, therapistId);
            if (res == 1)
                return Ok();
            return BadRequest();
        }
    }
}
