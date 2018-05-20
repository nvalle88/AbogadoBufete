#region Using

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SmartAdmin.Web.Configuration;
using SmartAdmin.Web.Models;
using SmartAdmin.Web.Models.AccountViewModels;
using SmartAdmin.Web.Utils;

#endregion

namespace SmartAdmin.Web.Data
{
    /// <summary>
    /// Helper class that ensures that the data store used by the application contains the demo user.
    /// </summary>
    public class ApplicationDbSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private bool _seeded;

        public ApplicationDbSeeder(IConfiguration configuration,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // We take a dependency on the manager as we want to create a valid user
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        private void LoadRole()
        {
            //Add Role variables estaticas
            Role.Admin = _configuration.GetSection("Roles:0").Value;
            Role.Cliente = _configuration.GetSection("Roles:1").Value;
            Role.Abogado = _configuration.GetSection("Roles:2").Value;
        }

        private async Task CreateAdminRole()
        {

            var Email = _configuration.GetSection("Admin1:Email").Value;
            var user =await _userManager.FindByNameAsync(Email);
            if (!await _userManager.IsInRoleAsync(user,Role.Abogado))
            {
                await _userManager.AddToRoleAsync(user, Role.Abogado);
            }
            if (!await _userManager.IsInRoleAsync(user, Role.Admin))
            {
                await _userManager.AddToRoleAsync(user, Role.Admin);
            }
        }

        private async Task CreateAdminUsers()
        {
            try
            {
                var Identificacion = _configuration.GetSection("Admin1:Identificacion").Value;
                var Email = _configuration.GetSection("Admin1:Email").Value;
                var Nombre = _configuration.GetSection("Admin1:Nombre").Value;
                var Apellido = _configuration.GetSection("Admin1:Apellido").Value;
                var Password = _configuration.GetSection("Admin1:Password").Value;
                

                //var rolesArray = JsonConvert.DeserializeObject<RegisterViewModel>(admins.ToString());

                var user = await _userManager.FindByNameAsync(Email);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        Nombre=Nombre,
                        Apellido=Apellido,
                        Identificacion=Identificacion,
                        UserName =Email,
                        Email =Email,
                    };
                  await _userManager.CreateAsync(user, Password);
                }
            }
            catch (Exception ex)
            {
               var a= ex.Message;
                throw;
            }
           

            //user = _userManager.FindByName("consulta@simed.com");
            //if (user == null)
            //{
            //    user = new ApplicationUser
            //    {
            //        UserName = "consulta@simed.com",
            //        Email = "consulta@simed.com",
            //    };
            //    _userManager.Create(user, "Consulta123**");
            //}
        }

        private async Task CreateUserRoles()
        {

           var roles = _configuration.GetSection("Roles");

            var rolesArray = roles.AsEnumerable();
            
            foreach (var role in rolesArray)
            {
                if (role.Value!=null)
                {
                    if (!await _roleManager.RoleExistsAsync(role.Value))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role.Value));
                    }
                }
               
            }
          
        }
        /// <summary>
        /// Performs the data store seeding of the demo user if it does not exist yet.
        /// </summary>
        /// <returns>A <c>bool</c> indicating whether the seeding has occurred.</returns>
        public async Task EnsureSeed()
        {
            LoadRole();
            await CreateUserRoles();
            await CreateAdminUsers();
            await CreateAdminRole();
            if (!_seeded)
            {
                try
                {
                    // First we check if an existing user can be found for the configured demo credentials
                    var existingUser = await _userManager.FindByEmailAsync(SmartSettings.DemoUsername);

                    // If an existing user was found
                    if (existingUser != null)
                    {
                        // Notify the developer
                        Console.WriteLine("Database already seeded!");

                        // Then seeding has already taken place
                        _seeded = true;
                        return;
                    }

                    // Prepare the new user with the configured demo credentials
                    var user = new ApplicationUser
                    {
                        UserName = SmartSettings.DemoUsername,
                        Email = SmartSettings.DemoUsername
                    };

                    // Attempt to create the demo user in the data store using the configured demo password
                    var result = await _userManager.CreateAsync(user, SmartSettings.DemoPassword);

                    // Notify the developer whether the demo user was created successfully
                    Console.WriteLine(result.Succeeded ? "Database successfully seeded!" : "Database already seeded!");

                    // We either already have the demo user or it was just added, either way we're good
                    _seeded = true;
                    return;
                }
                catch (Exception ex)
                {
                    // Notify the developer that storing the demo user encountered an error
                    Console.Error.WriteLine("Error trying to seed the database");
                    Console.Error.WriteLine(ex);
                    return;
                }
            }

            // Notify the developer
            Console.WriteLine("Database already seeded!");
        }
    }
}
