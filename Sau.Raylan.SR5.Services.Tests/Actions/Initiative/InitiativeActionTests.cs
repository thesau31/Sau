using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Contracts.Interfaces;
using Sau.Raylan.SR5.Services.Actions.Initiative;

namespace Sau.Raylan.SR5.Services.Tests.Actions.Initiative
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InitiativeActionTests
    {
        [TestClass]
        public class Do
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullCharacter_ThenThrowArgumentNullException()
            {
                // arrange

            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullDiceBag_ThenThrowArgumentNullException()
            {

            }

            [TestMethod]
            public void GivenCharacterAndBag_ThenReturnActionResult()
            {
                // arrange
                var input = new InitiativeActionInput()
                {
                    AttributesUsed = new List<AttributeType>() { AttributeType.Reaction, AttributeType.Intuition }
                };
                var actual = new InitiativeAction(input);
                var mockBag = new Mock<IDiceBag>(MockBehavior.Strict);
                var mockAttributePool = new Mock<IAttributePool>(MockBehavior.Strict);
                var mockSource = new Mock<IHasAttributes>(MockBehavior.Strict);
                const int reaction = 7;
                const int intuition = 5;
                var diceResults = new List<int>() { 1, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5, 6 };

                mockBag.Setup(x => x.d6(reaction + intuition))
                    .Returns(diceResults);

                mockAttributePool.Setup(x => x[AttributeType.Reaction])
                    .Returns(reaction);
                mockAttributePool.Setup(x => x[AttributeType.Intuition])
                    .Returns(intuition);
                mockAttributePool.Setup(x => x.Display(AttributeType.Reaction))
                    .Returns("one");
                mockAttributePool.Setup(x => x.Display(AttributeType.Intuition))
                    .Returns("two");

                mockSource.Setup(x => x.Attributes)
                    .Returns(mockAttributePool.Object);
                
                // act
                var results = actual.Do(mockBag.Object, mockSource.Object);

                // assert
                Assert.IsNotNull(results);
                Assert.AreEqual("one + two", results.RollNotation);
                Assert.AreEqual(4, results.DiceResults.Hits);
            }
        }
    }
}
