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
    public class DepartmentService : BaseService, IDepartmentService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string departmentUrl;
        public DepartmentService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory=clientFactory;
            this.departmentUrl = configuration.GetValue<string>("ServiceUrls:APIkey");
            //this.departmentUrl = "https://localhost:7001";
        }
        public Task<T> CreateAsync<T>(DepartmentDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                Url = departmentUrl+"/api/DepartmentAPI",
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.DELETE,
                Url = departmentUrl+"/api/DepartmentAPI/"+id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,
                Url = departmentUrl+"/api/DepartmentAPI",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,
                Url = departmentUrl+"/api/DepartmentAPI/"+id,
            });
        }

        public Task<T> UpdateAsync<T>(int id, DepartmentDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.PUT,
                Data = dto,
                Url = departmentUrl+"/api/DepartmentAPI/"+id,
            });
        }
    }
}
