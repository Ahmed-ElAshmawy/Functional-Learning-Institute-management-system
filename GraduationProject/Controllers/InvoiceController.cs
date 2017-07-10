using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GraduationProject.Models;
using System.Collections;
using GraduationProject.BL;
using System.Threading.Tasks;

namespace GraduationProject.Controllers
{
    public class InvoiceController : ApiController
    {
        InvoiceService InvoService = new InvoiceService();
        public IEnumerable GetAllChildren(DateTime specificDate, string fundType)
        {
            return InvoService.GetAllChildren(specificDate,fundType);
        }
        public IEnumerable GetAllChild(string childId ,DateTime specificDate)
        {
            return InvoService.ChildInvoice(childId, specificDate);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostImage(GList imageFile, string childId, string email)
        {
            if (imageFile.Value == null || childId == null)
            {
                return BadRequest();
            }
            var res = await InvoService.SendInvoice(imageFile, childId, email);
            if (res == 1)
                return Ok();
            return BadRequest();
        }
    }
}
