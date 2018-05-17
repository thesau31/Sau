using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sau.Raylan.SR5.Contracts.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AttributePoolTests
    {
        [TestClass]
        public class Ctor
        {
            [TestMethod]
            public void GivenEmptyConstructor_ThenInitializeCorrectly()
            {
                // arrange & act
                var results = new AttributePool();

                // assert
                Assert.IsNotNull(results[AttributeType.Body]);
                Assert.IsNotNull(results[AttributeType.Agility]);
                Assert.IsNotNull(results[AttributeType.Reaction]);
                Assert.IsNotNull(results[AttributeType.Strength]);
                Assert.IsNotNull(results[AttributeType.Willpower]);
                Assert.IsNotNull(results[AttributeType.Logic]);
                Assert.IsNotNull(results[AttributeType.Intuition]);
                Assert.IsNotNull(results[AttributeType.Charisma]);
                Assert.IsNotNull(results[AttributeType.Edge]);
                Assert.IsNotNull(results[AttributeType.Essence]);
                Assert.IsNotNull(results[AttributeType.Magic]);

                Assert.IsNotNull(results.LimitValue(LimitType.Mental));
                Assert.IsNotNull(results.LimitValue(LimitType.Physical));
                Assert.IsNotNull(results.LimitValue(LimitType.Social));
            }
        }

        [TestClass]
        public class Indexer
        {
            [TestMethod]
            public void GivenAttributePresent_SetAndReturnValue()
            {
                // arrange
                var actual = new AttributePool();

                // act
                actual[AttributeType.Body] = 12;
                var results = actual[AttributeType.Body];

                // assert
                Assert.AreEqual(12, results);
            }
        }

        [TestClass]
        public class Display
        {
            [TestMethod]
            public void GivenAttributeValue_DisplayProperly()
            {
                // arrange
                var actual = new AttributePool();

                // act
                actual[AttributeType.Body] = 12;
                var results = actual.Display(AttributeType.Body);

                // assert
                Assert.AreEqual("BOD (12)", results);
            }
        }

        [TestClass]
        public class LimitValue
        {
            [TestMethod]
            public void GivenMentalAttributesAreEvenlyDivisibleByThree_ThenCalcOkAndDoNotRoundUp()
            {
                // arrange
                var actual = new AttributePool();
                actual[AttributeType.Logic] = 4;
                actual[AttributeType.Intuition] = 2;
                actual[AttributeType.Willpower] = 2;

                // act
                var results = actual.LimitValue(LimitType.Mental);

                // assert
                Assert.AreEqual(4, results);
            }

            [TestMethod]
            public void GivenMentalAttributesAreNotEvenlyDivisibleByThree_ThenCalcOkAndRoundUp()
            {
                // arrange
                var actual = new AttributePool();
                actual[AttributeType.Logic] = 4;
                actual[AttributeType.Intuition] = 3;
                actual[AttributeType.Willpower] = 2;

                // act
                var results = actual.LimitValue(LimitType.Mental);

                // assert
                Assert.AreEqual(5, results);
            }

            [TestMethod]
            public void GivenPhysicalAttributesAreEvenlyDivisibleByThree_ThenCalcOkAndDoNotRoundUp()
            {
                // arrange
                var actual = new AttributePool();
                actual[AttributeType.Strength] = 4;
                actual[AttributeType.Body] = 2;
                actual[AttributeType.Reaction] = 2;

                // act
                var results = actual.LimitValue(LimitType.Physical);

                // assert
                Assert.AreEqual(4, results);
            }

            [TestMethod]
            public void GivenPhysicalAttributesAreNotEvenlyDivisibleByThree_ThenCalcOkAndRoundUp()
            {
                // arrange
                var actual = new AttributePool();
                actual[AttributeType.Strength] = 4;
                actual[AttributeType.Body] = 3;
                actual[AttributeType.Reaction] = 2;

                // act
                var results = actual.LimitValue(LimitType.Physical);

                // assert
                Assert.AreEqual(5, results);
            }

            [TestMethod]
            public void GivenSocialAttributesAreEvenlyDivisibleByThree_ThenCalcOkAndDoNotRoundUp()
            {
                // arrange
                var actual = new AttributePool();
                actual[AttributeType.Charisma] = 4;
                actual[AttributeType.Willpower] = 2;
                actual[AttributeType.Essence] = 2;

                // act
                var results = actual.LimitValue(LimitType.Social);

                // assert
                Assert.AreEqual(4, results);
            }

            [TestMethod]
            public void GivenSocialAttributesAreNotEvenlyDivisibleByThree_ThenCalcOkAndRoundUp()
            {
                // arrange
                var actual = new AttributePool();
                actual[AttributeType.Charisma] = 4;
                actual[AttributeType.Willpower] = 3;
                actual[AttributeType.Essence] = 2;

                // act
                var results = actual.LimitValue(LimitType.Social);

                // assert
                Assert.AreEqual(5, results);
            }
        }

        [TestClass]
        public class LimitDisplay
        {
            [TestMethod]
            public void GivenAttributePool_ThenDisplayLimitProperly()
            {
                // arrange
                var actual = new AttributePool();

                // act
                var results = actual.LimitDisplay(LimitType.Physical);

                // assert
                Assert.AreEqual("[Physical (0)]", results);
            }
        }
    }
}
