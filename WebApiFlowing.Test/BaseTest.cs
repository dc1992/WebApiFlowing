using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        protected IWeightTrendCalculator _weightCalculator;
        protected IMathHelper _mathHelper;
        protected IBodyMassIndexCalculator _bodyMassIndexCalculator;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            
            _options = new DbContextOptionsBuilder<WebApiFlowingDataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dataContext = new WebApiFlowingDataContext(_options);

            _defaultUserGuid = Guid.Parse("ae277024-e1a8-4e0b-a188-9ed15ab8ba71");

            _weightCalculator = A.Fake<IWeightTrendCalculator>();

            _mathHelper = A.Fake<IMathHelper>();

            _bodyMassIndexCalculator = A.Fake<IBodyMassIndexCalculator>();
        }

        protected void Validate(object toValidate)
        {
            if (toValidate == null)
            {
                throw new ArgumentNullException("model");
            }

            var context = new ValidationContext(toValidate);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(toValidate, context, validationResults, true);

            if (!isValid)
            {
                var exception = new ValidationException($"[{toValidate.GetType().FullName}] is not valid.");

                // add information for logging purpose
                exception.Data.Add("Exception Detail",
                    validationResults
                        .Select(s => new { PropertyName = string.Join(",", s.MemberNames), Message = s.ErrorMessage })
                        .ToList());

                throw exception;
            }
        }
    }
}