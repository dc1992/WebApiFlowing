using System;
using System.Collections.Generic;
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

        [Test]
        public void ListContainsExpectedElements_ShouldDoNothing()
        {
            var listObjects = new List<object>
            {
                1
            };
            Assert.DoesNotThrow(() => listObjects.ShouldContainAtLeast(1));
        }

        [Test]
        public void ListDoesNotContainExpectedElemenets_ShouldThrowArgumentNullException()
        {
            var listObjects = new List<object>();
            Assert.Throws<ArgumentOutOfRangeException>(() => listObjects.ShouldContainAtLeast(2));
        }
    }
}