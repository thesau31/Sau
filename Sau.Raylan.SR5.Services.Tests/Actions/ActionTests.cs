using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Contracts.Interfaces;
using Sau.Raylan.SR5.Services.Actions;
using Action = Sau.Raylan.SR5.Services.Actions.Action;

namespace Sau.Raylan.SR5.Services.Tests.Actions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ActionTests
    {
        [TestClass]
        public class Do
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullDiceBag_ThenThrowArgumentNullException()
            {
                // arrange
                var actual = new Action(new ActionInput());

                // act
                var results = actual.Do(null, new Character());

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullCharacter_ThenThrowArgumentNullException()
            {
                // arrange
                var actual = new Action(new ActionInput());

                // act
                var results = actual.Do(new DiceBag(), null);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            public void GivenCharacterAndBagNoLimit_ThenReturnActionResult()
            {
                // arrange
                var input = new ActionInput()
                {
                    AttributesUsed = new List<AttributeType>() { AttributeType.Reaction, AttributeType.Intuition },
                    SkillsUsed = new List<SkillType>() { SkillType.UnarmedCombat }
                };
                var actual = new Action(input);
                var mockBag = new Mock<IDiceBag>(MockBehavior.Strict);
                var mockAttributePool = new Mock<IAttributePool>(MockBehavior.Strict);
                var mockSkillPool = new Mock<ISkillPool>(MockBehavior.Strict);
                var mockSource = new Mock<ICharacter>(MockBehavior.Strict);
                const int reaction = 7;
                const int intuition = 5;
                const int unarmedCombat = 1;
                var diceResults = new List<int>() { 1, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5, 6, 6 };

                mockBag.Setup(x => x.d6(reaction + intuition + unarmedCombat))
                    .Returns(diceResults);

                mockAttributePool.Setup(x => x[AttributeType.Reaction])
                    .Returns(reaction);
                mockAttributePool.Setup(x => x[AttributeType.Intuition])
                    .Returns(intuition);
                mockAttributePool.Setup(x => x.Display(AttributeType.Reaction))
                    .Returns("one");
                mockAttributePool.Setup(x => x.Display(AttributeType.Intuition))
                    .Returns("two");

                mockSkillPool.Setup(x => x[SkillType.UnarmedCombat])
                    .Returns(unarmedCombat);
                mockSkillPool.Setup(x => x.Display(SkillType.UnarmedCombat))
                    .Returns("three");

                mockSource.Setup(x => x.Attributes)
                    .Returns(mockAttributePool.Object);
                mockSource.Setup(x => x.Skills)
                    .Returns(mockSkillPool.Object);

                // act
                var results = actual.Do(mockBag.Object, mockSource.Object);

                // assert
                Assert.IsNotNull(results);
                Assert.AreEqual("one + two + three", results.RollNotation); // todo: threshold
                Assert.AreEqual(5, results.DiceResults.Hits);
            }

            [TestMethod]
            public void GivenCharacterAndBagLimited_ThenReturnActionResult()
            {
                // arrange
                var input = new ActionInput()
                {
                    AttributesUsed = new List<AttributeType>() { AttributeType.Reaction, AttributeType.Intuition },
                    SkillsUsed = new List<SkillType>() { SkillType.UnarmedCombat },
                    Limit = LimitType.Physical
                };
                var actual = new Action(input);
                var mockBag = new Mock<IDiceBag>(MockBehavior.Strict);
                var mockAttributePool = new Mock<IAttributePool>(MockBehavior.Strict);
                var mockSkillPool = new Mock<ISkillPool>(MockBehavior.Strict);
                var mockSource = new Mock<ICharacter>(MockBehavior.Strict);
                const int reaction = 7;
                const int intuition = 5;
                const int unarmedCombat = 1;
                var diceResults = new List<int>() { 1, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5, 6, 6 };

                mockBag.Setup(x => x.d6(reaction + intuition + unarmedCombat))
                    .Returns(diceResults);

                mockAttributePool.Setup(x => x[AttributeType.Reaction])
                    .Returns(reaction);
                mockAttributePool.Setup(x => x[AttributeType.Intuition])
                    .Returns(intuition);
                mockAttributePool.Setup(x => x.Display(AttributeType.Reaction))
                    .Returns("one");
                mockAttributePool.Setup(x => x.Display(AttributeType.Intuition))
                    .Returns("two");
                mockAttributePool.Setup(x => x.LimitValue(LimitType.Physical))
                    .Returns(-1);
                mockAttributePool.Setup(x => x.LimitDisplay(LimitType.Physical))
                    .Returns("[limit]");

                mockSkillPool.Setup(x => x[SkillType.UnarmedCombat])
                    .Returns(unarmedCombat);
                mockSkillPool.Setup(x => x.Display(SkillType.UnarmedCombat))
                    .Returns("three");

                mockSource.Setup(x => x.Attributes)
                    .Returns(mockAttributePool.Object);
                mockSource.Setup(x => x.Skills)
                    .Returns(mockSkillPool.Object);

                // act
                var results = actual.Do(mockBag.Object, mockSource.Object);

                // assert
                Assert.IsNotNull(results);
                Assert.AreEqual("one + two + three [limit]", results.RollNotation); // todo: threshold
                Assert.AreEqual(5, results.DiceResults.Hits);
            }
        }
    }
}
