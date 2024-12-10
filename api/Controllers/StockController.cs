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
            var stock = await _stockRepo.GetByIdAsync(id);

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
            
            await _stockRepo.CreateAsync(stockModel);
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
            var stockDelete = await _stockRepo.DeleteAsync(id);
            if(stockDelete == null)
            {
                return BadRequest("No data");

            }
            
            return Ok("Success Delete");
        }
        /// <summary>
        /// Update data stock 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateStockDto"></param>
        /// <returns></returns>
        [HttpPut("update_data")]
        public async Task<IActionResult> Update([FromForm]int id, [FromForm] UpdateStockRequestDto updateStockDto)
        {
            var stockModel = await _stockRepo.UpdateAsync(id, updateStockDto);

            if(stockModel == null)
            {
                return NotFound();
            }

            return Ok("Success Update Data");
        }
    }
}