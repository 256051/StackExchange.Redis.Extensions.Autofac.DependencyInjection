using Autofac;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;
using System;
using Module = Autofac.Module;

namespace StackExchange.Redis.Extensions.Autofac.DependencyInjection
{
    internal class StackExchangeRedisModule : Module
    {
        private readonly RedisConfiguration _redisConfiguration;
        private readonly Type _serializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configAction"></param>
        /// <param name="serializer"></param>
        public StackExchangeRedisModule(RedisConfiguration configAction, Type serializer)
        {
            this._redisConfiguration = configAction ??
                                 throw new ArgumentNullException(nameof(configAction));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RedisClientFactory>().As<IRedisClientFactory>().SingleInstance();
            builder.Register(c => _redisConfiguration).As<RedisConfiguration>().SingleInstance();

            builder.RegisterType(_serializer).As<ISerializer>().SingleInstance();


            builder.Register(c => c.Resolve<IRedisClientFactory>().GetDefaultRedisClient())
                .As<IRedisClient>().AsImplementedInterfaces().SingleInstance();

            builder.Register(c =>
                    c.Resolve<IRedisClientFactory>().GetDefaultRedisClient().GetDefaultDatabase())
                .As<IRedisDatabase>().AsImplementedInterfaces().SingleInstance();
        }


       
    }
}
