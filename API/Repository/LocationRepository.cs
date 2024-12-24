using API.Data;
using API.Models;
using API.Models.DTO.LocationDTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public LocationRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db=db;
            _mapper = mapper;
        }

        public async Task<Location> CreateAsync(LocationDTO entity)
        {
            var obj = _mapper.Map<Location>(entity);
            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = obj.CreateDate;
            await _db.Location.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Location>> GetAllAsync(Expression<Func<Location, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Location> query = _db.Location;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<Location> GetAsync(Expression<Func<Location, bool>> filter = null, bool tracked = true, string includeProperties = null)
        {
            IQueryable<Location> query = _db.Location;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Location entity)
        {
            _db.Location.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Location> UpdateAsync(UpdateLocationDTO entity)
        {
            var objFromDb = _db.Location.Where(u=>u.Id==entity.Id).AsNoTracking().FirstOrDefault();
            var obj = _mapper.Map<Location>(entity);
            obj.CreateDate = objFromDb.CreateDate;
            obj.UpdateDate = DateTime.Now;
            _db.Location.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
    }
}
