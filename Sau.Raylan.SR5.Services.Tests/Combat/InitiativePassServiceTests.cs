using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Sau.Raylan.SR5.Services.Combat;

namespace Sau.Raylan.SR5.Services.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InitiativePassServiceTests
    {
        [TestClass]
        public class Ctor
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullInitiativeOrder_ThrowArgumentNullException()
            {
                // arrange & act
                var actual = new InitiativePassService(null);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            public void GivenInitiativeOrder_ThenStoreAndSortOrder()
            {
                // arrange
                var one = new InitiativePassSlot() { HasActed = false, CurrentInitiative = 21 };
                var two = new InitiativePassSlot() { HasActed = false, CurrentInitiative = 16 };
                var three = new InitiativePassSlot() { HasActed = true, CurrentInitiative = 18 };
                var four = new InitiativePassSlot() { HasActed = true, CurrentInitiative = 12 };

                // act
                var actual = new InitiativePassService(new List<InitiativePassSlot>() { four, two, one, three });

                // assert
                Assert.AreSame(one, actual.InitiativeOrder[0]);
                Assert.AreSame(two, actual.InitiativeOrder[1]);
                Assert.AreSame(three, actual.InitiativeOrder[2]);
                Assert.AreSame(four, actual.InitiativeOrder[3]);
            }
        }

        [TestClass]
        public class Next
        {
            [TestMethod]
            public void GivenEveryoneActed_ThenReturnNull()
            {
                // arrange
                var one = new InitiativePassSlot() { CurrentInitiative = 5, HasActed = true };
                var two = new InitiativePassSlot() { CurrentInitiative = 15, HasActed = true };
                var three = new InitiativePassSlot() { CurrentInitiative = 17, HasActed = true };
                var four = new InitiativePassSlot() { CurrentInitiative = 10, HasActed = true };
                var five = new InitiativePassSlot() { CurrentInitiative = 8, HasActed = true };
                var initiativeOrder = new List<InitiativePassSlot>() { one, two, three, four, five };
                var actual = new InitiativePassService(initiativeOrder);

                // act
                var results = actual.Next();

                // assert
                Assert.IsNull(results);
            }

            [TestMethod]
            public void GivenNoOneWhoHasNotActedAlsoHasPositiveInitiative_ThenReturnNull()
            {
                // arrange
                var one = new InitiativePassSlot() { CurrentInitiative = 0, HasActed = false };
                var two = new InitiativePassSlot() { CurrentInitiative = 15, HasActed = true };
                var three = new InitiativePassSlot() { CurrentInitiative = 17, HasActed = true };
                var four = new InitiativePassSlot() { CurrentInitiative = 0, HasActed = false };
                var five = new InitiativePassSlot() { CurrentInitiative = 8, HasActed = true };
                var initiativeOrder = new List<InitiativePassSlot>() { one, two, three, four, five };
                var actual = new InitiativePassService(initiativeOrder);

                // act
                var results = actual.Next();

                // assert
                Assert.IsNull(results);
            }

            [TestMethod]
            public void GivenThereIsSomeoneLeftToAct_ThenReturnTheNextCharacter()
            {
                // arrange
                var one = new InitiativePassSlot() { CurrentInitiative = 0, HasActed = false };
                var two = new InitiativePassSlot() { CurrentInitiative = 15, HasActed = false };
                var three = new InitiativePassSlot() { CurrentInitiative = 17, HasActed = false };
                var four = new InitiativePassSlot() { CurrentInitiative = 0, HasActed = false };
                var five = new InitiativePassSlot() { CurrentInitiative = 8, HasActed = false };
                var initiativeOrder = new List<InitiativePassSlot>() { one, two, three, four, five };
                var actual = new InitiativePassService(initiativeOrder);

                // act
                var results = actual.Next();

                // assert
                Assert.IsNotNull(results);
                Assert.AreSame(three, results);
            }
        }
    }
}
