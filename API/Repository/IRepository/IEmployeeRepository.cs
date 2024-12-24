using API.Models;
using API.Models.DTO;
using System.Linq.Expressions;

namespace API.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync(Expression<Func<Employee, bool>>? filter = null, string? includeProperties = null);
        Task<Employee> GetAsync(Expression<Func<Employee, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task<Employee> UpdateAsync(int id, EmployeeDTO entity);
        Task<Employee> CreateAsync(EmployeeDTO entity);
        Task RemoveAsync(Employee entity);
    }
}
