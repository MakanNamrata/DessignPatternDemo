using CompanyApplication.Core.Contract;
using CompanyApplication.Core.Domain.RequestModel;
using CompanyApplication.Infra.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Net;
using System.Text;

namespace CompanyApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyApplicationController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly CompanyDBContext _context;

        public CompanyApplicationController(IEmployeeService employeeService, CompanyDBContext context)
        {
            _employeeService = employeeService;
            _context = context;
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees(string? searchTerm = null, int page=1, int pageSize = 5)
        {
            var employees = await _employeeService.GetEmployeeAsync(searchTerm, page, pageSize);    
            return Ok(employees);
        }

        [HttpPost("employee")]
        public async Task<IActionResult> PostEmployees([FromForm] EmployeeRequestModel employee)
        {
            await _employeeService.AddEmployeeAsync(employee);
            return Created("employees", null);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromForm] EmployeeRequestModel employee, int employeeId)
        {
            
            await _employeeService.UpdateEmployee(employee,employeeId);
            return Ok(employee);
        }

        [HttpDelete("employee/{id}")]
        public async Task<IActionResult> DeleteEmployees(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpGet("DownloadMyFile/{id}")]
        public async Task<IActionResult> DownloadMyFile(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            try
            {
                var file = await _employeeService.ViewFile(id);
                var filearr = file.Split("\\");
                var filename = filearr[filearr.Length - 1];
                /* var bytes = Encoding.UTF8.GetBytes(filename);
                 var base64 = Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);
                 var base64Encoded = Convert.FromBase64String(base64);
                 var inputString = Encoding.UTF8.GetString(base64Encoded);
                 return File(inputString, "application/pdf");*/
                var bytes = await System.IO.File.ReadAllBytesAsync(file);
                return File(bytes, "application/pdf");
                //return Ok("File Downloaded successfully.");
            }
            catch (Exception)
            {
                throw;
            }
            
            /*var file = employee.Split("/");
            var name = file[file.Length-1];
            Response.Headers.Add("Content-Disposition", $"inline; filename={name}");
            //return File(name, "application/pdf");

            IFileProvider provider = new PhysicalFileProvider(employee);
            IFileInfo fileInfo = provider.GetFileInfo(name);
            var readStream = fileInfo.CreateReadStream();

            return File(readStream, "application/pdf", name);*/
        }

        [HttpGet("DownloadFileAsync")]
        public async Task<IActionResult> DownloadFileAsync(int employeeId)
        {
            var result1 = await _employeeService.DownloadFile(employeeId);
            var filepath = result1.Split("/");
            var filename = filepath[filepath.Length - 1];

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var filePath = $"D:\\Downloads\\{filename}";
            var bytes = System.IO.File.ReadAllBytes(filePath);

            result.Content = new ByteArrayContent(bytes);

            var mediaType = "application/octet-stream";
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType);
            //return Ok(result);

            /*var filepath = result.Split("/");
            var filename = filepath[filepath.Length - 1];
            *//*var bytes = Encoding.UTF8.GetBytes(@"D:\Namrata\Bigscal\asp\API\CompanyApplicationAPI\CompanyApplicationAPI\FileDownloaded\Namrata_Resume.pdf");
            
            return File(bytes, "application/pdf", "Resume.pdf");*//*

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(result), $"D:\\Downloads\\{filename}");
            }
            return Ok();*/
            /*
                        using (var client = new HttpClient())
                        {

                            using (var result1 =await client.GetAsync(result))
                            {
                                if (result1.IsSuccessStatusCode)
                                {
                                    return await result1.Content.ReadAsByteArrayAsync();
                                }

                            }
                        }
                        return null;*/
            var filePath2 = $"{filename}.txt"; // Here, you should validate the request and the existance of the file.

            var bytes2 = await System.IO.File.ReadAllBytesAsync(filePath2);
            return File(bytes2, "text/plain", Path.GetFileName(filePath));
        }

        [HttpGet("DownloadCloudinaryFile")]
        public async Task<IActionResult> DownloadCloudinaryFile(int employeeId)
        {
            var file = _employeeService.DownloadFromCloudinary(employeeId);
            return Ok(file);
        }
    }
}
