using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sau.Raylan.SR5.Contracts.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CharacterTests
    {
        [TestClass]
        public class InitiativeDicePool
        {
            [TestMethod]
            public void GivenNoModifiers_ThenReturnOne()
            {
                // arrange
                var actual = new Character();

                // act
                var results = actual.InitiativeDicePool;

                // assert
                Assert.AreEqual(1, results);
            }
        }
    }
}
