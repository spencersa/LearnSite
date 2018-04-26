using Dapper;
using MySql.Data.MySqlClient;
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
            var myConnectionString = "Server=localhost;Port=3306;Database=testdb;Uid=LearnSite;Pwd=cfdcada42723;";
            List<TestModel> list = new List<TestModel>();
            using (MySqlConnection db = new MySqlConnection(myConnectionString))
            {
                string query = "Select * From tbl_test";
                list =  db.Query<TestModel>(query, commandType: CommandType.Text).ToList();
            }
        }
    }
}
