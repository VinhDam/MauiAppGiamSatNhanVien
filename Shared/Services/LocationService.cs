using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Shared.Models;
using Shared.Models.DTO.LocationDTO;
using Shared.Services.IServices;
using Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class LocationService : BaseService, ILocationService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string locationUrl;
        public LocationService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory=clientFactory;
            this.locationUrl = configuration.GetValue<string>("ServiceUrls:LocationAPI");
            //this.locationUrl = "https://localhost:7001";
        }
        public Task<T> CreateAsync<T>(LocationDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                Url = locationUrl+"/api/LocationAPI",
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.DELETE,
                Url = locationUrl+"/api/LocationAPI/"+id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,
                Url = locationUrl+"/api/LocationAPI",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.GET,
                Url = locationUrl+"/api/LocationAPI/"+id,
            });
        }

        public Task<T> UpdateAsync<T>(UpdateLocationDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.ApiType.PUT,
                Data = dto,
                Url = locationUrl+"/api/LocationAPI/"+dto.Id,
            });
        }
    }
}
