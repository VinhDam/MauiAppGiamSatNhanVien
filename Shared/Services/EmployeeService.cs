using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Shared.Models;
using Shared.Models.DTO;
using Shared.Services.IServices;
using Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string employeeUrl;
        public EmployeeService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory=clientFactory;
            this.employeeUrl = configuration.GetValue<string>("ServiceUrls:APIkey");
            //this.employeeUrl = "https://localhost:7001";
        }
        public Task<T> CreateAsync<T>(EmployeeDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                Url = employeeUrl+"/api/EmployeeAPI",
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.DELETE,
                Url = employeeUrl+"/api/EmployeeAPI/"+id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,
                Url = employeeUrl+"/api/EmployeeAPI",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,
                Url = employeeUrl+"/api/EmployeeAPI/"+id,
            });
        }

        public Task<T> UpdateAsync<T>(int id, EmployeeDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.PUT,
                Data = dto,
                Url = employeeUrl+"/api/EmployeeAPI/"+id,
            });
        }
    }
}
