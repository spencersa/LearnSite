
namespace DAL.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public string PostDate { get; set; }
        public bool Deleted { get; set; }
    }
}
