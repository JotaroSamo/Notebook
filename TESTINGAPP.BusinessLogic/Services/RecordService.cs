using AutoMapper;
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
        private  readonly RecordContext _recordContext;
        private readonly IMapper _mapper;

        public RecordService(RecordContext recordContext, IMapper mapper) { 
            _recordContext = recordContext;
            _mapper = mapper;

        }
        public async Task<List<RecordDto>> AllRecord(int UserId)
        {
            return _mapper.Map<List<Record>,List<RecordDto>>(await _recordContext.Records.Where(c => c.UserId == UserId).ToListAsync());
        }

        public async Task RecordCreate(RecordDto record, int id)
        {

            if (record!= null)
            {

                var rec = _mapper.Map<RecordDto, Record>(record);
                rec.UserId = id;
                 _recordContext.Records.Add(rec);
                await _recordContext.SaveChangesAsync();
            }
        }


        public async Task DeleteRecord(int id)
        {

            _recordContext.Remove(await _recordContext.Records.FirstOrDefaultAsync(c => c.Id == id));
            _recordContext.SaveChanges();
        }
        public async Task<RecordDto> GetRecordDtoById(int id)
        {
            return await MappingInRecordCreateDto(await _recordContext.Records.FirstOrDefaultAsync(c => c.Id == id));
        }
        public async Task EditRecord(RecordDto record, int id, int UserId)
        {
            if (record != null)
            {

				var rec = _mapper.Map<RecordDto, Record>(record);
				rec.UserId = UserId;
                rec.Id = id;
				if (record.DeletePhoto)
				{
					rec.Photo = null;
				}
				_recordContext.Records.Update(rec);
                await _recordContext.SaveChangesAsync();
            }
           
        }
        private async Task<RecordDto> MappingInRecordCreateDto(Record record)
        {
            var rec = new RecordDto()
            {
                Title = record.Title,
                Description = record.Description,
                Categories = record.Categories,
                Url = record.Url,
                Photo= record.Photo
            };

            return rec;
        }
        private async Task<Record> GetRecordById(int id)
        {
            return await _recordContext.Records.FirstOrDefaultAsync(c => c.Id == id);
        }
   
        public async Task<byte[]> ConvertToByteArray(IFormFile file)
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

            record = record.Where(u => u.Title.Contains(searchString) || u.Categories.Contains(searchString)|| u.Url.Contains(searchString)|| u.Description.Contains(searchString));

            var recordList = await record.ToListAsync();
            return recordList;
        }
    }
}
