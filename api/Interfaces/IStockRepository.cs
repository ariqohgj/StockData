using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        ValueTask<Stock?> FindByIdAsync(int id);
        ValueTask<EntityEntry<Stock>> AddStockAsync(Stock stock);
        Task<Stock> FindSingleAsync(int id);
    }
}