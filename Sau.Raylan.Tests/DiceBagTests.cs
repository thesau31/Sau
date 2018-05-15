using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Sau.Raylan.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DiceBagTests
    {
        [TestClass]
        public class d6
        {
            [TestMethod]
            public void GivenNegativeTimes_ThenEmptyResults()
            {
                // arrange
                var actual = new DiceBag();
                var expected = 0;

                // act
                var results = actual.d6(-1);

                // assert
                Assert.AreEqual(expected, results.Count());
            }

            [TestMethod]
            public void GivenZeroTimes_ThenEmptyResults()
            {
                // arrange
                var actual = new DiceBag();
                var expected = 0;

                // act
                var results = actual.d6(0);

                // assert
                Assert.AreEqual(expected, results.Count());
            }

            [TestMethod]
            public void Valid()
            {
                // arrange
                const int seed = 1234;
                var actual = new DiceBag(seed);
                var expected = new Random(seed).Next(1, 6);

                // act
                var results = actual.d6();

                // assert
                Assert.AreEqual(expected, results);
            }

            [TestMethod]
            public void Valid_List()
            {
                // arrange
                const int seed = 1234;
                var actual = new DiceBag(seed);
                var random = new Random(seed);
                var expected = new List<int>
            {
                random.Next(1, 6),
                random.Next(1, 6),
                random.Next(1, 6),
                random.Next(1, 6)
            };

                // act
                var results = actual.d6(4).ToList();

                // assert
                Assert.AreEqual(expected.Count, results.Count);
                for (int i = 0; i < results.Count; i++)
                    Assert.AreEqual(expected[i], results[i]);
            }

            [TestMethod]
            public void ResultsAreWithinRange()
            {
                // arrange
                var actual = new DiceBag();

                // act 
                var results = actual.d6(10000);

                // assert
                Assert.AreEqual(0, results.Count(x => x < 1));
                Assert.AreEqual(0, results.Count(x => x > 6));
            }
        }
    }
}
