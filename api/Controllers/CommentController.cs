using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepo = commentRepository;
        }

        [HttpGet("get_all_comment")]
        public async Task<ActionResult> GetAll()
        {
            var comment = await _commentRepo.GetAllAsync();
            
            if(comment == null)
            {
                return NotFound();
            }

            var commentDto = comment.Select(s => s.ToCommentDto());

            return Ok(commentDto);
        }
    }
}