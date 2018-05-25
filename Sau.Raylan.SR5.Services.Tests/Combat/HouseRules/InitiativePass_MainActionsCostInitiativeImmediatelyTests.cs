using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Services.Combat;
using Sau.Raylan.SR5.Services.Combat.HouseRules;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Sau.Raylan.SR5.Services.Tests.Combat.HouseRules
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InitiativePass_MainActionsCostInitiativeImmediatelyTests
    {
        [TestClass]
        public class Reset
        {
            [TestMethod]
            public void GivenResetCalled_ThenResetTheInitiativePass()
            {
                // arrange
                var actual = new InitiativePass_MainActionsCostInitiativeImmediately();

                actual.Setup(new DiceBag(), new List<Character>());
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 5, HasActed = true });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 15, HasActed = true });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 17, HasActed = true });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 10, HasActed = true });
                actual.InitiativeOrder.Add(new InitiativePassSlot() { CurrentInitiative = 8, HasActed = true });

                // act
                actual.Reset();

                // assert
                Assert.IsTrue(actual.InitiativeOrder.Count(x => x.CurrentInitiative == 5) == 1);
                Assert.IsTrue(actual.InitiativeOrder.Count(x => x.CurrentInitiative == 15) == 1);
                Assert.IsTrue(actual.InitiativeOrder.Count(x => x.CurrentInitiative == 17) == 1);
                Assert.IsTrue(actual.InitiativeOrder.Count(x => x.CurrentInitiative == 10) == 1);
                Assert.IsTrue(actual.InitiativeOrder.Count(x => x.CurrentInitiative == 8) == 1);
                Assert.IsTrue(actual.InitiativeOrder.All(x => x.HasActed == false));
                Assert.AreEqual(5, actual.InitiativeOrder.Count);
            }
        }

        [TestClass]
        public class Next
        {
            [TestMethod]
            public void GivenEveryoneActed_ThenReturnNull()
            {
                // arrange
                var actual = new InitiativePass_MainActionsCostInitiativeImmediately();

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
                var actual = new InitiativePass_MainActionsCostInitiativeImmediately();

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
            public void GivenThereIsSomeoneLeftToAct_ThenReturnTheNextCharacterAndSubtractInitiative()
            {
                // arrange
                var actual = new InitiativePass_MainActionsCostInitiativeImmediately();
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
                Assert.AreEqual(7, three.CurrentInitiative);
            }
        }
    }
}
