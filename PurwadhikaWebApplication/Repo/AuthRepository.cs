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
        //Register ini gak kepake, just in case aja
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
        public async Task<ApplicationUser> FindUser(string userName)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(userName);
            return user;
        }
        public string HashPassword(string newPassword)
        {
            return _userManager.PasswordHasher.HashPassword(newPassword);
        }
        public async Task<IdentityResult> EditMe(ApplicationUser user)
        {
            //var editProfile = new ApplicationUser();
            
            //me = new ApplicationUser();
            //var me = await _userManager.FindByEmailAsync(email);
            //me.About = myModel.About;
            //me.AccountPicture = myModel.uri;
            //me.Address = myModel.Address;
            //me.Experience = myModel.Experience;
            //me.Skills = myModel.Skills;
            ////  me.PasswordHash = myModel.Password;
            //var newPassword = _userManager.PasswordHasher.HashPassword(myModel.Password);
            //me.PasswordHash = newPassword;

            var updated = await _userManager.UpdateAsync(user);
           
            return updated;
            
        }

        //public async Task<IdentityResult>ChangePassword(string email, string newPassword)
        //{
        //    //var store = await _userManager.hash
        //}
        public async Task<ApplicationUser> FindMe(string email)
        {
            var me = await _userManager.FindByEmailAsync(email);

            var myModel = new ApplicationUser
            {
                Id = me.Id,
                Firstname = me.Firstname,
                Lastname = me.Lastname,
                Gender = me.Gender,
                Address = me.Address,
                Batch = me.Batch,
                AccountPicture = me.AccountPicture,
                AccountTranscript = me.AccountTranscript,
                About = me.About,
                Skills = me.Skills,
                Experience = me.Experience,
                Email = me.Email,
                PhoneNumber = me.PhoneNumber,
                InstanceName = me.InstanceName,
                UserName = me.UserName

            };

            return myModel;
        }
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}