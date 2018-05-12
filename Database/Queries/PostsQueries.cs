using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Queries
{
    public static class PostsQueries
    {
        public const string GetPosts =
            @"SELECT [Id]
                  ,[PostTitle]
                  ,[PostDate]
                  ,[Deleted]
                  ,[OwnerID]
            FROM[TestDb].[dbo].[tblPosts]";

        public const string InsertPosts =
            @"INSERT tblPosts(PostTitle, PostDate, Deleted, OwnerID) 
              VALUES (@PostTitle, @PostDate, @Deleted, @OwnerID)";
    }
}
