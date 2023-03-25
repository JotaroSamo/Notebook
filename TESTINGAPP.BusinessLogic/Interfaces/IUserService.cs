using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTINGAPP.Common.Dto;
using TESTINGAPP.Models;

namespace TESTINGAPP.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        void Create(UserCreateDto userCreateDto);

        List<User> GetAll();

        User Get(UserAuthDto userAuthDto);
    }
}
