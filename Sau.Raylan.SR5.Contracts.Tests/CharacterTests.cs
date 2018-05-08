using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sau.Raylan.SR5.Contracts;
using Moq;
using System.Collections.Generic;

namespace Sau.Raylan.Contracts.Tests
{
    [TestClass]
    public class CharacterTests
    {
        [TestClass]
        public class Ctor
        {
            //[TestMethod]
            //[ExpectedException(typeof(ArgumentNullException))]
            //public void GivenNullDiceBag_ThrowNewArgumentNullException()
            //{
            //    // arrange & act
            //    var results = new Character(null);

            //    // assert
            //    Assert.Fail("ArgumentNullException should have been thrown.");
            //}
        }

        [TestClass]
        public class RollInitiative
        {
            //[TestMethod]
            //public void GivenReactionAndIntuition_ThenRollInitiativeShouldCalculateProperly()
            //{
            //    // arrange
            //    const int reaction = 2;
            //    const int intuition = 3;
            //    var mockDiceBag = new Mock<DiceBag>(MockBehavior.Strict);

            //    mockDiceBag
            //        .Setup(x => x.d6(1))
            //        .Returns(new List<int> { 5 });

            //    var actual = new Character(mockDiceBag.Object) { Reaction = reaction, Intuition = intuition };

            //    // act
            //    actual.RollInitiative();

            //    // assert
            //    Assert.AreEqual(10, actual.CurrentInitiative);
            //}
        }
    }
}
