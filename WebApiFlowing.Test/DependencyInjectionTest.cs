using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using WebApiFlowing.Data.Interfaces;

namespace WebApiFlowing.Test
{
    [TestFixture]
    public class DependencyInjectionTest
    {
        private IHost _host;

        [SetUp]
        public void SetUp()
        {
            _host = Program
                .CreateHostBuilder(null)
                .Build();

            Environment.SetEnvironmentVariable("CONNECTION_STRING", "testConnection");
        }

        [Test]
        public void Startup_ShouldHaveIDataContextRegistered()
        {
            Assert.IsNotNull(_host.Services.GetRequiredService<IDataContext>());
        }

        [Test]
        public void Startup_ShouldHaveIUserRepositoryRegistered()
        {
            Assert.IsNotNull(_host.Services.GetRequiredService<IUserRepository>());
        }
    }
}