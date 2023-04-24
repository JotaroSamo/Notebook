using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notebook.BusinessLogic.Interfaces;
using Notebook.Common.Dto;
using Notebook.Models;

namespace Notebook.BusinessLogic.Services
{
    public class AdminService : IAdminService
    {
      private readonly  RecordContext _recordContext;
        private readonly IMapper _mapper;
        public AdminService( RecordContext recordContext, IMapper mapper) 
        { 
            _recordContext = recordContext;
            _mapper = mapper;
        }

        public async Task Delete(int id)
        {
            var recordsToRemove = _recordContext.Records.Where(c => c.UserId == id).ToList();
            _recordContext.Records.RemoveRange(recordsToRemove);
            var userToRemove = await _recordContext.Users.FindAsync(id);
            _recordContext.Users.Remove(userToRemove);

            await _recordContext.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAll()
        {
            return _mapper.Map<List<User>,List<UserDto>>(await _recordContext.Users.ToListAsync());
        }


        public async Task<UserDto> GetById(int id)
        {
            return _mapper.Map<User, UserDto>(await _recordContext.Users.FindAsync(id));

		}

        public async Task<List<UserDto>> SearchAsync(string searchString)
        {

            var users = from u in _recordContext.Users
                        select u;
          
                users = users.Where(u => u.Name.Contains(searchString) || u.Email.Contains(searchString));
           
            return _mapper.Map<List<User>, List<UserDto>>(await users.ToListAsync());
		}

        public async Task UpdateUser(UserDto user)
        {
            _recordContext.Update(_mapper.Map<UserDto, User>(user));
            await _recordContext.SaveChangesAsync();
        }
    }
}
