using Blog.Api.Core.DTO;
using Blog.Application.UseCases;
using System.Collections.Generic;

namespace Blog.Api.Core.ImageHelpers
{
    public interface IAddImageToBlogPostCommand : ICommand<ImageDto>
    {
    }
}
