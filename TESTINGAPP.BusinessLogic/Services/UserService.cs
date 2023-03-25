using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(RecordContext recordContext, IMapper mapper)
        {
            _recordContext = recordContext;
            _mapper = mapper;
        }

        public void Create(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<UserCreateDto, User>(userCreateDto);

            _recordContext.Users.Add(user);
            _recordContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            var users = _recordContext.Users.ToList();

            return users;
        }

        public User Get(UserAuthDto userAuthDto)
        {
            var user = _recordContext.Users.FirstOrDefault(u =>
                u.Email == userAuthDto.Email && u.Password == userAuthDto.Password);

            return user;
        }
    }
}
