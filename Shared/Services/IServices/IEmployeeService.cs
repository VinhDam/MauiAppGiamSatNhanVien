using Shared.Models;
using Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.IServices
{
    public interface IEmployeeService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(EmployeeDTO dto);
        Task<T> UpdateAsync<T>(int id, EmployeeDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
