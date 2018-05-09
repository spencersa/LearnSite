using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IPostsService
    {
        List<Post> GetPosts();
    }
}
