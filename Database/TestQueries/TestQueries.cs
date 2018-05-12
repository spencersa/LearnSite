using System;
using System.Collections.Generic;
using System.Text;

namespace Database.TestQueries
{
    public static class TestQueries
    {
        public const string CreateAllTables =
            @"CREATE TABLE [dbo].[tblPosts] (
	          Id INT NOT NULL IDENTITY PRIMARY KEY,
	          PostTitle VARCHAR(255) NOT NULL,
	          PostDate DATETIME NOT NULL,
	          Deleted BIT NOT NULL,
	          OwnerID int)";

        public const string DropAllTables =
            @"DROP TABLE [dbo].[tblPosts];";
    }
}
