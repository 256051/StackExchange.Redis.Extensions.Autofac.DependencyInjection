using Autofac;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;
using System.Threading.Tasks;
using Xunit;
namespace StackExchange.Redis.Extensions.Autofac.DependencyInjection.Tests
{
    public class ContainerBuilderExtensionTests
    {

        [Fact]
        public async Task ContainerBuilderExtensions_Registered()
        {
            var conf = new RedisConfiguration()
            {
                Hosts = new[]
                {
                    new RedisHost()
                    {
                        Host = "127.0.0.1",
                        Port = 6379
                    },
                },
                Password = "",
                KeyPrefix = "",
                Database = 4
            };
            var builder = new ContainerBuilder();
            builder.RegisterStackExchange<SystemTextJsonSerializer>(conf);
            
            var container = builder.Build();

            var redis = container.Resolve<IRedisDatabase>();
            var isSave= await redis.AddAsync("Test", "test");
            Assert.True(isSave);

            var test = await redis.GetAsync<string>("Test");
            Assert.Equal("test", test);

        }
    }
}
