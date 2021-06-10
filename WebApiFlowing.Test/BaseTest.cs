using System;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WebApiFlowing.BusinessLogic.Interfaces;
using WebApiFlowing.Data;
using WebApiFlowing.Data.Interfaces;

namespace WebApiFlowing.Test
{
    public class BaseTest
    {
        protected IUserRepository _userRepository;
        protected IDataContext _dataContext;
        protected DbContextOptions<WebApiFlowingDataContext> _options;
        protected Guid _defaultUserGuid;
        protected IWeightCalculator _weightCalculator;
        protected IMathHelper _mathHelper;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            
            _options = new DbContextOptionsBuilder<WebApiFlowingDataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dataContext = new WebApiFlowingDataContext(_options);

            _defaultUserGuid = Guid.Parse("ae277024-e1a8-4e0b-a188-9ed15ab8ba71");

            _weightCalculator = A.Fake<IWeightCalculator>();

            _mathHelper = A.Fake<IMathHelper>();
        }
    }
}