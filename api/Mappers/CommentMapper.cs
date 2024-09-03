using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Model;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto{
                Id=commentModel.Id,
                Content=commentModel.Content,
                CreatedOn=commentModel.CreatedOn,
                StockId=commentModel.StockId,
                Title=commentModel.Title
            };
        }

        public static Comment ToComment (this CommentCreateDto commentDto,int stockId)
        {
            return new Comment{
                StockId=stockId,
                Content=commentDto.Content,
                Title=commentDto.Title,
                
            };
        }

        public static Comment ToCommentFromUpdate (this CommentUpdateDto commentDto)
        {
            return new Comment{
                Content=commentDto.Content,
                Title=commentDto.Title,
                
            };
        }

    }
}