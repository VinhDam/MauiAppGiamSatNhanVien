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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public EmployeeRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db=db;
            _mapper = mapper;
        }

        public async Task<Employee> CreateAsync(EmployeeDTO entity)
        {
            var obj = _mapper.Map<Employee>(entity);
            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = obj.CreateDate;
            await _db.Employees.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Employee>> GetAllAsync(Expression<Func<Employee, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Employee> query = _db.Employees;

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

        public async Task<Employee> GetAsync(Expression<Func<Employee, bool>> filter = null, bool tracked = true, string includeProperties = null)
        {
            IQueryable<Employee> query = _db.Employees;

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

        public async Task RemoveAsync(Employee entity)
        {
            _db.Employees.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Employee> UpdateAsync(int id, EmployeeDTO entity)
        {
            var objFromDb = await _db.Employees.Where(u=>u.Id == id).AsNoTracking().FirstOrDefaultAsync();
            var obj = _mapper.Map<Employee>(entity);
            obj.Id = id;
            obj.CreateDate = objFromDb.CreateDate;
            obj.UpdateDate = DateTime.Now;
            _db.Employees.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
    }
}
