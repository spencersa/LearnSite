using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPostsRepository
    {
        Task<List<Post>> GetPosts();
        Task InsertPosts(List<Post> posts);
    }
}
