using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnSite.Controllers.Api
{
    public class PostsController : BaseApiController
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet("{amount}", Name = "getMostRecentPosts")]
        public async Task<List<Post>> GetMostRecentPosts(int amount)
        {
            return await _postsService.GetMostRecentPosts(amount);
        }
        
        // POST: api/Posts
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
    }
}
