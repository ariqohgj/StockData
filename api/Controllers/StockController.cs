using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet("get_all")]
        public IActionResult GetAll()
        {
            var stock = _context.Stock.ToList().Select(x => x.ToStockDto());

            if(stock is null){
                return NotFound();
            }
            return new JsonResult(stock);
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
        public IActionResult GetById([FromForm]int id)
        {
            var stock = _context.Stock.Find(id);

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
        public IActionResult Create([FromForm] CreateStockRequestDto stockDto)
        {
            
            var stockModel = stockDto.ToStockFromCreateDTO();
            if(stockModel == null)
            {
                return BadRequest("No data");

            }
            
            _context.Stock.Add(stockModel);
            _context.SaveChanges();
            return Ok("Data successfully added");

        }

        /// <summary>
        /// Delete data by {StockId}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_data")]
        public IActionResult Delete([FromForm]int id)
        {
            var stockDelete = _context.Stock.Single(x => x.Id == id);
            if(stockDelete == null)
            {
                return BadRequest("No data");

            }

            _context.Stock.Remove(stockDelete);
            _context.SaveChanges();
            return Ok("Success Delete");
        }
    }
}