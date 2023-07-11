using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsTests.ControllerTests
{
    [TestFixture]
    public class ControllerTestBase
    {
        private TestServer _testServer;
        protected IServiceProvider Services => _testServer.Services;
        protected HttpClient HttpClient;

        [SetUp]
        public void Setup()
        {
            var app = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder =>
               {
                   builder.ConfigureServices(services =>
                   {

                   })
                   .ConfigureAppConfiguration(configurationBuilder =>
                   {
                       configurationBuilder.AddInMemoryCollection(CreateInMemoryConfiguration());
                   })
                   .ConfigureTestServices(OverrideServices);

                   builder.UseEnvironment("Testing");
               });
            _testServer = app.Server;
            HttpClient = _testServer.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _testServer?.Dispose();
        }

        protected virtual void OverrideServices(IServiceCollection services)
        {
            
        }

        protected virtual Dictionary<string, string?> CreateInMemoryConfiguration()
        {
            return new Dictionary<string, string?>();
        }
    }

    
}
