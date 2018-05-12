using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Dapper;
using Database.Queries;

namespace DAL.Services
{
    public class PostsRepository : BaseRepository, IPostsRepository
    {
        public PostsRepository(string connectionString) 
            : base(connectionString) { }

        public async Task<List<Post>> GetPosts() => 
            await CallDatabaseAsync(async c =>
                {
                    return (await c.QueryAsync<Post>(PostsQueries.GetPosts, commandType: CommandType.Text)).ToList();
                });

        public async Task InsertPosts(List<Post> posts)
        {
             await CallDatabaseAsync(async (c, t) =>
             {
                 await c.ExecuteAsync(PostsQueries.InsertPosts, posts, transaction: t);
                 return Task.CompletedTask;
             });
        }
    }
}
