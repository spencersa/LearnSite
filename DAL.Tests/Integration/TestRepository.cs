using DAL.Models;
using DAL.Services;
using Dapper;
using Database.Queries;
using Database.TestQueries;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL.Tests.Integration
{
    public class TestRepository : BaseRepository, IDisposable
    {
        public TestRepository(string connectionString)
            : base(connectionString)
        {
            SetupDatabase();
        }

        public void SetupDatabase()
        {
            CallDatabase(c =>
            {
                return c.Execute(TestQueries.CreateAllTables, commandType: CommandType.Text);
            });
        }

        public void SeedPosts(List<Post> posts)
        {
            CallDatabase((c, t) =>
            {
                return c.Execute(PostsQueries.InsertPosts, posts, transaction: t);
            });
        }

        public void CleanUpDatabase()
        {
            CallDatabase(c =>
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
