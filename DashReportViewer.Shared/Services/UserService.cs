using DashReportViewer.Context;
using DashReportViewer.Models;
using DashReportViewer.Shared.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBackpack.Time;

namespace DashReportViewer.Shared.Services
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetUsers();
        Task<NewUserResult> CreateUser(NewUser user);
        Task<ApplicationUser> GetUser(long Id);
        Task UpdateUser(long id, string firstName, string lastName, string email, int role);
    }

    public class UserService : IUserService
    {
        readonly DashReportViewerContext context;
        readonly UserManager<ApplicationUser> userManager;
        public UserService(DashReportViewerContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await context.Users.Select(u => new ApplicationUser()
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                locale = u.locale
            }).ToListAsync();
        }

        public async Task UpdateUser(long id, string firstName, string lastName, string email, int role)
        {
            var user = await context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            user.FirstName = firstName;
            user.LastName = lastName;

            user.Email = email;
            user.NormalizedEmail = email.ToUpper();
            user.UserName = email;
            user.NormalizedUserName = email.ToUpper();

            await context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetUser(long Id)
        {
            return await context.Users.Where(u => u.Id == Id).Select(u => new ApplicationUser()
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                locale = u.locale
            }).FirstOrDefaultAsync();
        }

        public async Task<NewUserResult> CreateUser(NewUser user)
        {
            var locale = TimeZoneExtention.ConvertIanaIdToWindowsTime(user.Timezone);

            var newUser = new ApplicationUser { 
                UserName = user.Email, 
                Email = user.Email, 
                FirstName = user.FirstName, 
                LastName = user.LastName, 
                locale = locale,
                Created = SystemTime.Now,
                LastLoggedIn = SystemTime.Now,
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
            {
                return new NewUserResult()
                {
                    Success = true
                };
            }
            else
            {
                return new NewUserResult()
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }
        }
    }
}
