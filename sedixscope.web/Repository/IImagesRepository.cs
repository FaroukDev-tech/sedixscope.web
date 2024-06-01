using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sedixscope.web.Repository
{
    public interface IImagesRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}