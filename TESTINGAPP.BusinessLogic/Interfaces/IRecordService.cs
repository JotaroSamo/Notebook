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
        Task RecordCreate(RecordDto record, int id);
        Task DeleteRecord(int id);
        Task EditRecord(RecordDto record, int id, int UserId);
        Task<List<Record>> SearchAsync(string searchString);
        Task<RecordDto> GetRecordDtoById(int id);
    }
}
