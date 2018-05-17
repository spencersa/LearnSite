using AutoFixture;
using DAL.Interfaces;
using DAL.Models;
using LearnSite.Controllers.Api;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LearnSite.Test.ControllerTests
{
    public class PostsControllerTests
    {
        private readonly Mock<IPostsService> _mockPostsService;
        private readonly Fixture _fixture;

        public PostsControllerTests()
        {
            _mockPostsService = new Mock<IPostsService>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetMostRecentPosts_ShouldGetMostRecentPosts()
        {
            var amount = _fixture.Create<int>();
            _mockPostsService.Setup(x => x.GetMostRecentPosts(amount)).Returns(Task.FromResult(GetTestPosts(amount)));
            var postsController = new PostsController(_mockPostsService.Object);

            var result = await postsController.GetMostRecentPosts(amount);

            Assert.Equal(amount, result.Count());
        }

        private List<Post> GetTestPosts(int amount)
        {
            return _fixture.CreateMany<Post>(amount).ToList();
        }
    }
}
