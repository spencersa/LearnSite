using AutoFixture;
using DAL.Models;
using DAL.Services;
using Dapper;
using Database.Queries;
using Database.TestQueries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL.Tests.Integration
{
    public class TestService : BaseRepository, IDisposable
    {
        private readonly Fixture _fixture;

        public TestService(string connectionString)
            : base(connectionString)
        {
            SetupDatabase();
            _fixture = new Fixture();
        }

        public void SetupDatabase()
        {
            Query(c =>
            {
                return c.Execute(TestQueries.CreateAllTables, commandType: CommandType.Text);
            });
        }

        public void SeedPosts(int amount, out List<Post> seededPosts)
        {
            var posts = _fixture.CreateMany<Post>(amount).ToList();
            seededPosts = posts;
            Upsert((c, t) =>
            {
                return c.Execute(PostsQueries.InsertPosts, posts, transaction: t);
            });
        }

        public void CleanUpDatabase()
        {
            Query(c =>
            {
                return c.Execute(TestQueries.DropAllTables, commandType: CommandType.Text);
            });
        }

        public void Dispose()
        {
            CleanUpDatabase();
        }
    }
}
