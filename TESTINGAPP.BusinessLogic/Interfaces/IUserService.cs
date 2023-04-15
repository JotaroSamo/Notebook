using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notebook.Common.Dto;
using Notebook.Models;


namespace Notebook.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(UserCreateDto userCreateDto);


        Task<User> GetAsync(UserAuthDto userAuthDto);

        Task<UserAuthDto> GetCheckAsync(CheckUser checkUser);

        Task<bool> CheckNull(UserCreateDto model);
      

    }
}
