/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace pip_air.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "Admin" };
            var role2 = new IdentityRole { Name = "Manager" };
            var role3 = new IdentityRole { Name = "User" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            // создаем пользователей Admin
            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
             string password = "admin123";
             var result = userManager.Create(admin, password);
            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
            }
                // создаем пользователей Manager
            var manager = new ApplicationUser { Email = "manager@gmail.com", UserName = "manager@gmail.com" };
            var password1 = "manager";
            var result1 = userManager.Create(manager, password1);
            // если создание пользователя прошло успешно
            if (result1.Succeeded)
             {
                 // добавляем для пользователя роль
                 userManager.AddToRole(manager.Id, role2.Name);
             }

            base.Seed(context);
        }
    }
}*/