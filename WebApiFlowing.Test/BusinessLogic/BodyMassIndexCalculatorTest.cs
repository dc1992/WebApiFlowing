using System;
using System.Threading.Tasks;
using NUnit.Framework;
using WebApiFlowing.BusinessLogic;
using WebApiFlowing.Data;
using WebApiFlowing.Data.Models;

namespace WebApiFlowing.Test.BusinessLogic
{
    [TestFixture]
    public class BodyMassIndexCalculatorTest : BaseTest
    {
        private BodyMassIndexCalculator _bodyMassIndexCalculator;

        [SetUp]
        public void SetUp()
        {
            _bodyMassIndexCalculator = new BodyMassIndexCalculator(_dataContext);
        }

        [Test]
        public void GetIdealWeightRange_NotExistingRange_ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => 
                await _bodyMassIndexCalculator.GetIdealWeightRange(-1));
        }

        [TestCase(2, 40, 80)]
        [TestCase(3, 90, 180)]
        public async Task GetIdealWeightRange_ExistingRange_ShouldReturnExpectedRange(double height, double expectedMin, double expectedMax)
        {
            //setup
            await using (var setupContext = new WebApiFlowingDataContext(_options))
            {
                await setupContext.BodyMassIndexRanges.AddAsync(new BodyMassIndexRange
                {
                    MinimumBMI = 0,
                    MaximumBMI = 10,
                    Description = "test",
                    IsIdeal = false
                });
                await setupContext.BodyMassIndexRanges.AddAsync(new BodyMassIndexRange
                {
                    MinimumBMI = 10,
                    MaximumBMI = 20,
                    Description = "test",
                    IsIdeal = true
                });
                await setupContext.BodyMassIndexRanges.AddAsync(new BodyMassIndexRange
                {
                    MinimumBMI = 20,
                    MaximumBMI = double.MaxValue,
                    Description = "test",
                    IsIdeal = false
                });

                await setupContext.SaveChangesAsync();
            }
            
            //test
            var idealRange = await _bodyMassIndexCalculator.GetIdealWeightRange(height);

            //assert
            Assert.AreEqual(expectedMin, idealRange.MinimumIdealWeightInKgs);
            Assert.AreEqual(expectedMax, idealRange.MaximumIdealWeightInKgs);
        }
    }
}