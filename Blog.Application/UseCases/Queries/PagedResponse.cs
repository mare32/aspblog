using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Queries
{
    public class PagedResponse<T> where T : class
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int PagesCount => (int)Math.Ceiling((float)TotalCount / ItemsPerPage);
        public IEnumerable<T> Data { get; set; }
    }

    //Ukupno 58
    //Po strani 10
    //Broj strana: 6
    // 58/10 => 5

    // (float)58/10 => 5.8
    // (float)53/10 => 5.3
    // (float)50/10 => 5
}
