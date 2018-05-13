using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DAL.Configurations;
using DAL.Interfaces;
using DAL.Models;
using Dapper;
using Database.Queries;
using Microsoft.Extensions.Options;

namespace DAL.Services
{
    public class PostsService : BaseRepository, IPostsService
    {
        public PostsService(IOptions<ConnectionConfiguration> connectionConfiguration) 
            : base(connectionConfiguration.Value.LearnSiteConnection) { }

        public async Task<List<Post>> GetPosts(IEnumerable<int> ids) => 
            await GetAsync(async c =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ids", ids);
                    return (await c.QueryAsync<Post>(PostsQueries.GetPostsById, parameters, commandType: CommandType.Text)).ToList();
                });

        public async Task<List<Post>> GetMostRecentPosts(int amount) => 
            await QueryAsync(async c =>                                                                   
            {
                var parameters = new DynamicParameters();
                parameters.Add("@amount", amount);
                return (await c.QueryAsync<Post>(PostsQueries.GetTopPosts, parameters, commandType: CommandType.Text)).ToList();
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
