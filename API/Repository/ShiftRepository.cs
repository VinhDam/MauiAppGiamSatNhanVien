using API.Data;
using API.Models;
using API.Models.DTO.ShiftDTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Repository
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ShiftRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db=db;
            _mapper = mapper;
        }

        public async Task<Shift> CreateAsync(ShiftDTO entity)
        {
            var obj = _mapper.Map<Shift>(entity);
            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = obj.CreateDate;
            await _db.Shift.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Shift>> GetAllAsync(Expression<Func<Shift, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Shift> query = _db.Shift;

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

        public async Task<Shift> GetAsync(Expression<Func<Shift, bool>> filter = null, bool tracked = true, string includeProperties = null)
        {
            IQueryable<Shift> query = _db.Shift;

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

        public async Task RemoveAsync(Shift entity)
        {
            _db.Shift.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Shift> UpdateAsync(UpdateShiftDTO entity)
        {
            var objFromDb = _db.Shift.Where(u=>u.Id==entity.Id).AsNoTracking().FirstOrDefault();
            var obj = _mapper.Map<Shift>(entity);
            obj.CreateDate = objFromDb.CreateDate;
            obj.UpdateDate = DateTime.Now;
            _db.Shift.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
    }
}
