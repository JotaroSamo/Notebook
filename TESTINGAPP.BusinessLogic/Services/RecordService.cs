using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Record>> AllRecord(int UserId)
        {
            return await _recordContext.Records.Where(c => c.UserId == UserId).ToListAsync();
        }

        public async Task RecordCreate(RecordCreateDto record, int id)
        {

            if (record!= null)
            {

                var rec = await MappingInRecord(record, id);
                 _recordContext.Records.Add(rec);
                await _recordContext.SaveChangesAsync();
            }
          

          
        }


        public async Task DeleteRecord(int id)
        {

            _recordContext.Remove(await _recordContext.Records.FirstOrDefaultAsync(c => c.Id == id));
            _recordContext.SaveChanges();
        }
        public async Task<RecordCreateDto> GetRecordById(int id)
        {
            return await MappingInRecordCreateDto(await _recordContext.Records.FirstOrDefaultAsync(c => c.Id == id));
        }
        public async Task EditRecord(RecordCreateDto record, int id, int UserId)
        {
            if (record != null)
            {

                var rec = await MappingInRecordEdit(record, id, UserId);
                _recordContext.Records.Update(rec);
                _recordContext.SaveChanges();
            }
           
        }
        private async Task<RecordCreateDto> MappingInRecordCreateDto(Record record)
        {
            var rec = new RecordCreateDto()
            {
                Title = record.Title,
                Description = record.Description,
                Categories = record.Categories,
                Url = record.Url
            };

            return rec;
        }
        private async Task<Record> MappingInRecordEdit(RecordCreateDto record, int id, int UserId)
        {

            var rec = new Record()
            {
                Id = id,
                Date = DateTime.Now,
                Title = record.Title,
                Description = record.Description,
                Categories = record.Categories,
                Url = record.Url,
                FileName = record.Photo?.FileName,
                UserId= UserId
            };
            if (record.Photo != null)
            {
                rec.Photo = await ConvertToByteArray(record.Photo);
            }

            return rec;
        }
        private async Task<Record> MappingInRecord(RecordCreateDto record, int id)
        {

            var rec = new Record()
            {
                Date = DateTime.Now,
                Title = record.Title,
                Description = record.Description,
                Categories = record.Categories,
                Url = record.Url,
                FileName = record.Photo?.FileName,
                UserId = id
            };
            if (record.Photo!=null)
            {
                rec.Photo = await ConvertToByteArray(record.Photo);
            }

            return rec;
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

        public async Task<List<Record>> SearchAsync(string searchString)
        {
            var record = from u in _recordContext.Records
                        select u;

#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            record = record.Where(u => u.Title.Contains(searchString) || u.Categories.Contains(searchString)|| u.Url.Contains(searchString)|| u.Description.Contains(searchString));
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

            var recordList = await record.ToListAsync();
            return recordList;
        }
    }
}
