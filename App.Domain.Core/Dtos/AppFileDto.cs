using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos
{
    public class AppFileDto
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
