using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using GraduationProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NLog;

namespace GraduationProject
{
    public class CommonService
    {
        public static string emailName { get; set; }
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        public static async Task<IdentityResult> CreateUser(ApplicationUser model, string password)
        {
            UserManager<ApplicationUser> userManager = GetUserManger();
            IdentityResult result = await userManager.CreateAsync(model, password);
            userManager.Dispose();
            return result;
        }

        static UserManager<ApplicationUser> GetUserManger()
        {
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(store);
            return userManager;
        }

        public static async Task AddToRole(string role, string id)
        {
            UserManager<ApplicationUser> userManager = GetUserManger();
            await userManager.AddToRoleAsync(id, role);
            userManager.Dispose();
        }

        public static async void RemoveUserRoles(string id)
        {
            UserManager<ApplicationUser> userManager = GetUserManger();
            var userRoles = await userManager.GetRolesAsync(id);
            //Delete all user roles
            foreach (var role in userRoles)
            {
                await userManager.RemoveFromRoleAsync(id, role);
            }
            userManager.Dispose();
        }
        public static async Task<IdentityResult> RemoveUser(string id)
        {
            UserManager<ApplicationUser> userManager = GetUserManger();
            var user = await userManager.FindByIdAsync(id);
            IdentityResult result = await userManager.DeleteAsync(user);
            userManager.Dispose();            
            return result;
        }

        public static async Task<int> SendMail(string mailTo, string subject, string body)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(mailTo));  // Email For 
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
                return 1;
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);

                return -1;
            }

        }

        public static async Task<int> SendMail(string mailTo, string subject, string body, byte[] bytes)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(mailTo));  // Email For 
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.Attachments.Add(new Attachment(new MemoryStream(bytes), "Invoice" + emailName + ".pdf"));
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
                return 1;
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);
                return -1;
            }
        }
    }
}