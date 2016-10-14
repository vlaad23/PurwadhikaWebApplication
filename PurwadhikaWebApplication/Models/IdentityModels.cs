using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PurwadhikaWebApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int Batch { get; set; }
        public bool AccountStatus { get; set; } = true;
        public string AccountTranscript{ get; set; }
        public string AccountPicture { get; set; }
        public string InstanceName { get; set; }
        public string Industry { get; set; }
        public string About { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //tambahan mulai dari : base
        public ApplicationDbContext()
           : base("DefaultConnection", throwIfV1Schema: false)
                       
        {

        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        static ApplicationDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        DbSet<JobMaster> JobMaster;
        DbSet<AnnouncementMaster> AnnouncementMaster;
        DbSet<MessageMaster> MessageMaster;


        public DbSet<JobMaster> JobMasters { get; set; }
        public DbSet<MessageMaster> MessageMasters { get; set; }

        public DbSet<AnnouncementMaster> AnnouncementMasters { get; set; }

        public DbSet<JobViewModel> JobViewModels { get; set; }

        public DbSet<MessageViewModel> MessageViewModels { get; set; }



       // public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        //public System.Data.Entity.DbSet<PurwadhikaWebApplication.Models.ApplicationUser> ApplicationUsers { get; set; }
    }

    public class DbInitializer : CreateDatabaseIfNotExists <ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            RoleInitialize(context);
        }

        private void RoleInitialize(ApplicationDbContext context)
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            RoleManager.Create(new IdentityRole("Admin"));
            RoleManager.Create(new IdentityRole("Student"));
            RoleManager.Create(new IdentityRole("Marketing"));
            RoleManager.Create(new IdentityRole("Employer"));
            RoleManager.Create(new IdentityRole("Faculty"));
            RoleManager.Create(new IdentityRole("Parent"));
        }
    }
}