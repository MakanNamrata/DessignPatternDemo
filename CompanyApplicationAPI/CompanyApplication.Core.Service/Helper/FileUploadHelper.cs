using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CompanyApplication.Core.Domain.Exceptions;
using CompanyApplication.Infra.Domain;
using CompanyApplication.Infra.Domain.Entity;
using dotenv.net;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.Core.Service.Helper
{
    public class FileUploadHelper
    {
        private readonly CompanyDBContext _context;

        public FileUploadHelper(CompanyDBContext context)
        {
            _context = context;
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                string path = "";
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);

                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return path + "\\" + file.FileName;
                }
                else
                {
                    throw new BadRequestException("Cv is not uploaded successfully.");
                }
                /*DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
                Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
                cloudinary.Api.Secure = true;

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    UseFilename = true,
                    UniqueFilename = true,
                    Overwrite = true
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                return uploadResult.SecureUrl.ToString();*/
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        
    }
}
