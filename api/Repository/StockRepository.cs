using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<Stock> FindSingleAsync(int id)
        {
            return _context.Stock.SingleAsync(x => x.Id == id);
        }

        public ValueTask<Stock?> FindByIdAsync(int id)
        {

            return _context.Stock.FindAsync(id);
        }

        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stock.ToListAsync();
        }

        ValueTask<EntityEntry<Stock>> IStockRepository.AddStockAsync(Stock stock)
        {
            return _context.Stock.AddAsync(stock);
        }
    }
}