using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CompanyApplication.Core.Builder;
using CompanyApplication.Core.Contract;
using CompanyApplication.Core.Domain.Exceptions;
using CompanyApplication.Core.Domain.RequestModel;
using CompanyApplication.Core.Domain.ResponseModel;
using CompanyApplication.Core.Service.Helper;
using CompanyApplication.Infra.Contract;
using CompanyApplication.Shared;
using dotenv.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.Core.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly FileUploadHelper _fileUploadHelper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, FileUploadHelper fileHelper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _fileUploadHelper = fileHelper;
        }

        public async Task AddEmployeeAsync(EmployeeRequestModel employeeModel)
        {
            try
            {
                var fileKey = await _fileUploadHelper.UploadFile(employeeModel.CvFile);
                var employee = EmployeeBuilder.Build(employeeModel,fileKey);
                var count = await _employeeRepository.AddEmployee(employee);

                if(count == 0)
                {
                    throw new BadRequestException("Employee is not created.");
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployee(employeeId);

                if(employee == null)
                {
                    throw new NotFoundException($"Employee with {employeeId} is not found.");
                }

                employee.Delete();
                var count = await _employeeRepository.UpdateEmployeeAsync(employee, employeeId);

                if( count == 0)
                {
                    throw new BadRequestException("Employee is not updated.");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<PagedList<EmployeeResponseModel>> GetEmployeeAsync(string searchTerm = null, int page = 1, int pageSize = 5)
        {
            try
            {
                var employees = await _employeeRepository.GetEmployees(searchTerm, page, pageSize);
                var result = _mapper.Map<PagedList<EmployeeResponseModel>>(employees);

                return result;
            }catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<string> ViewFile(int employeeId)
        {
            try
            {
                var file = await _employeeRepository.GetFile(employeeId);
                var content = new System.IO.MemoryStream(file.Length);
                //D:\Namrata\Bigscal\asp\API\CompanyApplicationAPI\CompanyApplicationAPI\FileDownloaded
                var filearr = file.Split("\\");
                var filename = filearr[filearr.Length - 1];

                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), @"D:\Namrata\Bigscal\asp\API\CompanyApplicationAPI\CompanyApplicationAPI\FileDownloaded\",
                    filename);

                await CopyStream(content, path);
                return file;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var filestream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(filestream);
            }
        }

        public async Task<string> DownloadFile(int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetFile(employeeId);
                return employee;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<string> DownloadFromCloudinary(int employeeId)
        {
            try
            {
                var file = _employeeRepository.GetFile(employeeId);
                DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
                Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
                cloudinary.Api.Secure = true;

                var getResource = new GetResourceParams(file.Result)
                {
                    Pages = true
                };
                var info = cloudinary.GetResource(getResource);

                return info.ToString();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateEmployee(EmployeeRequestModel employee, int employeeId)
        {
            try
            {
                var fileKey = await _fileUploadHelper.UploadFile(employee.CvFile);
                var employeetoUpdate = EmployeeBuilder.Build(employee, fileKey);
                var count = await _employeeRepository.UpdateEmployeeAsync(employeetoUpdate,employeeId);

                if (count == 0)
                {
                    throw new BadRequestException("Employee is not updated.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async MemoryStream 
    }
}
