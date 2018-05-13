using DAL.Configurations;
using Microsoft.Extensions.Options;

namespace DAL.Tests.Integration
{
    public class IntegrationTestBase
    {
        public IOptions<ConnectionConfiguration> TestConnectionConfiguration { get; } =
            Options.Create(new ConnectionConfiguration { LearnSiteConnection = "Server=localhost;Database=TestDb;User Id=LearnSite;Password=3964fd31d461;" });
    }
}
