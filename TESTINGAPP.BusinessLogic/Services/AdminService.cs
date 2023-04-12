using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTINGAPP.BusinessLogic.Interfaces;
using TESTINGAPP.Models;

namespace TESTINGAPP.BusinessLogic.Services
{
    public class AdminService : IAdminService
    {
        RecordContext _recordContext;
        public AdminService() { 
            _recordContext = new RecordContext();
        }

        public async Task Delete(int id)
        {
            var recordsToRemove = _recordContext.Records.Where(c => c.UserId == id).ToList();
            _recordContext.Records.RemoveRange(recordsToRemove);
            var userToRemove = await _recordContext.Users.FindAsync(id);
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            _recordContext.Users.Remove(userToRemove);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            await _recordContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _recordContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _recordContext.Users.FindAsync(id);
            
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
            return user;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        }

        public async Task<List<User>> SearchAsync(string searchString)
        {

            var users = from u in _recordContext.Users
                        select u;
          
                users = users.Where(u => u.Name.Contains(searchString) || u.Email.Contains(searchString));
           
            var userList = await users.ToListAsync();
            return userList;
        }

        public async Task UpdateUser(User user)
        {
            _recordContext.Update(user);
            await _recordContext.SaveChangesAsync();
        }
    }
}
