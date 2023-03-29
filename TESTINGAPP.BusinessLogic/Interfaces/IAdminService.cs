using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTINGAPP.Models;

namespace TESTINGAPP.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        Task<List<User>> GetAll();
        Task Delete(int id);
    }
}
