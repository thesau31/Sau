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
            public void GivenEmptyConstructor_ThenAttributePoolInitializedCorrectly()
            {
                // arrange & act
                var results = new AttributePool();

                // assert
                Assert.IsNotNull(results[AttributeType.Body]);
                Assert.IsNotNull(results[AttributeType.Agility]);
                Assert.IsNotNull(results[AttributeType.Reaction]);
                Assert.IsNotNull(results[AttributeType.Strength]);
                Assert.IsNotNull(results[AttributeType.Will]);
                Assert.IsNotNull(results[AttributeType.Logic]);
                Assert.IsNotNull(results[AttributeType.Intuition]);
                Assert.IsNotNull(results[AttributeType.Charisma]);
                Assert.IsNotNull(results[AttributeType.Edge]);
                Assert.IsNotNull(results[AttributeType.Essence]);
                Assert.IsNotNull(results[AttributeType.Magic]);
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
    }
}
