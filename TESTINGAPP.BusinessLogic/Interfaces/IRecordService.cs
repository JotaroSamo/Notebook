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
        Task<List<Record>> AllRecord(int id);
        Task RecordCreate(RecordCreateDto record, int id);
        Task DelateRecord(int id);
        Task EditRecord(Record record);
    }
}
