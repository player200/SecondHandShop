namespace SecondHandShop.Web.Infrastructures.Extentions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Web.Data;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<SecondHandShopDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var adminName = WebConstants.AdministratorRole;

                        var roles = new[]
                        {
                            adminName,
                            WebConstants.ModeratorRole,
                            WebConstants.UserRole
                        };

                        foreach (var role in roles)
                        {

                            var roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        var adminEmail = "admin@admin.com";
                        var adminUser = await userManager.FindByEmailAsync(adminEmail);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Email = adminEmail,
                                UserName = adminName,
                                Name = adminName,
                                PhoneNumber = "+0000000000"
                            };

                            await userManager.CreateAsync(adminUser, "admin12");
                            await userManager.AddToRoleAsync(adminUser, adminName);
                        }
                    })
                    .Wait();
            }

            return app;
        }
    }
}