using ExamApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Web.Seed
{
    public static class DatabaseSeedUtilities
    {
        public static void SeedRoles(this WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                using (var examAppDbContext = serviceScope.ServiceProvider.GetRequiredService<ExamAppDbContext>())
                {
                    examAppDbContext.Database.Migrate();

                    if (examAppDbContext.Roles.ToList().Count == 0)
                    {
                        IdentityRole adminRole = new IdentityRole();
                        adminRole.Name = "Admin";
                        adminRole.NormalizedName = adminRole.Name.ToUpper();

                        IdentityRole userRole = new IdentityRole();
                        userRole.Name = "User";
                        userRole.NormalizedName = userRole.Name.ToUpper();

                        examAppDbContext.Add(adminRole);
                        examAppDbContext.Add(userRole);

                        examAppDbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
