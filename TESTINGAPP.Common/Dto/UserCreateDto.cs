using Notebook.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Common.Dto
{
    public class UserCreateDto
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
       
        public string Age { get; set; }

        public Role Role { get; set; }
    }
}
