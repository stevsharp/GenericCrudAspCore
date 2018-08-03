using System.Collections.Generic;

namespace WebApiDemo.Models
{
    public class Blog : EntityBase
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
