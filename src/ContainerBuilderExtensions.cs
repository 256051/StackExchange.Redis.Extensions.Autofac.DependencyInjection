using Autofac;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using System;

namespace StackExchange.Redis.Extensions.Autofac.DependencyInjection
{
    public static class ContainerBuilderExtensions
    {
        
        public static ContainerBuilder RegisterStackExchange<T>(this ContainerBuilder builder,
            RedisConfiguration redisConfiguration) where T : class, ISerializer, new()
        {
            return RegisterStackExchangeInternal<T>(builder, redisConfiguration);
        }

        private static ContainerBuilder RegisterStackExchangeInternal<T>(ContainerBuilder builder,
            RedisConfiguration redisConfiguration) where T : class, ISerializer, new()
        {

            var usedConfigExpression = redisConfiguration ?? throw new ArgumentNullException(nameof(redisConfiguration));

            builder.RegisterModule(new StackExchangeRedisModule(usedConfigExpression, typeof(T)));

            return builder;
        }
    }
}
