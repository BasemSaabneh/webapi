using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Comment;
using api.Interface;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController:ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo,IStockRepository stockRepo)
        {
         _commentRepo=commentRepo;   
         _stockRepo=stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var comments= await _commentRepo.GetAllAsync();
            var commetsDto = comments.Select(x=>x.ToCommentDto());
            return Ok(commetsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var comment = await _commentRepo.GetByIdAsync(id);
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,CommentCreateDto commentCreateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _stockRepo.StockExists(stockId))
                return BadRequest("Stock does not exists");
            
            var commentModel = commentCreateDto.ToComment(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById),new {id= commentModel.Id},commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] CommentUpdateDto updateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _commentRepo.UpdateAsync(id,updateDto.ToCommentFromUpdate());
            if(comment == null)
                return NotFound("Comment Not Found");
            
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel =await _commentRepo.DeleteAsync(id);
            if(commentModel == null)
                return NotFound("Comment does not exist");
            
            return Ok(commentModel);

        }
    }//class
}