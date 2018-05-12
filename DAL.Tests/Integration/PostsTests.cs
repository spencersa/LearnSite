using DAL.Models;
using DAL.Services;
using FluentAssertions;
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

            Post testPost = new Post
            {
                Id = 1,
                PostDate = DateTime.UtcNow.Date,
                Deleted = false,
                PostTitle = "Test Title"
            };

            List<Post> testPosts = new List<Post> { testPost };
            _testRepository.SeedPosts(testPosts);

            var results = await _postsRepository.GetPosts();

            Assert.Single(results);
            results.Should().BeEquivalentTo(testPosts);
        }

        [Fact]
        public async Task InsertPosts_ShouldInsertPosts()
        {
            Post testPost = new Post
            {
                Id = 1,
                PostDate = DateTime.UtcNow.Date,
                Deleted = false,
                PostTitle = "Test Title"
            };

            List<Post> testPosts = new List<Post> { testPost };

            var result = await _postsRepository.InsertPosts(testPosts);

            Assert.Equal(1, result);
        }

        public void Dispose()
        {
            _testRepository.Dispose();
        }
    }
}
