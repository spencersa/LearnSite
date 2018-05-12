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
        private readonly PostsService _postsRepository;
        private readonly TestService _testRepository;
        private readonly Fixture _fixture;

        public PostsTests()
        {
            _testRepository = new TestService(TestConnectionString);
            _postsRepository = new PostsService(TestConnectionString);
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetPosts_ShouldGetPosts(int amountOfPosts)
        {
            var ids = new List<int>();
            for (int i = 0; i < amountOfPosts + 1; i++)
            {
                ids.Add(i + 1);
            }

            _testRepository.SeedPosts(amountOfPosts, out List<Post> seededPosts);

            var results = await _postsRepository.GetPosts(ids);

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
            var ids = new List<int>();
            var results = await _postsRepository.GetPosts(ids);
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

            var posts = await _postsRepository.GetPosts(updatedPosts.Select(x => x.Id).ToList());

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
