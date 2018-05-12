using AutoFixture;
using DAL.Models;
using DAL.Services;
using FluentAssertions;
using System;
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
            var testPosts = _fixture.CreateMany<Post>(amountOfPosts).ToList();
            _testRepository.SeedPosts(testPosts);

            var results = await _postsRepository.GetPosts();

            Assert.Equal(amountOfPosts, results.Count());

            results.Should().BeEquivalentTo(testPosts, config => 
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

        public void Dispose()
        {
            _testRepository.Dispose();
        }
    }
}
