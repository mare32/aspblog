using Blog.Domain;
using System.Collections.Generic;

namespace Blog.Api.Core
{
    public class JwtUser : IApplicationUser
    {
        public string Identity { get; set; }

        public int Id { get; set; }
        public IEnumerable<int> UseCaseIds { get; set; } = new List<int>();
        public string Email { get; set; }
    }

    public class AnonimousUser : IApplicationUser
    {
        public string Identity => "Anonymous";

        public int Id => 0;

        public IEnumerable<int> UseCaseIds => new List<int> { 1002, 2006, 2007, 2017, 1, 2029 }; 
        //  1002 je registracija
        //  2006 pretraga objava
        //  2007 jedna objava
        //  2017 pretraga komentara
        //  1 pretraga kategorija
        public string Email => "anonimous@blog-api.com";
    }
}
