
namespace Database.TestQueries
{
    public static class TestQueries
    {
        public const string CreateAllTables =
            @"
              IF OBJECT_ID('dbo.tblPosts', 'U') IS NOT NULL 
                DROP TABLE dbo.tblPosts; 

              CREATE TABLE [dbo].[tblPosts] (
	          Id INT NOT NULL IDENTITY PRIMARY KEY,
	          PostTitle VARCHAR(255) NOT NULL,
	          PostDate DATETIME NOT NULL,
	          Deleted BIT NOT NULL,
	          OwnerID int)";

        public const string DropAllTables =
            @"DROP TABLE [dbo].[tblPosts];";
    }
}
