using System;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WebApiFlowing.BusinessLogic;
using WebApiFlowing.BusinessLogic.Interfaces;
using WebApiFlowing.Data;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.Data.Repositories;

namespace WebApiFlowing.IntegrationTest
{
    public class BaseTest
    {
        protected IUserRepository _userRepository;
        protected IDataContext _fakeDataContext;
        protected DbContextOptions<WebApiFlowingDataContext> _fakeDbContextOptions;
        protected Guid _defaultUserGuid;
        protected IWeightCalculator _weightCalculator;
        protected IMathHelper _mathHelper;

        [SetUp]
        public void SetUp()
        {
            _fakeDbContextOptions = new DbContextOptionsBuilder<WebApiFlowingDataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _fakeDataContext = new WebApiFlowingDataContext(_fakeDbContextOptions);

            _userRepository = new UserRepository(_fakeDataContext);

            _mathHelper = new MathHelper();

            _weightCalculator = new WeightCalculator(_mathHelper);
        }
    }
}