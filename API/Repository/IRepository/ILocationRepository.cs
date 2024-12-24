using API.Models;
using API.Models.DTO;
using System.Linq.Expressions;

namespace API.Repository.IRepository
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllAsync(Expression<Func<Location, bool>>? filter = null, string? includeProperties = null);
        Task<Location> GetAsync(Expression<Func<Location, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task<Location> UpdateAsync(int id, LocationDTO entity);
        Task<Location> CreateAsync(LocationDTO entity);
        Task RemoveAsync(Location entity);
    }
}
