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
    public class ShiftService : BaseService, IShiftService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string shiftUrl;
        public ShiftService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory=clientFactory;
            this.shiftUrl = configuration.GetValue<string>("ServiceUrls:APIkey");
            //this.shiftUrl = "https://localhost:7001";
        }
        public Task<T> CreateAsync<T>(ShiftDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                Url = shiftUrl+"/api/ShiftAPI",
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.DELETE,
                Url = shiftUrl+"/api/ShiftAPI/"+id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,
                Url = shiftUrl+"/api/ShiftAPI",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,
                Url = shiftUrl+"/api/ShiftAPI/"+id,
            });
        }

        public Task<T> UpdateAsync<T>(int id, ShiftDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.PUT,
                Data = dto,
                Url = shiftUrl+"/api/ShiftAPI/"+id,
            });
        }
    }
}
