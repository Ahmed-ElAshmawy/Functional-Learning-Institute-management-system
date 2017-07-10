using System.Collections.Generic;
using GraduationProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GraduationProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GraduationProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GraduationProject.Models.ApplicationDbContext context)
        {

            var t = new DateTime(2000, 1, 1, 8, 0, 0);

            List<Slot> li = new List<Slot>();
            for (int i = 0; i < 48; i++)
            {
                li.Add(new Slot
                {
                    Id = "AAAABBBB-1111-1111-AAAA-11112222" + (i + 1000).ToString(),
                    From = t,
                    To = t.AddMinutes(15)
                });
                t = t.AddMinutes(15);
            }
            context.Slots.AddOrUpdate(li.ToArray());

            List<ServiceType> ServiceTypeList = new List<ServiceType>
            {
                new ServiceType{Id = "AAAABBBB-1111-1111-0000-11110000",Date = DateTime.Now,ServiceRate = 40,ServiceTypeName = "BI"},
                new ServiceType{Id = "AAAABBBB-1111-1111-0000-11110001",Date = DateTime.Now,ServiceRate = 100,ServiceTypeName = "BC"},
                new ServiceType{Id = "AAAABBBB-1111-1111-0000-11110002",Date = DateTime.Now,ServiceRate = 130,ServiceTypeName = "BPI"},
                new ServiceType{Id = "AAAABBBB-1111-1111-0000-11110003",Date = DateTime.Now,ServiceRate = 130,ServiceTypeName = "OT"},
                new ServiceType{Id = "AAAABBBB-1111-1111-0000-11110004",Date = DateTime.Now,ServiceRate = 40,ServiceTypeName = "Group"}
            };
            context.ServiceType.AddOrUpdate(ServiceTypeList.ToArray());
            
            if (!context.Roles.Any(r => r.Name == "Parent"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Parent" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Therapist"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Therapist" };
                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "Therapist1"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "Therapist1", CreatedBy = "0000" };
                manager.Create(user, "Abc_123");
                manager.AddToRole(user.Id, "Therapist");
            }
            if (!context.Users.Any(u => u.UserName == "Therapist2"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "Therapist2", CreatedBy = "0000" };
                manager.Create(user, "Abc_123");
                manager.AddToRole(user.Id, "Therapist");
            }

            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "Admin", CreatedBy = "0000" };
                manager.Create(user, "Abc_123");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "Parent1"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "Parent1", CreatedBy = "0000" };
                manager.Create(user, "Abc_123");
                manager.AddToRole(user.Id, "Parent");
            }


            if (!context.Users.Any(u => u.UserName == "Parent2"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "Parent2", CreatedBy = "0000" };
                manager.Create(user, "Abc_123");
                manager.AddToRole(user.Id, "Parent");
            }

            if (context.Users.Any(u => u.UserName == "Parent1"))
            {
                var Id = context.Users.First(u => u.UserName == "Parent1").Id;
                if (!context.Parents.Any(u => u.Id == Id))
                {
                    var q =
                    @"INSERT INTO [dbo].[Parent]
                    ([Id],[MotherName],[FatherName],[PostalCode],[City],[StreetName],[StreetNo],[Province],[BirthDate])
                    VALUES('{0}','mariam','hossam','123456','alex','st1','10','alex','10/10/1980');";
                    context.Database.ExecuteSqlCommand(string.Format(q, Id));
                }
            }

            if (context.Users.Any(u => u.UserName == "Parent2"))
            {
                var Id = context.Users.First(u => u.UserName == "Parent2").Id;
                if (!context.Parents.Any(u => u.Id == Id))
                {
                    var q =
                    @"INSERT INTO [dbo].[Parent]
                    ([Id],[MotherName],[FatherName],[PostalCode],[City],[StreetName],[StreetNo],[Province],[BirthDate])
                    VALUES('{0}','so3ad','ahmed','123456','alex','st1','10','alex','10/10/1980');";
                    context.Database.ExecuteSqlCommand(string.Format(q, Id));
                }
            }
            if (context.Users.Any(u => u.UserName == "Therapist1"))
            {
                var Id = context.Users.First(u => u.UserName == "Therapist1").Id;
                if (!context.Therapists.Any(u => u.Id == Id))
                {
                    var q =
                    @"INSERT INTO [dbo].[Therapist]
                     ([Id],[StaffName],[LastName],[FullLegalName],[SIN],[Phone],[CRCExpiry],[StartDate],[TransitNo],[IsEmployee],[InstitueNo],[AccountNo],[EmergancyContact],[EmergancyPhone],[JopTitle],[PostalCode],[Address],[BirthDate])
                     VALUES('{0}','ibrahim','mohamed','ibrahim mohamed','123456789','12345679','10/10/2019','1/6/2017','12346789',1,'12346','123456789','contactemg','123977','tayar','123465','taddress','1/1/1988');";
                    context.Database.ExecuteSqlCommand(string.Format(q, Id));
                }
            }

            if (context.Users.Any(u => u.UserName == "Therapist2"))
            {
                var Id = context.Users.First(u => u.UserName == "Therapist2").Id;
                if (!context.Therapists.Any(u => u.Id == Id))
                {
                    var q =
                    @"INSERT INTO [dbo].[Therapist]
                     ([Id],[StaffName],[LastName],[FullLegalName],[SIN],[Phone],[CRCExpiry],[StartDate],[TransitNo],[IsEmployee],[InstitueNo],[AccountNo],[EmergancyContact],[EmergancyPhone],[JopTitle],[PostalCode],[Address],[BirthDate])
                     VALUES('{0}','sawsan','ashraf','sawsan ashraf','123456789','12345679','10/10/2019','1/6/2017','12346789',1,'12346','123456789','contactemg','123977','tayar','123465','taddress','1/1/1988');";
                    context.Database.ExecuteSqlCommand(string.Format(q, Id));
                }
            }
        }
    }
}