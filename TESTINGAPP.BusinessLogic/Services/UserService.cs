
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
    public class UserService : IUserService
    {
        private readonly RecordContext _recordContext;

		private readonly IMapper _mapper;
		public UserService(RecordContext recordContext, IMapper mapper)
        {
            _recordContext = recordContext;
			_mapper = mapper;
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
          
            _recordContext.Users.Add(_mapper.Map<UserCreateDto, User>(userCreateDto));
            await _recordContext.SaveChangesAsync();
            
        }

       

        public async Task<User> GetAsync(UserAuthDto userAuthDto)
        {
            var user = await _recordContext.Users.FirstOrDefaultAsync(u =>
                u.Email == userAuthDto.Email && u.Password == userAuthDto.Password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<UserAuthDto> GetCheckAsync(CheckUser checkUser)
        {
            var user = await _recordContext.Users.FirstOrDefaultAsync(u =>
                 u.Email == checkUser.Email || u.Name == checkUser.Name);
            if (user == null)
            {
                return null;
            }
			return _mapper.Map<User, UserAuthDto>(user);
		}

     
    }
}
