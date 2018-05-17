using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sau.Raylan.SR5.Contracts.Tests
{
    [TestClass]
    public class SkillPoolTests
    {
        [TestClass]
        public class Ctor
        {
            [TestMethod]
            public void GivenEmptyConstructor_ThenSkillPoolInitializedCorrectly()
            {
                // arrange & act
                var results = new SkillPool();

                // assert
                Assert.IsNotNull(results[SkillType.UnarmedCombat]);
                // todo: the rest of the skills
            }
        }

        [TestClass]
        public class Indexer
        {
            [TestMethod]
            public void GivenSkillPresent_SetAndReturnValue()
            {
                // arrange
                var actual = new SkillPool();

                // act
                actual[SkillType.UnarmedCombat] = 12;
                var results = actual[SkillType.UnarmedCombat];

                // assert
                Assert.AreEqual(12, results);
            }
        }

        [TestClass]
        public class Display
        {
            [TestMethod]
            public void GivenSkillValue_DisplayProperly()
            {
                // arrange
                var actual = new SkillPool();

                // act
                actual[SkillType.UnarmedCombat] = 12;
                var results = actual.Display(SkillType.UnarmedCombat);

                // assert
                Assert.AreEqual("Unarmed Combat (12)", results);
            }
        }
    }
}
