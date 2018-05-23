using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Contracts.Interfaces;
using Sau.Raylan.SR5.Services.Combat;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sau.Raylan.SR5.Services.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InitiativePassTests
    {
        [TestClass]
        public class Setup
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullDiceBag_ThrowArgumentNullException()
            {
                // arrange
                var actual = new InitiativePass();

                // act
                actual.Setup(null, new List<ICharacter>());

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullCharacterEnumerable_ThrowArgumentNullException()
            {
                // arrange
                var actual = new InitiativePass();

                // act
                actual.Setup(new DiceBag(), null);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            public void GivenDiceBagAndCharacterList_ThenStoreAndSortOrder()
            {
                // arrange
                var diceBag = new DiceBag();
                var characters = new List<Character>() { new Character(), new Character(), new Character(), new Character(), new Character(), new Character(), new Character(), new Character(), new Character(), new Character(), new Character(), new Character() };
                var actual = new InitiativePass();

                // act
                actual.Setup(diceBag, characters);

                // assert
                Assert.AreEqual(characters.Count, actual.InitiativeOrder.Count);
                Assert.IsTrue(actual.InitiativeOrder.TrueForAll(x => x.HasActed == false));
                for (int i = 1; i < actual.InitiativeOrder.Count; i++)
                    Assert.IsTrue(actual.InitiativeOrder[i].CurrentInitiative <= actual.InitiativeOrder[i - 1].CurrentInitiative);
            }
        }

        [TestClass]
        public class Next
        {
            [TestMethod]
            public void GivenEveryoneActed_ThenReturnNull()
            {
                // arrange
                var actual = new InitiativePass();

                actual.Setup(new DiceBag(), new List<Character>());
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 5, HasActed = true });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 15, HasActed = true });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 17, HasActed = true });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 10, HasActed = true });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 8, HasActed = true });

                // act
                var results = actual.Next();

                // assert
                Assert.IsNull(results);
            }

            [TestMethod]
            public void GivenNoOneWhoHasNotActedAlsoHasPositiveInitiative_ThenReturnNull()
            {
                // arrange
                var actual = new InitiativePass();

                actual.Setup(new DiceBag(), new List<Character>());
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 0, HasActed = false });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 15, HasActed = true });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 17, HasActed = true });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 0, HasActed = false });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 8, HasActed = true });

                // act
                var results = actual.Next();

                // assert
                Assert.IsNull(results);
            }

            [TestMethod]
            public void GivenThereIsSomeoneLeftToAct_ThenReturnTheNextCharacter()
            {
                // arrange
                var actual = new InitiativePass();
                var three = new InitiativePassSlot() { CurrentInitiative = 17, HasActed = false };

                actual.Setup(new DiceBag(), new List<Character>());
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 0, HasActed = false });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 15, HasActed = false });
                actual.InitiativeOrder.Add(three);
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 0, HasActed = false });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 8, HasActed = false });

                // act
                var results = actual.Next();

                // assert
                Assert.IsNotNull(results);
                Assert.AreSame(three, results);
            }
        }
    }
}
