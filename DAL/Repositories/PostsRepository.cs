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

        public async Task<int> UpdatePosts(List<Post> posts) => 
            await CallDatabaseAsync(async(c, t) =>                                                   
                {
                    return (await c.ExecuteAsync(PostsQueries.UpdatePost, posts, transaction: t));
                });

        public async Task<int> InsertPosts(List<Post> posts) => 
            await CallDatabaseAsync(async (c, t) =>                                            
                {
                    return (await c.ExecuteAsync(PostsQueries.InsertPosts, posts, transaction: t));
                });
    }
}
