using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sau.Mackey.NR.Contracts;
using Sau.Mackey.NR.Services.Interfaces;

namespace Sau.Mackey.NR.Services.Tests
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class CardServiceTests
	{
		[TestClass]
		public class CardService_Ctor
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullRepository_ThenThrowArgumentNullException()
			{
				// arrange & act
				new CardService(null, new List<IInterrogator>());

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullInterrogators_ThenThrowArgumentNullException()
			{
				// arrange
				var mockRepository = new Mock<IRepository>();

				// act
				new CardService(mockRepository.Object, null);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}
		}

		[TestClass]
		public class CardService_InitializeRelationships
		{
			[TestMethod]
			public void GivenCards_ThenRelationshipsAreRemoved()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>
					{
						new Card
						{
							Relationships = new List<Relationship>
							{
								new Relationship { Type = RelationshipType.Breaks }
							}
						}
					}.AsQueryable());

				// act
				service.InitializeRelationships();

				// assert
				var cards = mockRepository.Object.GetAll<Card>();
				Assert.AreEqual(1, cards.Count());
				Assert.AreEqual(0, cards.SelectMany(x => x.Relationships).Count());
			}

			[TestMethod]
			public void GivenInterrogators_ThenInterrogateIsCalled()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var mockInterrogator = new Mock<IInterrogator>(MockBehavior.Strict);
				var interrogators = new List<IInterrogator>
				{
					mockInterrogator.Object
				};
				var service = new CardService(mockRepository.Object, interrogators);

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>().AsQueryable());
				mockInterrogator
					.Setup(x => x.Interrogate(It.IsAny<IQueryable<Card>>()));

				// act
				service.InitializeRelationships();

				// assert
				mockInterrogator.Verify(x => x.Interrogate(It.IsAny<IQueryable<Card>>()), Times.Once);
			}
		}

		[TestClass]
		public class CardService_GetCard
		{
			[TestMethod]
			public void GivenCardIsNotPresent_ThenReturnsNull()
			{
				// arrange
				var id = Guid.NewGuid();
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>().AsQueryable());
				mockRepository
					.Setup(x => x.GetById<Card>(id))
					.Returns((Card)null);

				// act
				var result = service.GetCard(id);

				// assert
				Assert.IsNull(result);
			}

			[TestMethod]
			public void GivenCardIsPresent_ThenReturnsCard()
			{
				// arrange
				var id = Guid.NewGuid();
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());
				var card = new Card { DbId = id };

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>().AsQueryable());
				mockRepository
					.Setup(x => x.GetById<Card>(id))
					.Returns(card);

				// act
				var result = service.GetCard(id);

				// assert
				Assert.IsNotNull(result);
				Assert.AreEqual(card.DbId, result.DbId);
			}

		}

		[TestClass]
		public class CardService_GetCardBySetAndCard
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullSet_ThenThrowArgumentNullException()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>().AsQueryable());

				// act
				service.GetCardBySetAndCard(null, "2");

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullCard_ThenThrowArgumentNullException()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>().AsQueryable());

				// act
				service.GetCardBySetAndCard("1", null);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			public void GivenCardNotFound_ThenReturnNull()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>().AsQueryable());

				// act
				var result = service.GetCardBySetAndCard("s123", "c234");

				// assert
				Assert.IsNull(result);
			}

			[TestMethod]
			public void GivenCardFound_ThenReturnCard()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>
					{
						new Card { Set = "Test Set", Number = "c123", Name = "test 1" },
						new Card { Set = "Test Set", Number = "c234", Name = "test 2" },
						new Card { Set = "Test Set 2", Number = "c234", Name = "test 3" },
					}.AsQueryable());
				mockRepository
					.Setup(x => x.GetAll<Lookup>())
					.Returns(new List<Lookup>
					{
						new Lookup { LookupName = "Sets", Key = "Test Set", Value = "s123" },
						new Lookup { LookupName = "Sets", Key = "Test Set 2", Value = "s234" }
					}.AsQueryable());

				// act
				var result = service.GetCardBySetAndCard("s123", "c234");

				// assert
				Assert.IsNotNull(result);
				Assert.AreEqual("test 2", result.Name);
			}
		}

		[TestClass]
		public class CardService_GetCardsByKeywords
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullKeywords_ThenThrowArgumentNullException()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				// act
				service.GetCardsByKeywords(null);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			public void GivenNoMatchesFound_ThenReturnEmptyEnumerable()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>
					{
						new Card { Keywords = {"key 1", "key 2"} },
						new Card { Keywords = {"key 1", "key 3"} },
						new Card { Keywords = {"key 1", "key 4"} },
					}.AsQueryable);

				// act
				var results = service.GetCardsByKeywords(new[] { "not found" });

				// assert
				Assert.IsNotNull(results);
				Assert.AreEqual(0, results.Count());
			}

			[TestMethod]
			public void GivenPartialMatchFound_ThenReturnMatches()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>
					{
						new Card { Keywords = {"key 1", "key 2"} },
						new Card { Keywords = {"key 1", "key 3"} },
						new Card { Keywords = {"key 1", "key 4"} },
						new Card { Keywords = {"key 9", "key 2"} },
						new Card { Keywords = {"key 9", "key 3"} },
						new Card { Keywords = {"key 9", "key 4"} },
					}.AsQueryable);

				// act
				var results = service.GetCardsByKeywords(new[] { "key 1" });

				// assert
				Assert.IsNotNull(results);
				Assert.AreEqual(3, results.Count());
			}

			[TestMethod]
			public void GivenMoreCompleteMatchFound_ThenReturnSpecificMatches()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>
					{
						new Card { Keywords = {"key 1", "key 2", "key 3"} },
						new Card { Keywords = {"key 1", "key 3"} },
						new Card { Keywords = {"key 1", "key 4"} },
						new Card { Keywords = {"key 9", "key 2"} },
						new Card { Keywords = {"key 9", "key 3"} },
						new Card { Keywords = {"key 9", "key 4"} },
					}.AsQueryable);

				// act
				var results = service.GetCardsByKeywords(new[] { "key 1", "key 2" });

				// assert
				Assert.IsNotNull(results);
				Assert.AreEqual(1, results.Count());
			}
		}

		[TestClass]
		public class CardService_GetSetNumberByName
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullSet_ThenThrowArgumentNullException()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Lookup>())
					.Returns(new List<Lookup>().AsQueryable());

				// act
				service.GetSetNumberByName(null);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			public void GivenSetNotFound_ThenReturnNull()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Lookup>())
					.Returns(new List<Lookup>().AsQueryable());

				// act
				var result = service.GetSetNumberByName("not found");

				// assert
				Assert.IsNull(result);
			}

			[TestMethod]
			public void GivenSetFound_ThenReturnSetNumber()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Lookup>())
					.Returns(new List<Lookup>
					{
						new Lookup { LookupName = "Sets", Key = "found", Value = "1" }
					}.AsQueryable());

				// act
				var result = service.GetSetNumberByName("found");

				// assert
				Assert.IsNotNull(result);
				Assert.AreEqual("1", result);
			}
		}

		[TestClass]
		public class CardService_BulkSave
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullList_ThenThrowsArgumentNullException()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>().AsQueryable());

				// act
				service.BulkSave(null);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			[ExpectedException(typeof(InvalidOperationException))]
			public void GivenListWithANullItem_ThenThrowsInvalidOperationException()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());
				var cards = new List<Card> { null };

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>().AsQueryable());

				// act
				service.BulkSave(cards);

				// assert
				Assert.Fail("InvalidOperationException should have been thrown.");
			}

			[TestMethod]
			public void GivenList_ThenCallsSaveOnEach()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());
				var cardsToSave = new List<Card>
				{
					new Card { Name = "Test One" },
					new Card { Name = "Test Two" },
					new Card { Name = "Test Three" },
				};

				mockRepository
					.Setup(x => x.GetAll<Card>())
					.Returns(new List<Card>().AsQueryable());
				mockRepository
					.Setup(x => x.Save(It.IsAny<Card>()))
					.Returns(Guid.NewGuid());

				// act
				service.BulkSave(cardsToSave);

				// assert
				mockRepository.Verify(x => x.Save(It.IsAny<Card>()), Times.Exactly(3));
			}
		}

		[TestClass]
		public class CardService_GetLookupValue
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullLookupName_ThenThrowArgumentNullException()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Lookup>())
					.Returns(new List<Lookup>().AsQueryable());

				// act
				service.GetLookupValue(null, "key");

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullKey_ThenThrowArgumentNullException()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Lookup>())
					.Returns(new List<Lookup>().AsQueryable());

				// act
				service.GetLookupValue("lookup name", null);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			public void GivenLookupNameNotFound_ThenReturnNull()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Lookup>())
					.Returns(new List<Lookup>
					{
						new Lookup { LookupName = "test lookup", Key = "test key", Value = "test value" }
					}.AsQueryable());

				// act
				var value = service.GetLookupValue("not found", "test key");

				// assert
				Assert.IsNull(value);
			}

			[TestMethod]
			public void GivenKeyNotFound_ThenReturnNull()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Lookup>())
					.Returns(new List<Lookup>
					{
						new Lookup { LookupName = "test lookup", Key = "test key", Value = "test value" }
					}.AsQueryable());

				// act
				var value = service.GetLookupValue("test lookup", "not found");

				// assert
				Assert.IsNull(value);
			}

			[TestMethod]
			public void GivenKeyFound_ThenReturnValue()
			{
				// arrange
				var mockRepository = new Mock<IRepository>(MockBehavior.Strict);
				var service = new CardService(mockRepository.Object, new List<IInterrogator>());

				mockRepository
					.Setup(x => x.GetAll<Lookup>())
					.Returns(new List<Lookup>
					{
						new Lookup { LookupName = "test lookup", Key = "test key", Value = "test value" }
					}.AsQueryable());

				// act
				var value = service.GetLookupValue("test lookup", "test key");

				// assert
				Assert.IsNotNull(value);
				Assert.AreEqual("test value", value);
			}
		}


	}
}
