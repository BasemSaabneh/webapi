using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interface;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [Route("api/stock")]
    [ApiController]
    public class StockController:ControllerBase
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo=stockRepo; 
        }

        [HttpGet]
        [Authorize]
        public async  Task<IActionResult> GetAll([FromQuery] QueryObject queryObject){
            var stocks = await _stockRepo.GetAllAsync(queryObject);
            var stockDto = stocks.Select(s=>s.ToStockDTO());
            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public  async  Task<IActionResult> GetById([FromRoute] int id){
            var stock =await _stockRepo.GetByIdAsync(id);
            if (stock == null)
                return NotFound();
            return Ok(stock.ToStockDTO());
        }

    [HttpPost]
    public  async  Task<IActionResult> Create([FromBody] StockCreateDto stockCreateDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var stockModel =  stockCreateDto.ToStock();
        await _stockRepo.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById),new {id= stockModel.Id},stockModel.ToStockDTO());
    }

    [HttpPut]
    [Route("{id:int}")]
    public  async  Task<IActionResult> Update([FromRoute] int id,[FromBody] StockUpdateDto stockUpdateDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var stockModel =await _stockRepo.UpdateAsync(id,stockUpdateDto);
        if (stockModel == null)
        {
            return NotFound();
        }
        return Ok(stockModel.ToStockDTO());
    
    }

    [HttpDelete]
    [Route("{id:int}")]
    public  async  Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock =await  _stockRepo.DeleteAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    }
}