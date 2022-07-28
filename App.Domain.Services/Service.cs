using App.Domain.Core.Contracts.Services;
using App.Domain.Core.Entities;
using App.InfraStructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services
{
    public class Service : IService
    {
        private readonly AppDbContext _context;
        public Service(AppDbContext context)
        {
            _context = context;
        }

        public async Task SetProfileImg(Guid userId, FileInfo file)
        {
            try
            {
                //var imgString = file.Substring(22);  // remove data:image/png;base64,
                //byte[] imgBytes = Convert.FromBase64String(file);

                //MemoryStream ms = new MemoryStream(file);

                //Image baseImg = Image.FromStream(file);



                ApplicationUser user = _context.AppUsers.Find(userId);

                //string imgName = user.UserName + ".png";

                string imgUrl = Path.Combine("wwwroot/Images/Site/Profile/", file.Name);

                //upload image (file) on host
                File.Create(imgUrl);


                //save image name (string) in database
             //   user.PictureFileId = file.Name;

                _context.SaveChanges();

                 await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                var code = ex.InnerException;
                 await Task.FromResult(false);
            }
        }
    }
}
