using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Services.Combat;

namespace Sau.Raylan.SR5.Services.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CombatTurnServiceTests
    {
        [TestClass]
        public class Ctor
        {
            //[TestMethod]
            //[ExpectedException(typeof(ArgumentNullException))]
            //public void GivenNullInitiativePassService_ThrowArgumentNullException()
            //{
            //    // arrange
            //    var characters = new List<Character>();

            //    // act
            //    var results = new CombatTurnService(null, characters);

            //    // assert
            //    Assert.Fail("ArgumentNullException should have been thrown.");
            //}

            //[TestMethod]
            //[ExpectedException(typeof(ArgumentNullException))]
            //public void GivenNullParticipants_ThrowArgumentNullException()
            //{
            //    // arrange
            //    var mockInitiativePassService = new Mock<IInitiativePassService>(MockBehavior.Strict);

            //    // act
            //    var results = new CombatTurnService(mockInitiativePassService.Object, null);

            //    // assert
            //    Assert.Fail("ArgumentNullException should have been thrown.");
            //}

            //[TestMethod]
            //public void GivenParticipants_CreateCombatTurnService()
            //{
            //    // arrange
            //    var mockInitiativePassService = new Mock<IInitiativePassService>(MockBehavior.Strict);
            //    var characters = new List<Character>() { };

            //    // act
            //    var results = new CombatTurnService(mockInitiativePassService.Object, characters);

            //    // assert
            //    Assert.Fail(); // todo test
            //}
        }
    }
}
