﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sau.Raylan.SR5.Contracts;
using Sau.Raylan.SR5.Contracts.Interfaces;
using Sau.Raylan.SR5.Services.Combat;
using Sau.Raylan.SR5.Services.Tests.Combat;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sau.Raylan.SR5.Services.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CombatTurnServiceTests
    {
        [TestClass]
        public class Ctor
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullDiceBag_ThrowArgumentNullException()
            {
                // arrange & act
                var results = new CombatTurnService<MockInitiativePass>(null, new List<ICharacter>());

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void GivenNullParticipants_ThrowArgumentNullException()
            {
                // arrange & act
                var results = new CombatTurnService<MockInitiativePass>(new DiceBag(), null);

                // assert
                Assert.Fail("ArgumentNullException should have been thrown.");
            }

            [TestMethod]
            public void GivenParticipants_CreateCombatTurnService()
            {
                // arrange & act
                var characters = new List<ICharacter>();
                var results = new CombatTurnService<MockInitiativePass>(new DiceBag(), characters);

                // assert
                Assert.AreSame(characters, results.Participants);
                Assert.IsNotNull(results.CurrentInitiativePass);
            }
        }

        [TestClass]
        public class Next
        {
            [TestMethod]
            public void GivenParticipantsLeftToActInCurrentPass_ThenReturnNextParticipant()
            {
                // arrange
                var bag = new DiceBag();
                var characters = new List<ICharacter>();
                var actual = new CombatTurnService<MockInitiativePass>(bag, characters);

                (actual.CurrentInitiativePass as MockInitiativePass).ShouldReturnNext = true;

                // act
                var results = actual.Next();

                // assert
                Assert.IsNotNull(results);
                Assert.IsFalse((actual.CurrentInitiativePass as MockInitiativePass).WasSetupCalled);
                Assert.IsTrue((actual.CurrentInitiativePass as MockInitiativePass).WasNextCalled);
            }

            [TestMethod]
            public void GivenPassIsCompletedAndAnotherPassIsNeeded_ThenSetupNextPassAndReturnNextParticipant()
            {
                // arrange
                var bag = new DiceBag();
                var characters = new List<ICharacter>();
                var actual = new CombatTurnService<MockInitiativePass>(bag, characters);

                (actual.CurrentInitiativePass as MockInitiativePass).ShouldReturnNext = true;

                // act
                var results = actual.Next();

                // assert
                Assert.IsNotNull(results);
                Assert.IsTrue((actual.CurrentInitiativePass as MockInitiativePass).WasSetupCalled);
                Assert.IsTrue((actual.CurrentInitiativePass as MockInitiativePass).WasNextCalled);
            }

            [TestMethod]
            public void GivenPassIsCompletedAndNoOneLeftToAct_ThenMarkCompletedAndReturnNull()
            {
                // arrange
                var bag = new DiceBag();
                var characters = new List<ICharacter>();
                var actual = new CombatTurnService<MockInitiativePass>(bag, characters);

                (actual.CurrentInitiativePass as MockInitiativePass).ShouldReturnNext = false;

                // act
                var results = actual.Next();

                // assert
                Assert.IsNull(results);
                Assert.IsFalse((actual.CurrentInitiativePass as MockInitiativePass).WasSetupCalled);
                Assert.IsFalse((actual.CurrentInitiativePass as MockInitiativePass).WasNextCalled);
            }
        }
    }
}
