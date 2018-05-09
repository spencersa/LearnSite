using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Xunit;

namespace DAL.Tests
{
    public class TestModel
    {
        public string idtbl_test { get; set; }
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var myConnectionString = "Server=localhost;Database=TestDb;User Id=LearnSite;Password=3964fd31d461;";
            List<TestModel> list = new List<TestModel>();
            using (SqlConnection db = new SqlConnection(myConnectionString))
            {
                string query = "Select * From testTable";
                list =  db.Query<TestModel>(query, commandType: CommandType.Text).ToList();
            }
        }
    }
}
