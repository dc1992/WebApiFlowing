using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using WebApiFlowing.BusinessLogic.Interfaces;
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

        [Test]
        public void Startup_ShouldHaveIWeightCalculatorRegistered()
        {
            Assert.IsNotNull(_host.Services.GetRequiredService<IWeightTrendCalculator>());
        }

        [Test]
        public void Startup_ShouldHaveIMathHelperRegistered()
        {
            Assert.IsNotNull(_host.Services.GetRequiredService<IMathHelper>());
        }

        [Test]
        public void Startup_ShouldHaveIBodyMassIndexCalculatorRegistered()
        {
            Assert.IsNotNull(_host.Services.GetRequiredService<IBodyMassIndexCalculator>());
        }
    }
}