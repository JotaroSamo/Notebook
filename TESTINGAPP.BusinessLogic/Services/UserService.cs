
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTINGAPP.BusinessLogic.Interfaces;
using TESTINGAPP.Common.Dto;
using TESTINGAPP.Models;

namespace TESTINGAPP.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly RecordContext _recordContext;


        public UserService(RecordContext recordContext)
        {
            _recordContext = recordContext;
          
        }

        public async Task<bool> CheckNull(UserCreateDto model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Age))
            {
                return false;
            }
            return true;
        }

        public async Task CreateAsync(UserCreateDto userCreateDto)
        {
            var user = new User
            {
                Name = userCreateDto.Name,
                Email = userCreateDto.Email,
                Password = userCreateDto.Password,
                Age = userCreateDto.Age,
                Role = "User"
            };

            _recordContext.Users.Add(user);
            await _recordContext.SaveChangesAsync();
            
        }

       

        public async Task<UserDto> GetAsync(UserAuthDto userAuthDto)
        {
            var user = await _recordContext.Users.FirstOrDefaultAsync(u =>
                u.Email == userAuthDto.Email && u.Password == userAuthDto.Password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<UserDto> GetCheckAsync(CheckUser checkUser)
        {
            var user = await _recordContext.Users.FirstOrDefaultAsync(u =>
                 u.Email == checkUser.Email || u.Name == checkUser.Name);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public CheckUser Maping(UserCreateDto model)
        {
            var check = new CheckUser()
            {
                Email = model.Email,
                Name = model.Name

            };
            return check;
        }
    }
}
