﻿using API.Models;
using API.Models.DTO;
using System.Linq.Expressions;

namespace API.Repository.IRepository
{
    public interface IShiftRepository
    {
        Task<List<Shift>> GetAllAsync(Expression<Func<Shift, bool>>? filter = null, string? includeProperties = null);
        Task<Shift> GetAsync(Expression<Func<Shift, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task<Shift> UpdateAsync(int id, ShiftDTO entity);
        Task<Shift> CreateAsync(ShiftDTO entity);
        Task RemoveAsync(Shift entity);
    }
}
