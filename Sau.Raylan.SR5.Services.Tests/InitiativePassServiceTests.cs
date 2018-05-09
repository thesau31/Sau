using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Sau.Raylan.SR5.Services.Tests
{
    [TestClass]
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
    }
}
