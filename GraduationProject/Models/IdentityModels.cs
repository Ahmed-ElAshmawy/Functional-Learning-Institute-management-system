using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Web;

namespace GraduationProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.CreatedDate = DateTime.Now;
            //this.CreatedBy= HttpContext.Current.User.Identity.GetUserId();
        }
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string DeletedBy { get; set; }

        public virtual ICollection<UserMessage> UserMessage { get; set; }
        public virtual ICollection<Connection> Connections { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; } = false;

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //    this.Configuration.LazyLoadingEnabled = false;
            //    this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Child> Children { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<ParentEmail> ParentEmails { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<Slot> Slots { get; set; }

        public DbSet<Therapist> Therapists { get; set; }

        public DbSet<TherapistTask> Tasks { get; set; }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<ChildrenAvailability> ChildrenAvailability { get; set; }
        public DbSet<TherapistAvailability> TherapistAvailability { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<UserMessage> UserMessage { get; set; }

        public DbSet<ServiceType> ServiceType { get; set; }
        public DbSet<ClientFund> ClientFund { get; set; }
        public virtual ObjectResult<Slot> Getsolts(string id)
        {

            var prams = new ObjectParameter("slotId", id);
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Slot>("getslot", prams);
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}