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
        Task CreateAsync(UserCreateDto userCreateDto);


        CheckUser Maping(UserCreateDto model);

        Task<UserDto> GetAsync(UserAuthDto userAuthDto);

        Task<UserDto> GetCheckAsync(CheckUser checkUser);

        Task<bool> CheckNull(UserCreateDto model);
      

    }
}
