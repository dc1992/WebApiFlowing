using System;
using NUnit.Framework;
using WebApiFlowing.BusinessLogic.Extensions;

namespace WebApiFlowing.Test.BusinessLogic
{
    [TestFixture]
    public class GenericExtensionsTest
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase("prova")]
        [TestCase(true)]
        public void NotNullObject_ShouldDoNothing<T>(T source)
        {
            Assert.DoesNotThrow(() => source.ShouldNotBeNull());
        }

        [Test]
        public void NullObject_ShouldThrowArgumentNullException()
        {
            object nullObject = null;
            Assert.Throws<ArgumentNullException>(() => nullObject.ShouldNotBeNull());
        }
    }
}