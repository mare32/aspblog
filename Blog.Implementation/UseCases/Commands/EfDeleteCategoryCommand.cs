using Blog.Application.Exceptions;
using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfDeleteCategoryCommand : EfUseCase, IDeleteCategoryCommand
    {
        public EfDeleteCategoryCommand(BlogContext context) : base(context)
        {
        }

        public int Id => 2004;

        public string Name => "Delete category";

        public string Description => "Delete category if its not connected to any blog posts using EF";

        public void Execute(int id)
        {
            var category = Context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                throw new EntityNotFoundException(nameof(Category), id);
            }
            if( category.BlogPostCategories.Any())
            {
                throw new ValidationConflictException("Neke objave su povezane sa ovom kategorijom, nije je moguce obrisati.");
            }
            Context.Categories.Remove(category);
            Context.SaveChanges();
        }
    }
}
