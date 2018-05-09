using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sau.Raylan.SR5.Services;

namespace Sau.Raylan.SR5.Services.Tests
{
    [TestClass]
    public class InitiativePassSlotTests
    {
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
