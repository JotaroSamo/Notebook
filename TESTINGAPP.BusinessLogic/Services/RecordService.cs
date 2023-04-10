using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
    public class RecordService : IRecordService
    {
        private RecordContext _recordContext;

        public RecordService(RecordContext recordContext) { 
            _recordContext = recordContext;

        }
        public Task<List<Record>> AllRecord(int id)
        {
            throw new NotImplementedException();
        }

        public async Task RecordCreate(RecordCreateDto record, int id)
        {

            if (record!= null)
            {
                var rec = new Record()
                {
                    Date = DateTime.Now,
                    Title = record.Title,
                    Description = record.Description,
                    Categories = record.Categories,
                    Url = record.Url,
                    Photo = await ConvertToByteArray(record.Photo),
                    UserId = id
                };

                _recordContext.Records.Add(rec);
                await _recordContext.SaveChangesAsync();
            }
          

          
        }

        public async Task DelateRecord(int id)
        {
           _recordContext.Remove(await _recordContext.Records.FirstOrDefaultAsync(c=> c.Id==id));
          await _recordContext.SaveChangesAsync();
        }

        public async Task EditRecord(Record record)
        {
            _recordContext.Records.Update(record);
            await _recordContext.SaveChangesAsync();
        }
        private async Task<byte[]> ConvertToByteArray(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                return stream.ToArray();
            }
        }


    }
}
