using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notebook.Common.Dto;
using Notebook.Models;

namespace Notebook.BusinessLogic.Interfaces
{
   public interface IRecordService
    {
        Task<List<RecordDto>> AllRecord(int UserId);
        Task RecordCreate(RecordDto record, int id);
        Task DeleteRecord(int id);
        Task EditRecord(RecordDto record, int id, int UserId);
        Task<List<RecordDto>> SearchAsync(string searchString, int UserId);
        Task<RecordDto> GetRecordDtoById(int id);
        Task<byte[]> ConvertToByteArray(IFormFile file);

	}
}
