using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuktiSolutions.SmartERP.Models.Database
{
    public static class UserManager
    {
        /// <summary>
        /// Saves the users.
        /// </summary>
        /// <param name="userViewModel">Contains all properties of UserViewModel to be saved</param>
        public static void SaveUser(UserViewModel userViewModel)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Id.Equals(userViewModel.Id, StringComparison.CurrentCultureIgnoreCase));

                PasswordHasher hasher = new PasswordHasher();

                if (user == null)
                {
                    //Create user.
                    user = context.Users.Add( new ApplicationUser
                    {
                        UserName = userViewModel.UserName,
                        Email = userViewModel.Email,
                        PhoneNumber = userViewModel.Number,
                        PhoneNumberConfirmed = true,
                        PasswordHash = hasher.HashPassword(userViewModel.Password),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        LockoutEnabled = true
                    });
                }

                if (userViewModel.SetPass == true && user != null)
                {
                    user.PasswordHash = hasher.HashPassword(userViewModel.Password);
                    user.UserName = userViewModel.UserName;
                    user.Email = userViewModel.Email;
                    user.PhoneNumber = userViewModel.Number;
                }

                if (userViewModel.SetPass == false && user != null)
                {
                    user.UserName = userViewModel.UserName;
                    user.Email = userViewModel.Email;
                    user.PhoneNumber = userViewModel.Number;
                }
                context.SaveChanges();
            }
        }
    }
}