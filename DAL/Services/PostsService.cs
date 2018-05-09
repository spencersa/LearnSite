using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DAL.Interfaces;
using DAL.Models;
using Dapper;
using Database.Queries;

namespace DAL.Services
{
    public class PostsService : IPostsService, IDisposable
    {
        private SqlConnection _dbConnection;

        //TODO make this configuration variable
        public PostsService(string connectionString)
        {
            _dbConnection = new SqlConnection(connectionString);
        }

        public List<Post> GetPosts() => 
            _dbConnection.Query<Post>(PostQueries.GetPosts, commandType: CommandType.Text).ToList();

        public void Dispose()
        {
            _dbConnection.Dispose();
        }
    }
}
