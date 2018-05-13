using System;

namespace DAL.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public DateTime PostDate { get; set; }
        public bool Deleted { get; set; }
        public int OwnerId { get; set; }
    }
}
