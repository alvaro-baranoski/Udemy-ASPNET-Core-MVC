using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }


        public async void Initialize()
        {
            // Migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch
            {

            }
            // Create roles if they are not created
            if (! await _roleManager.RoleExistsAsync(SD.Role_Admin)) 
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee));
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Indi));
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Comp));

                // If roles are not created, then we will create admin user
                await _userManager.CreateAsync(new ApplicationUser 
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Álvaro José",
                    PhoneNumber = "41 9 12345678",
                    StreetAddress = "Rua Falsa 123",
                    State = "PR",   
                    PostalCode = "80000000",
                    City = "Curitiba"
                }, "Admin123@");

                var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");

                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, SD.Role_Admin);
                }
                else
                {
                    throw new Exception("Failed to create initial user!");
                }
            }
            return;
        }
    }
}