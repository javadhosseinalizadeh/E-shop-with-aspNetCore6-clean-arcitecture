using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class UserFile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FileId { get; set; }
        public bool IsDeleted { get; set; }



        public virtual AppUser User { get; set; }
        public virtual AppFile File { get; set; }
    }
}
