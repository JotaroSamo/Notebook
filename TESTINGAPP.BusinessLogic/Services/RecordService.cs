using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
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
#pragma warning disable CS8634 // Тип не может быть использован как параметр типа в универсальном типе или методе. Допустимость значения NULL для аргумента типа не соответствует ограничению "class".
            _recordContext.Remove(await _recordContext.Records.FirstOrDefaultAsync(c => c.Id == id));
#pragma warning restore CS8634 // Тип не может быть использован как параметр типа в универсальном типе или методе. Допустимость значения NULL для аргумента типа не соответствует ограничению "class".
            await _recordContext.SaveChangesAsync();
        }

        public async Task EditRecord(RecordCreateDto record, int id)
        {
            if (record != null)
            {

                var rec = await MappingInRecord(record, id);
                _recordContext.Records.Update(rec);
                await _recordContext.SaveChangesAsync();
            }
           
        }
        private async Task<Record> MappingInRecord(RecordCreateDto record, int id)
        {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            var rec = new Record()
            {
                Date = DateTime.Now,
                Title = record.Title,
                Description = record.Description,
                Categories = record.Categories,
                Url = record.Url,
                Photo = await ConvertToByteArray(record.Photo),
                FileName = record.Photo.FileName,
                UserId = id
            };
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            return rec;
        }
#pragma warning disable CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
        private async Task<IFormFile> ConvertToIFormFile(byte[] bytes, string fileName)
#pragma warning restore CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
        {
            if (bytes == null)
            {
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
                return null;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
            }

            var ms = new MemoryStream(bytes);
            var formFile = new FormFile(ms, 0, ms.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream",
            };

            return formFile;
        }
        private async Task<byte[]> ConvertToByteArray(IFormFile file)
        {
            if (file == null)
            {
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
                return null;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
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
