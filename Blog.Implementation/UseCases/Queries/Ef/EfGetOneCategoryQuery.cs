using Blog.Application.Exceptions;
using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetOneCategoryQuery : EfUseCase, IGetOneCategoryQuery
    {
        public EfGetOneCategoryQuery(BlogContext context) : base(context)
        {
        }

        public int Id => 2028;

        public string Name => "Show category";

        public string Description => "Show category by id using EF";

        public CategoryDto Execute(int id)
        {
            var category = Context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
                throw new EntityNotFoundException(nameof(Category), id);
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
