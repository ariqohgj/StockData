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
        public JsonResult GetAll()
        {
            var stock = _context.Stock.ToList().Select(x => x.ToStockDto());

            return new JsonResult(stock);
        }

        [HttpGet]
        public string getalldata()
        {
            return "stockString";
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var stock = _context.Stock.Find(id);

            if(stock == null){
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost("post_data")]
        public IActionResult Create([FromForm] CreateStockRequestDto stockDto)
        {
            
            var stockModel = stockDto.ToStockFromCreateDTO();
            if(stockModel != null)
            {
                _context.Stock.Add(stockModel);
                _context.SaveChanges();
                return Ok("Data successfully added");
            }
            
            return BadRequest("No data");
        }

        [HttpDelete("delete_data")]
        public IActionResult Delete([FromForm]int id)
        {
            var stockDelete = _context.Stock.Single(x => x.Id == id);
            if(stockDelete != null)
            {
                _context.Stock.Remove(stockDelete);
                _context.SaveChanges();
                return Ok("Success Delete");
            }
            
            return BadRequest("No data");
        }

    }
}