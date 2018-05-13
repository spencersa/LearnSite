using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Dapper;
using Database.Queries;

namespace DAL.Services
{
    public class PostsService : BaseRepository, IPostsService
    {
        public PostsService(string connectionString) 
            : base(connectionString) { }

        public async Task<List<Post>> GetPosts(IEnumerable<int> ids) => 
            await QueryAsync(async c =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ids", ids);
                    return (await c.QueryAsync<Post>(PostsQueries.GetPosts, parameters, commandType: CommandType.Text)).ToList();
                });

        public async Task<int> UpdatePosts(List<Post> posts) => 
            await UpsertAsync(async(c, t) =>                                                   
                {
                    return (await c.ExecuteAsync(PostsQueries.UpdatePost, posts, transaction: t));
                });

        public async Task<int> InsertPosts(List<Post> posts) => 
            await UpsertAsync(async (c, t) =>                                            
                {
                    return (await c.ExecuteAsync(PostsQueries.InsertPosts, posts, transaction: t));
                });
    }
}
