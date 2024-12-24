using API.Data;
using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public DepartmentRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db=db;
            _mapper = mapper;
        }

        public async Task<Department> CreateAsync(DepartmentDTO entity)
        {
            var obj = _mapper.Map<Department>(entity);
            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = obj.CreateDate;
            await _db.Department.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Department>> GetAllAsync(Expression<Func<Department, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Department> query = _db.Department;

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

        public async Task<Department> GetAsync(Expression<Func<Department, bool>> filter = null, bool tracked = true, string includeProperties = null)
        {
            IQueryable<Department> query = _db.Department;

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

        public async Task RemoveAsync(Department entity)
        {
            _db.Department.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Department> UpdateAsync(int id, DepartmentDTO entity)
        {
            var objFromDb = _db.Department.Where(u=>u.Id==id).AsNoTracking().FirstOrDefault();
            var obj = _mapper.Map<Department>(entity);
            obj.Id = id;
            obj.CreateDate = objFromDb.CreateDate;
            obj.UpdateDate = DateTime.Now;
            _db.Department.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
    }
}
