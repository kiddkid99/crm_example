using Microsoft.AspNet.Identity.EntityFramework;

namespace CRM.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CRMDatabase")
        {      
           
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("Users", "dbo");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "dbo");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "dbo");
        }
    }
}