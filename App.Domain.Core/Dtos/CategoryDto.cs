using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
    }
}
