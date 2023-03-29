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
  
            _recordContext.Users.Remove(await GetById(id));
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

            return user;
        }
    }
}
