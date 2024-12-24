using Shared.Models;
using Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.IServices
{
    public interface ILocationService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(LocationDTO dto);
        Task<T> UpdateAsync<T>(int id, LocationDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
