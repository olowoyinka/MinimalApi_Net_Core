using Domain.Models;
using MediatR;

namespace Application.Posts.Commands;

public class UpdatePost : IRequest<Post>
{
    public int PostId { get; set; }
    public string? UpdateContent { get; set; }
}