using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Services.Actions.Initiative;

namespace Sau.Raylan.SR5.Services.Tests.Actions.Initiative
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InitiativeActionFactoryTests
    {
        [TestClass]
        public class Create
        {
            [TestMethod]
            [ExpectedException(typeof(NotImplementedException))]
            public void GivenActionTypeNotInDictionary_ThenThrowNotImplementedException()
            {
                // arrange
                var actual = new InitiativeActionFactory();

                // act
                var results = actual.Create(InitiativeActionType.None);

                // assert
                Assert.Fail("NotImplementedException should have been thrown.");
            }

            [TestMethod]
            public void GivenActionTypeIsBlock_ThenCreateReturnsBlockAction()
            {
                // arrange
                var actual = new InitiativeActionFactory();

                // act
                var results = actual.Create(InitiativeActionType.Block);

                // assert
                Assert.AreEqual("Block", results.Name);
                Assert.AreEqual(5, results.InitiativeCost.Cost);
                Assert.IsTrue(results.InitiativeCost.IsCostRequired);
                Assert.AreEqual(2, results.AttributesUsed.Count);
                Assert.IsTrue(results.AttributesUsed.Contains(AttributeType.Reaction));
                Assert.IsTrue(results.AttributesUsed.Contains(AttributeType.Intuition));

                // todo skills
                // todo limits
            }
        }
    }
}
