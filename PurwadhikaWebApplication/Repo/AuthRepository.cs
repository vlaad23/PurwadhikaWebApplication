using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PurwadhikaWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace PurwadhikaWebApplication.Repo
{
    public class AuthRepository : IDisposable
    {
        private ApplicationDbContext _ctx;
        private UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _ctx = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }
        //Register ini gak kepake, cuma buat test sama contoh aja
        public async Task<IdentityResult> RegisterUser(RegisterViewModel userModel)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.Email,
                Firstname = userModel.Firstname,
                Lastname = userModel.Lastname,
                Email = userModel.Email,
                About = userModel.About,
                Address = userModel.Address,
                Batch = userModel.Batch,
                AccountPicture = userModel.AccountPicture,

            };

            // var result = await _userManager.CreateAsync(user, userModel.Password);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            return result;
        }

        //login pake mekanisme ini
        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public async Task<ApplicationUser> FindMe(string email)
        {
            var me = await _userManager.FindByEmailAsync(email);
            return me;
        }
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}