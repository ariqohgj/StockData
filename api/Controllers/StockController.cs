using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepo = stockRepository;
        }
        [HttpGet("get_all")]
        public async Task<IActionResult> GetAll()
        {
            var stock = await _stockRepo.GetAllAsync();
            var stockDto = stock.Select(x => x.ToStockDto());

            if(stockDto is null){
                return NotFound();
            }
            return new JsonResult(stockDto);
        }

        [HttpGet]
        public string getalldata()
        {
           
            return  @"""Hey Whats up, what are you looking for
            please try add 
            /get_data
            /get_all
            /post_data
            /delete_data"""; 
            
        }

        [HttpGet("get_data")]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            var stock = await _stockRepo.FindByIdAsync(id);

            if(stock == null){
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        /// <summary>
        /// Post data from stockDto data model
        /// </summary>
        /// <param name="stockDto"></param>
        /// <returns></returns>
        [HttpPost("post_data")]
        public async Task<IActionResult> Create([FromForm] CreateStockRequestDto stockDto)
        {
            
            var stockModel = stockDto.ToStockFromCreateDTO();
            if(stockModel == null)
            {
                return BadRequest("No data");

            }
            
            await _stockRepo.AddStockAsync(stockModel);
            await _context.SaveChangesAsync();
            return Ok("Data successfully added");

        }

        /// <summary>
        /// Delete data by {StockId}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_data")]
        public async Task<IActionResult> Delete([FromForm]int id)
        {
            var stockDelete = await _stockRepo.FindSingleAsync(id);
            if(stockDelete == null)
            {
                return BadRequest("No data");

            }

            _context.Stock.Remove(stockDelete);
            await _context.SaveChangesAsync();
            return Ok("Success Delete");
        }

        [HttpPut("update_data")]
        public async Task<IActionResult> Update([FromForm]int id, [FromForm] UpdateStockRequestDto updateStockDto)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if(stockModel == null)
            {
                return NotFound();
            }
            
            stockModel.Symbol = updateStockDto.Symbol;
            stockModel.Company = updateStockDto.Company;
            stockModel.Purchase = updateStockDto.Purchase;
            stockModel.LastDiv = updateStockDto.LastDiv;
            stockModel.Industry = updateStockDto.Industry;
            stockModel.MarketCap = updateStockDto.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());
        }
    }
}