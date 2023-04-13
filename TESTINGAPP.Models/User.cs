using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notebook.Common.Enum;

namespace TESTINGAPP.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Age { get; set; }

		public Role Role { get; set; }

	}
}
