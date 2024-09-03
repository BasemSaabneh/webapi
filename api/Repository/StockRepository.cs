using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interface;
using api.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
//https://www.youtube.com/watch?v=F_b5wjkpg8M&list=PL82C6-O4XrHfrGOCPmKmwTO7M0avXyQKc&index=20
namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
         _context = context;  
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
             var stockModel = await _context.Stock.FirstOrDefaultAsync(x=>x.Id == id);
             if(stockModel == null)
             {
                return null;
             }
             _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks =  _context.Stock.Include(c=>c.Comments).AsQueryable();
            if(!String.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stocks = stocks.Where(s=>s.CompanyName.Contains(queryObject.CompanyName));
            }
            if(!String.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stocks = stocks.Where(s=>s.Symbol.Contains(queryObject.Symbol));
            }    

            if(!String.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if(queryObject.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDecsending ? stocks.OrderByDescending(s =>s.Symbol) : stocks.OrderBy(s=>s.Symbol);
                }
            }   

            var skipNumnber = (queryObject.PageNumber - 1 ) * queryObject.PageSize;
            return await stocks.Skip(skipNumnber).Take(queryObject.PageSize).ToListAsync();       
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.Include(c=>c.Comments).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stock.AnyAsync(s=>s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, StockUpdateDto stockUpdateDto)
        {
             var stockModel = await _context.Stock.Include(c=>c.Comments).FirstOrDefaultAsync(x=>x.Id == id);
             if(stockModel == null)
             {
                return null;
             }
            stockModel.CompanyName = stockUpdateDto.CompanyName;
            stockModel.Industry = stockUpdateDto.Industry;
            stockModel.LastDiv = stockUpdateDto.LastDiv;
            stockModel.MarketCap = stockUpdateDto.MarketCap;
            stockModel.Purchase = stockUpdateDto.Purchase;
            stockModel.Symbol = stockUpdateDto.Symbol;
            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}