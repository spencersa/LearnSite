using DAL.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace DAL.Tests.Integration
{
    public class PostsTests : IntegrationTestBase
    {
        private readonly PostsRepository _repository;

        public PostsTests()
        {
            _repository = new PostsRepository(TestConnectionString);
        }

        [Fact]
        public async Task GetPosts_ShouldGetPosts()
        {
            Assert.True((await _repository.GetPosts()).Count() > 0);
        }
    }
}
