using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sau.Raylan.SR5.Tests
{
    [TestClass]
    public class DicePoolResultsTests
    {
        [TestClass, ExcludeFromCodeCoverage]
        public class Ctor
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullEnumerable_ThenThrowArgumentNullException()
            {
                // arrange & act
                var results = new DicePoolResults(null);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }
        }

        [TestClass]
        public class Totals
        {
            [TestMethod]
            public void GivenResults_ThenTotalProperly()
            {
                // arrange
                const int expected = 15;
                var actual = new DicePoolResults(new List<int> { 1, 2, 3, 4, 5 });

                // act
                var results = actual.Total;

                // assert
                Assert.AreEqual(expected, results);
            }
        }

        [TestClass]
        public class Hits
        {
            [TestMethod]
            public void GivenHits_ThenCountProperly_1()
            {
                // arrange
                const int expected = 3;
                var actual = new DicePoolResults(new List<int> { 1, 2, 3, 4, 5, 6, 6 });

                // act
                var results = actual.Hits;

                // assert
                Assert.AreEqual(expected, results);
            }

            [TestMethod]
            public void GivenHits_ThenCountProperly_2()
            {
                // arrange
                const int expected = 2;
                var actual = new DicePoolResults(new List<int> { 1, 2, 3, 4, 6, 6 });

                // act
                var results = actual.Hits;

                // assert
                Assert.AreEqual(expected, results);
            }

            [TestMethod]
            public void GivenHits_ThenCountProperly_3()
            {
                // arrange
                const int expected = 1;
                var actual = new DicePoolResults(new List<int> { 1, 2, 3, 4, 5 });

                // act
                var results = actual.Hits;

                // assert
                Assert.AreEqual(expected, results);
            }

            [TestMethod]
            public void GivenNoHits_ThenCountProperly()
            {
                // arrange
                const int expected = 0;
                var actual = new DicePoolResults(new List<int> { 1, 2, 3, 4 });

                // act
                var results = actual.Hits;

                // assert
                Assert.AreEqual(expected, results);
            }

            [TestMethod]
            public void GivenHitsLimited_ThenCountProperly()
            {
                // arrange
                const int limit = 5;
                var actual = new DicePoolResults(new List<int> { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 }, limit);

                // act
                var results = actual.Hits;

                // assert
                Assert.AreEqual(limit, results);
            }
        }

        [TestClass]
        public class IsGlitch
        {
            [TestMethod]
            public void Given2OnesOutOf4_ThenNoGlitch()
            {
                // arrange
                var actual = new DicePoolResults(new List<int> { 1, 1, 6, 6 });

                // act
                var results = actual.IsGlitch;

                // assert
                Assert.IsFalse(results);
            }

            [TestMethod]
            public void Given2OnesOutOf3_ThenGlitch()
            {
                // arrange
                var actual = new DicePoolResults(new List<int> { 1, 1, 6 });

                // act
                var results = actual.IsGlitch;

                // assert
                Assert.IsTrue(results);
            }
        }

        [TestClass]
        public class IsCriticalGlitch
        {
            [TestMethod]
            public void GivenGlitchAndHits_ThenNoCriticalGlitch()
            {
                // arrange
                var actual = new DicePoolResults(new List<int> { 1, 1, 6 });

                // act
                var results = actual.IsCriticalGlitch;

                // assert
                Assert.IsFalse(results);
            }

            [TestMethod]
            public void GivenGlitchAndNoHits_ThenCriticalGlitch()
            {
                // arrange
                var actual = new DicePoolResults(new List<int> { 1, 1, 2 });

                // act
                var results = actual.IsCriticalGlitch;

                // assert
                Assert.IsTrue(results);
            }

            [TestMethod]
            public void GivenNoGlitchAndNoHits_ThenNoCriticalGlitch()
            {
                // arrange
                var actual = new DicePoolResults(new List<int> { 2, 3, 4 });

                // act
                var results = actual.IsCriticalGlitch;

                // assert
                Assert.IsFalse(results);
            }
        }
    }
}
