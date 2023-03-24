using System.ComponentModel.DataAnnotations;

namespace TESTINGAPP.Models.DB
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Record> Records { get; set; }
    }
}
