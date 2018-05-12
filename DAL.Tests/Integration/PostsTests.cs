using AutoFixture;
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
        private readonly Fixture _fixture;

        public PostsTests()
        {
            _testRepository = new TestRepository(TestConnectionString);
            _postsRepository = new PostsRepository(TestConnectionString);
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetPosts_ShouldGetPost(int amountOfPosts)
        {
            _testRepository.SeedPosts(amountOfPosts, out List<Post> seededPosts);

            var results = await _postsRepository.GetPosts();

            Assert.Equal(amountOfPosts, results.Count());

            results.Should().BeEquivalentTo(seededPosts, config => 
            {
                config.Excluding(property => property.Id);
                config.Using<DateTime>(context => context.Subject.Should().BeCloseTo(context.Expectation)).WhenTypeIs<DateTime>();
                return config;
            });
        }

        [Fact]
        public async Task GetPosts_WithEmptyDatabase_ShouldNotGetAnyPosts()
        {
            var results = await _postsRepository.GetPosts();
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task InsertPosts_ShouldInsertPost(int amountOfPosts)
        {
            var testPosts = _fixture.CreateMany<Post>(amountOfPosts).ToList();

            var result = await _postsRepository.InsertPosts(testPosts);

            Assert.Equal(amountOfPosts, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task UpdatePosts_ShouldUpdatePosts(int amountOfPosts)
        {
            _testRepository.SeedPosts(amountOfPosts, out _);

            var updatedPosts = new List<Post>();
            for (int i = 0; i < amountOfPosts; i++)
            {
                Post updatedPost = new Post()
                {
                    Id = i + 1,
                    PostDate = DateTime.Now,
                    PostTitle = "TestTitle",
                    OwnerId = 1,
                    Deleted = false
                };
                updatedPosts.Add(updatedPost);
            }

            var result = await _postsRepository.UpdatePosts(updatedPosts);
            Assert.Equal(amountOfPosts, result);

            var posts = await _postsRepository.GetPosts();

            posts.Should().BeEquivalentTo(updatedPosts, config => 
            {
                return config.Using<DateTime>(context => context.Subject.Should().BeCloseTo(context.Expectation)).WhenTypeIs<DateTime>();
            });
        }

        public void Dispose()
        {
            _testRepository.Dispose();
        }
    }
}
