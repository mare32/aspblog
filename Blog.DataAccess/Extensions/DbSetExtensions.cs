using Blog.DataAccess.Exceptions;
using Blog.Domain;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Extensions
{
    public static class DbSetExtensions
    {
        public static void DeactivateOneUser(this DbContext context, User user)
        {
            user.Active = 0;
            context.Entry(user).State = EntityState.Modified;
        }


        public static void DeactivateManyUsers<T>(this DbContext context, IEnumerable<int> ids)
            where T : User
        {
            var usersToDeactivate = context.Set<T>().Where(x => ids.Contains(x.Id));

            foreach(var d in usersToDeactivate)
            {
                d.Active = 0;
            }

        }
    }
}
