using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sau.Raylan.SR5.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DicePoolTests
    {
        [TestClass]
        public class Ctor
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullDiceBag_ThrowArgumentNullException()
            {
                // arrange, act & assert
                var results = new DicePool(null, 1);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void GivenNegativeNumberOfDice_ThrowArgumentOutOfRangeException()
            {
                // arrange, act & assert
                var results = new DicePool(new DiceBag(), -1);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void GivenZeroNumberOfDice_ThrowArgumentOutOfRangeException()
            {
                // arrange, act & assert
                var results = new DicePool(new DiceBag(), 0);
            }
        }

        [TestClass]
        public class Roll
        {
            [TestMethod]
            public void Valid()
            {
                // arrange
                const int seed = 1234;
                const int _numberOfDice = 3;
                var bag = new DiceBag(seed);
                var actual = new DicePool(bag, _numberOfDice);
                var expected = new DicePoolResults(new List<int> { 2, 5, 2 });

                // act
                var results = actual.Roll();

                // assert
                Assert.AreEqual(expected.RollResults.Count, results.RollResults.Count);
                for (int i = 0; i < results.RollResults.Count; i++)
                    Assert.AreEqual(expected.RollResults[i], results.RollResults[i]);
            }
        }
    }
}
