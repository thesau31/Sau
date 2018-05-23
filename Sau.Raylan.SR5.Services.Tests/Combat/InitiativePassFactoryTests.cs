using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sau.Raylan.SR5.Contracts.Interfaces;
using Sau.Raylan.SR5.Services.Combat;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sau.Raylan.SR5.Services.Tests.Combat
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InitiativePassFactoryTests
    {
        [TestClass]
        public class Ctor
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenDiceBagIsNull_ThrowArgumentNullException()
            {
                // arrange & act
                var results = new InitiativePassFactory(null, new List<ICharacter>());

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenParticipantsIsNull_ThrowArgumentNullException()
            {
                // arrange & act
                var results = new InitiativePassFactory(new DiceBag(), null);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }
        }

        [TestClass]
        public class Create
        {
            [TestMethod]
            public void GivenCreateType_ThenSetupAndReturnInstance()
            {
                // arrange
                var bag = new DiceBag();
                var characters = new List<ICharacter>();
                var actual = new InitiativePassFactory(bag, characters);

                // act
                var results = actual.Create<MockInitiativePass>();

                // assert
                Assert.IsNotNull(results);
                Assert.IsTrue(results.WasSetupCalled);
            }

            
        }

        
    }
}
