﻿using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPostsService
    {
        Task<List<Post>> GetMostRecentPosts(int amount);
        Task<List<Post>> GetPosts(IEnumerable<int> ids);
        Task<int> InsertPosts(List<Post> posts);
        Task<int> UpdatePosts(List<Post> post);
    }
}
