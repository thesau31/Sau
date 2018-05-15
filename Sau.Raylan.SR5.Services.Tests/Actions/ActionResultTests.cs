using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sau.Raylan.SR5.Services.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sau.Raylan.SR5.Services.Tests.Actions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ActionResultTests
    {
        [TestClass]
        public class Ctor
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullRollNotation_ThenThrowArgumentNullException()
            {
                // arrange
                var rollNotation = (string)null;
                var diceResults = new DicePoolResults(new List<int>());

                // act
                var results = new ActionResult(rollNotation, diceResults);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullDiceResults_ThenThrowArgumentNullException()
            {
                // arrange
                var rollNotation = "";
                var diceResults = (DicePoolResults)null;

                // act
                var results = new ActionResult(rollNotation, diceResults);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            public void GivenNotationAndDiceResults_ThenActionResultsCreated()
            {
                // arrange
                var rollNotation = "test roll notation";
                var diceResults = new DicePoolResults(new List<int>() { 13 });

                // act
                var results = new ActionResult(rollNotation, diceResults);

                // assert
                Assert.AreEqual("test roll notation", results.RollNotation);
                Assert.AreEqual(13, results.DiceResults.RollResults[0]);
            }
        }
    }
}
