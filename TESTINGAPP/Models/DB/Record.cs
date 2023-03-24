using System.ComponentModel.DataAnnotations;

namespace TESTINGAPP.Models.DB
{
    public class Record
    {
        [Key]
        public int Id { get; set; } // идентификационный номер записи
        public DateTime Date { get; set; } // дата
        public string Title { get; set; } // название
        public string Description { get; set; } // описание
        public List<Category> Categories { get; set; } // категории
        public string url { get; set; } // ссылки
        public byte[] Photo { get; set; } // фотография
    }
}
