using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Model;
//https://www.youtube.com/watch?v=F_b5wjkpg8M&list=PL82C6-O4XrHfrGOCPmKmwTO7M0avXyQKc&index=20

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDTO(this Stock stockModel)
        {
            return new StockDto
            {
                 Id = stockModel.Id,
                 CompanyName=stockModel.CompanyName,
                 Symbol=stockModel.Symbol,
                 Purchase=stockModel.Purchase,
                 Industry=stockModel.Industry,
                 LastDiv=stockModel.LastDiv,
                 MarketCap=stockModel.MarketCap,
                 Comments=stockModel.Comments.Select(c=>c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStock(this StockCreateDto stockPostDto)
        {
            return new Stock{
                 CompanyName=stockPostDto.CompanyName,
                 Symbol=stockPostDto.Symbol,
                 Purchase=stockPostDto.Purchase,
                 Industry=stockPostDto.Industry,
                 LastDiv=stockPostDto.LastDiv,
                 MarketCap=stockPostDto.MarketCap,
            };
        }

      
    }
}