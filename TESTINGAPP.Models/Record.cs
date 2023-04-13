using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTINGAPP.Models
{
    public class Record
    {
        [Key]
        public int Id { get; set; } 
        public DateTime Date { get; set; } 
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public string? Categories { get; set; } 
        public string? Url { get; set; } 
        public byte[]? Photo { get; set; } 
        public int UserId { get; set; } 
    }
}
