using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Queries
{
    public static class PostQueries
    {
        public static string GetPosts => 
        @"
    SELECT [Id]
          ,[PostTitle]
          ,[PostDate]
          ,[Deleted]
          ,[OwnerID]
    FROM[TestDb].[dbo].[tblPosts]";
    }
}
