using API.Models;
using API.Models.DTO;
using System.Linq.Expressions;

namespace API.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync(Expression<Func<Department, bool>>? filter = null, string? includeProperties = null);
        Task<Department> GetAsync(Expression<Func<Department, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task<Department> UpdateAsync(int id, DepartmentDTO entity);
        Task<Department> CreateAsync(DepartmentDTO entity);
        Task RemoveAsync(Department entity);
    }
}
