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
    public class PostsRepository : BaseRepository, IPostsService
    {
        public PostsRepository(string connectionString) 
            : base(connectionString) { }

        public async Task<List<Post>> GetPosts() => 
            await CallDatabaseAsync(async c =>
                {
                    return (await c.QueryAsync<Post>(PostQueries.GetPosts, commandType: CommandType.Text)).ToList();
                });
    }
}
