using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Notebook.Common.Dto;
using Notebook.Models;

namespace Notebook.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        Task<List<UserDto>> GetAll();
        Task Delete(int id);
        Task<UserDto> GetById(int id);
        Task UpdateUser(UserDto user);

        Task<List<UserDto>> SearchAsync(string searchString);
    }
}
