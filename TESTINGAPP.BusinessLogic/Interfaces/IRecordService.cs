using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTINGAPP.Common.Dto;
using TESTINGAPP.Models;

namespace TESTINGAPP.BusinessLogic.Interfaces
{
   public interface IRecordService
    {
        Task<List<Record>> AllRecord(int UserId);
        Task RecordCreate(RecordCreateDto record, int id);
        Task DeleteRecord(int id);
        Task EditRecord(RecordCreateDto record, int id);
        Task<List<Record>> SearchAsync(string searchString);
    }
}
