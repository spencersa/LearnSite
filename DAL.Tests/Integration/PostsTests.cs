using DAL.Models;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace DAL.Tests.Integration
{
    public class PostsTests : IntegrationTestBase, IDisposable
    {
        private readonly PostsRepository _postsRepository;
        private readonly TestRepository _testRepository;

        public PostsTests()
        {
            _testRepository = new TestRepository(TestConnectionString);
            _postsRepository = new PostsRepository(TestConnectionString);
        }

        [Fact]
        public async Task GetPosts_ShouldGetPosts()
        {
            List<Post> testPosts = new List<Post>
            {
                new Post
                {
                    PostDate = DateTime.UtcNow,
                    Deleted = false,
                    PostTitle = "Test Title"               
                }
            };
            _testRepository.SeedPosts(testPosts);

            Assert.True((await _postsRepository.GetPosts()).Count() > 0);
        }

        public void Dispose()
        {
            _testRepository.Dispose();
        }
    }
}
