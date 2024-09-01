using booking_app_template.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using booking_app_template.Data.Static;

namespace booking_app_template.Data
{
    public class AppDbInitialiser
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            context.Database.EnsureCreated();

            
            //Products
            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Dome Club Party",
                            Description = "Size: 10ft W x 10ft L x 10ft. Want to bounce the night or day away with a dance party? Our inflatable disco dome bounce house is the perfect match for bouncing and dancing. ",
                            Price = 75.50,
                            ImageURL = "https://th.bing.com/th/id/OIP.eTAgA1OZGjCxvtXrgtgnFgHaJ4?w=153&h=204&c=7&r=0&o=5&pid=1.7",
                            
                            
                        },
                        new Product()
                        {
                            Name = "Disney themed",
                            Description = "Disney bouncy castle is another great choice from our already great range of themed inflatables and comes with pictures of your favourite characters displayed all over",
                            Price = 100.90,
                            ImageURL = "https://th.bing.com/th/id/OIP.r29QWIcx8d0M4WdRfYqb1QHaEK?w=313&h=180&c=7&r=0&o=5&pid=1.7",
                            
                            
                        },
                        new Product()
                        {
                            Name = "Froozen",
                            Description = "Inflatable Bounce Bouncy Castle Inflatable Princess Castle Bouncy Jumping Bouncer Frozen Bouncy Castle Bounce House",
                            Price = 90.50,
                            ImageURL = "https://th.bing.com/th/id/OIP.6UJHhdc3IVJLolLUdjMJAQHaEK?w=281&h=180&c=7&r=0&o=5&pid=1.7",
                            
                            
                        },
                        new Product()
                        {
                            Name = "Avengers",
                            Description = "Assemble with Earth’s Mightiest Heroes with our 6in1 Marvel Avengers Combo Waterslide. This Marvel Avengers Bounce and Slide Combo ride features six different inflatable activities",
                            Price = 110.95,
                            ImageURL = "https://th.bing.com/th/id/R.007fcabeea74a4cdefceb9230032f939?rik=njnA0Q5ykBPyOQ&riu=http%3a%2f%2fwww.perthbouncycastlehire.com.au%2fwp-content%2fuploads%2f2015%2f10%2f1.jpg&ehk=HDPkIPc1mw2asWU5Rg17BQH%2fDmJA75MgssOhcnIEpfs%3d&risl=&pid=ImgRaw&r=0",
                            
                            
                        },
                        new Product()
                        {
                            Name = "SpiderMan",
                            Description = "Show Stopper Spiderman 3D Castle will steal the limelight at any party or event.\r\n\r\nFor all the little Spidey's out there - This one is for YOU! Spiderman is one of the Worlds most Timeless Super Heroes.",
                            Price = 105.75,
                            ImageURL = "https://th.bing.com/th/id/OIP.cG6B3qC5zdrTkVOO9gipuwHaHa?w=183&h=183&c=7&r=0&o=5&pid=1.7",
                            
                            
                        },
                        new Product()
                        {
                            Name = "Mickey Mouse",
                            Description = "Make way for adventure with the Official MICKEY AND FRIENDS bouncy Castle. It has plenty of space to bounce, hop, skip, and jump to their heart’s content.",
                            Price = 80.60,
                            ImageURL = "https://th.bing.com/th/id/OIP.hVTi4vrSLeKiZNSZGcBQ8AHaF5?w=266&h=212&c=7&r=0&o=5&pid=1.7",
                            
                            
                        }
                    });
                context.SaveChanges();
            }
            

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles, check if roles exist.
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@castle.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = "5 Llys",
                        City = "Neath",
                        State = "Glamorgan",
                        PostalCode = "SA1",
                        ContactNumber = "07850521484"
                    };
                    await userManager.CreateAsync(newAdminUser, "Castle@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@castle.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = "5 Llys",
                        City = "Neath",
                        State = "Glamorgan",
                        PostalCode = "SA1",
                        ContactNumber = "07850521484"
                    };
                    await userManager.CreateAsync(newAppUser, "Castle@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }

    }
}
