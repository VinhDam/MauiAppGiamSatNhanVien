﻿using Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.IServices
{
    public interface IShiftService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ShiftDTO dto);
        Task<T> UpdateAsync<T>(int id,ShiftDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
