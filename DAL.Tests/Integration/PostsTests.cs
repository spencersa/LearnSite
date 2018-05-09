using DAL.Models;
using DAL.Services;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Xunit;


namespace DAL.Tests.Integration
{
    public class PostsTests : IntegrationTestBase, IDisposable
    {
        private PostsService _service;

        public PostsTests()
        {
            _service = new PostsService(TestConnectionString);
        }

        [Fact]
        public void GetPosts_ShouldGetPosts()
        {
            Assert.True(_service.GetPosts().Count() > 1);
        }

        public void Dispose()
        {
            _service.Dispose();
        }
    }
}
