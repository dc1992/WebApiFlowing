using FakeItEasy;
using NUnit.Framework;
using WebApiFlowing.Data.Interfaces;

namespace WebApiFlowing.Test
{
    public class BaseTest
    {
        protected IUserRepository _userRepository;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
        }
    }
}