using System;

namespace Blog.Domain.Entities
{
    public class AuditLog : Entity
    {
        public string UseCaseName { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public DateTime ExecutedAt { get; set; }
        public string Data { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
