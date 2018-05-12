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
            FROM [dbo].[tblPosts]";

        public const string UpdatePost =
            @"UPDATE [dbo].[tblPosts]
                SET PostTitle = @PostTitle
                   ,PostDate = @PostDate
                   ,Deleted = @Deleted
                   ,OwnerID = @OwnerID
                WHERE Id = @Id";

        public const string InsertPosts =
            @"INSERT [dbo].[tblPosts] (PostTitle, PostDate, Deleted, OwnerID) 
              VALUES (@PostTitle, @PostDate, @Deleted, @OwnerID)";
    }
}
