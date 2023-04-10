using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTINGAPP.Common.Dto
{
    public class RecordCreateDto
    {
        
        public string? Title { get; set; } // название
        public string? Description { get; set; } // описание
        public string? Categories { get; set; } // категории
        public string? Url { get; set; } // ссылки
        public IFormFile? Photo { get; set; }
    }
}
