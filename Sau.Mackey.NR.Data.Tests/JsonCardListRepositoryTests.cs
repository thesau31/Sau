using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sau.Mackey.NR.Contracts;

namespace Sau.Mackey.NR.Data.Tests
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class JsonCardListRepositoryTests
	{
		[TestClass]
		public class JsonCardListRepository_Ctor
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullCardDirectoryInfo_ThenThrowArgumentNullException()
			{
				// arrange & act
				new JsonCardListRepository(null, new DirectoryInfo("."));

				// asser
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullLookupDirectoryInfo_ThenThrowArgumentNullException()
			{
				// arrange & act
				new JsonCardListRepository(new DirectoryInfo("."), null);

				// asser
				Assert.Fail("ArgumentNullException should have been thrown.");
			}
		}

		[TestClass]
		public class JsonCardListRepository_Get
		{
			[TestMethod]
			public void Given_ThenReturnsQueryable()
			{
				// arrange
				var repo = new JsonCardListRepository(new DirectoryInfo(@"."), new DirectoryInfo(@"."));

				// act
				var result = repo.GetAll<Card>();

				// assert
				Assert.IsNotNull(result);
			}
		}

		[TestClass]
		public class JsonCardListRepository_GetById
		{
			[TestMethod]
			public void GivenCardIsNotPresent_ThenReturnsNull()
			{
				// arrange
				var id = Guid.NewGuid();
				var repo = new JsonCardListRepository(new DirectoryInfo(@"."), new DirectoryInfo(@"."));

				// act
				var result = repo.GetById<Card>(id);

				// assert
				Assert.IsNull(result);
			}

			[TestMethod]
			public void GivenCardIsPresent_ThenReturnsCard()
			{
				// arrange
				var id = Guid.NewGuid();
				var repo = new JsonCardListRepository(new DirectoryInfo(@"."), new DirectoryInfo(@"."));
				var card = new Card { DbId = id };
				repo.Data[typeof(Card)].Add(card);

				// act
				var result = repo.GetById<Card>(id);

				// assert
				Assert.IsNotNull(result);
				Assert.AreEqual(id, result.DbId);
			}
		}

		[TestClass]
		public class JsonCardListRepository_Save
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullCard_ThenThrowsArgumentNullException()
			{
				// arrange
				var repo = new JsonCardListRepository(new DirectoryInfo(@"."), new DirectoryInfo(@"."));

				// act
				repo.Save<Card>(null);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			public void GivenValidCardToUpdate_ThenSave()
			{
				// arrange
				var id = Guid.NewGuid();
				var repo = new JsonCardListRepository(new DirectoryInfo(@"."), new DirectoryInfo(@"."));
				repo.Data[typeof(Card)].Add(new Card { DbId = id });
				var cardToUpdate = new Card { DbId = id, Name = "Test Card" };

				// act
				var result = repo.Save(cardToUpdate);

				// assert
				Assert.AreEqual(1, repo.Data[typeof(Card)].Count);
				Assert.AreEqual(id, result);
				Assert.AreEqual(id, cardToUpdate.DbId);
				Assert.AreEqual(id, repo.Data[typeof(Card)].Cast<Card>().ToList()[0].DbId);
				Assert.AreEqual(cardToUpdate.Name, repo.Data[typeof(Card)].Cast<Card>().ToList()[0].Name);
			}

			[TestMethod]
			public void GivenValidCardToInsert_ThenSave()
			{
				// arrange
				var repo = new JsonCardListRepository(new DirectoryInfo(@"."), new DirectoryInfo(@"."));
				var cardToInsert = new Card { Name = "Test Card" };

				// act
				var result = repo.Save(cardToInsert);

				// assert
				Assert.AreEqual(1, repo.Data[typeof(Card)].Count);
				Assert.AreNotEqual(Guid.Empty, result);
				Assert.AreEqual(result, cardToInsert.DbId);
				Assert.AreEqual(result, repo.Data[typeof(Card)].Cast<Card>().ToList()[0].DbId);
				Assert.AreEqual(cardToInsert.Name, repo.Data[typeof(Card)].Cast<Card>().ToList()[0].Name);
			}
		}

		[TestClass]
		public class JsonCardListRepository_Delete
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullCard_ThenThrowsArgumentNullException()
			{
				// arrange
				var repo = new JsonCardListRepository(new DirectoryInfo(@"."), new DirectoryInfo(@"."));

				// act
				repo.Delete<Card>(null);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			[ExpectedException(typeof(InvalidOperationException))]
			public void GivenCardNotFound_ThenThrowsInvalidOperationException()
			{
				// arrange
				var repo = new JsonCardListRepository(new DirectoryInfo(@"."), new DirectoryInfo(@"."));
				var card = new Card { DbId = Guid.NewGuid() };

				// act
				repo.Delete(card);

				// assert
				Assert.Fail("InvalidOperationException should have been thrown.");
			}

			[TestMethod]
			public void GivenCardFound_ThenCardRemoved()
			{
				// arrange
				var repo = new JsonCardListRepository(new DirectoryInfo(@"."), new DirectoryInfo(@"."));
				var card = new Card { DbId = Guid.NewGuid() };
				repo.Data[typeof(Card)].Add(card);

				// act
				repo.Delete(card);

				// assert
				Assert.AreEqual(0, repo.Data[typeof(Card)].Count);
				Assert.IsFalse(repo.Data[typeof(Card)].Cast<Card>().Any(x => x.DbId == card.DbId));
			}
		}
	}
}
