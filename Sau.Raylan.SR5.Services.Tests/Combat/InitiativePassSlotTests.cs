﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Services.Actions;
using Sau.Raylan.SR5.Services.Actions.Initiative;
using Sau.Raylan.SR5.Services.Combat;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sau.Raylan.SR5.Services.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InitiativePassSlotTests
    {
        [TestClass]
        public class PerformAction
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenActionIsNull_ThrowArgumentNullException()
            {
                // arrange
                var actual = new InitiativePassSlot();

                // act
                actual.PerformAction(new DiceBag(), null);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenDiceBagIsNull_ThrowArgumentNullException()
            {
                // arrange
                var mockAction = new Mock<IInitiativeAction>(MockBehavior.Strict);
                var actual = new InitiativePassSlot();

                // act
                actual.PerformAction(null, mockAction.Object);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            public void GivenCurrentInitiativeIsZero_ThrowInvalidOperationException()
            {
                // arrange
                var mockAction = new Mock<IInitiativeAction>(MockBehavior.Strict);
                var actual = new InitiativePassSlot() { CurrentInitiative = 0 };

                // act
                actual.PerformAction(new DiceBag(), mockAction.Object);

                // assert
                Assert.Fail("InvalidOperationException should have been thrown.");
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            public void GivenCurrentInitiativeIsNegative_ThrowInvalidOperationException()
            {
                // arrange
                var mockAction = new Mock<IInitiativeAction>(MockBehavior.Strict);
                var actual = new InitiativePassSlot() { CurrentInitiative = -1 };

                // act
                actual.PerformAction(new DiceBag(), mockAction.Object);

                // assert
                Assert.Fail("InvalidOperationException should have been thrown.");
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            public void GivenInitiativeCostGreaterThanCurrentInitiativeAndCostIsRequired_ThrowInvalidOperationException()
            {
                // arrange
                var mockAction = new Mock<IInitiativeAction>(MockBehavior.Strict);
                var cost = new InitiativeCost() { Cost = 2, IsCostRequired = true };
                var actual = new InitiativePassSlot() { CurrentInitiative = 1 };

                mockAction.Setup(x => x.InitiativeCost)
                    .Returns(cost);

                // act
                actual.PerformAction(new DiceBag(), mockAction.Object);

                // assert
                Assert.Fail("InvalidOperationException should have been thrown.");
            }

            [TestMethod]
            public void GivenInitativeCostGreaterThanCurrentInitiativeAndCostIsNotRequired_ThenCurrentInitiativeSetToZeroAndActionRun()
            {
                // arrange
                var character = new Character();
                var bag = new DiceBag();
                var mockAction = new Mock<IInitiativeAction>(MockBehavior.Strict);
                var actionResult = new ActionResult("", new DicePoolResults(new List<int>()));
                var cost = new InitiativeCost() { Cost = 2, IsCostRequired = false };
                var actual = new InitiativePassSlot()
                {
                    Participant = character,
                    CurrentInitiative = 1
                };

                mockAction.Setup(x => x.InitiativeCost)
                    .Returns(cost);
                mockAction.Setup(x => x.Do(bag, character))
                    .Returns(actionResult);

                // act
                actual.PerformAction(bag, mockAction.Object);

                // assert
                Assert.AreEqual(0, actual.CurrentInitiative);
                mockAction.Verify(x => x.Do(bag, character), Times.Once);
            }

            [TestMethod]
            public void GivenInitativeCostLessThanCurrentInitiative_ThenCurrentInitiativeDecrementedAndActionRun()
            {
                // arrange
                var character = new Character();
                var bag = new DiceBag();
                var mockAction = new Mock<IInitiativeAction>(MockBehavior.Strict);
                var actionResult = new ActionResult("", new DicePoolResults(new List<int>()));
                var cost = new InitiativeCost() { Cost = 5, IsCostRequired = false };
                var actual = new InitiativePassSlot()
                {
                    Participant = character,
                    CurrentInitiative = 7
                };

                mockAction.Setup(x => x.InitiativeCost)
                    .Returns(cost);
                mockAction.Setup(x => x.Do(bag, character))
                    .Returns(actionResult);

                // act
                actual.PerformAction(bag, mockAction.Object);

                // assert
                Assert.AreEqual(2, actual.CurrentInitiative);
                mockAction.Verify(x => x.Do(bag, character), Times.Once);
            }
        }

        [TestClass]
        public class RollInitiative
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullDiceBag_ThenThrowArgumentNullException()
            {
                // arrange 
                var actual = new InitiativePassSlot();

                // act
                var results = actual.RollInitiative(null);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            public void GivenDiceBagAndNoInitiativeModifiers_ThenRollInitiativeForCharacter()
            {
                // arrange
                var character = new Character();
                character.Attributes[AttributeType.Intuition] = 4;
                character.Attributes[AttributeType.Reaction] = 3;
                var mockDiceBag = new Mock<IDiceBag>(MockBehavior.Strict);

                mockDiceBag.Setup(x => x.d6(1)).Returns(new List<int> { 6 });

                var actual = new InitiativePassSlot() { Participant = character };

                // act
                var results = actual.RollInitiative(mockDiceBag.Object);

                // assert
                Assert.AreEqual(13, results);
            }
        }

        [TestClass]
        public class CompareTo
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenOtherIsNull_ThrowArgumentNullException()
            {
                // arrange
                var actual = new InitiativePassSlot();

                // act
                var results = actual.CompareTo(null);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown");
            }

            [TestMethod]
            public void GivenThisHasActedAndOtherHasNot_ThenOtherPrecedesThis()
            {
                // arrange
                var other = new InitiativePassSlot() { HasActed = false };
                var actual = new InitiativePassSlot() { HasActed = true };

                // act
                var results = actual.CompareTo(other);

                // assert
                Assert.AreEqual(1, results);
            }

            [TestMethod]
            public void GivenThisHasNotActedAndOtherHas_ThenThisPrecedesOther()
            {
                // arrange
                var other = new InitiativePassSlot() { HasActed = true };
                var actual = new InitiativePassSlot() { HasActed = false };

                // act
                var results = actual.CompareTo(other);

                // assert
                Assert.AreEqual(-1, results);
            }

            [TestMethod]
            public void GivenBothHaveNotActedAndThisHasAHigherInitiative_ThenThisPrecedesOther()
            {
                // arrange
                var other = new InitiativePassSlot() { HasActed = false, CurrentInitiative = 12 };
                var actual = new InitiativePassSlot() { HasActed = false, CurrentInitiative = 16 };

                // act
                var results = actual.CompareTo(other);

                // assert
                Assert.AreEqual(-1, results);
            }

            [TestMethod]
            public void GivenBothHaveNotActedAndOtherHasAHigherInitiative_ThenOtherPrecedesThis()
            {
                // arrange
                var other = new InitiativePassSlot() { HasActed = false, CurrentInitiative = 8 };
                var actual = new InitiativePassSlot() { HasActed = false, CurrentInitiative = 3 };

                // act
                var results = actual.CompareTo(other);

                // assert
                Assert.AreEqual(1, results);
            }

            [TestMethod]
            public void GivenBothHaveNotActedAndBothHaveSameInitiative_ThenEqual()
            {
                // arrange
                var other = new InitiativePassSlot() { HasActed = false, CurrentInitiative = 5 };
                var actual = new InitiativePassSlot() { HasActed = false, CurrentInitiative = 5 };

                // act
                var results = actual.CompareTo(other);

                // assert
                Assert.AreEqual(0, results);
            }
        }
    }
}
