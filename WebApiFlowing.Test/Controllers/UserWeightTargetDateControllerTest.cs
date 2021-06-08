using System;
using System.Threading.Tasks;
using NUnit.Framework;
using WebApiFlowing.Controllers;

namespace WebApiFlowing.Test.Controllers
{
    [TestFixture]
    public class UserWeightTargetDateControllerTest
    {
        private UserWeightTargetDateController _controller;
        private Guid _defaultUserGuid;

        [SetUp]
        public void Setup()
        {
            _controller = new UserWeightTargetDateController();
            _defaultUserGuid = Guid.Parse("ae277024-e1a8-4e0b-a188-9ed15ab8ba71");
        }

        [Test]
        public async Task Get_ShouldReturnDateTimeOffset()
        {
            var response = await _controller.Get(_defaultUserGuid);

            Assert.IsTrue(response is DateTimeOffset);
        }
    }
}