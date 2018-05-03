using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sau.Mackey.NR.Contracts;
using Sau.Mackey.NR.Services.Interfaces;
using Sau.Mackey.NR.Services.Interrogators.IcebreakerVs;

namespace Sau.Mackey.NR.Services.Tests.Interrogators.IcebreakerVs
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class IcebreakerVsRelationshipInterrogatorTests
	{
		[TestClass]
		public class IcebreakerVsRelationshipInterrogator_Interrogate
		{
			private Mock<IRelationshipBuilder> _mockRelationshipBuilder;
			private Mock<IcebreakerVsRelationshipInterrogator> _mockInterrogator;
			private List<Card> _cards;

			[TestInitialize]
			public void Init()
			{
				_mockRelationshipBuilder = new Mock<IRelationshipBuilder>();
				_mockInterrogator = new Mock<IcebreakerVsRelationshipInterrogator>(_mockRelationshipBuilder.Object) { CallBase = true };
				_cards = new List<Card>
				{
					new Card { Name = "test 1", Keywords = new List<string> { "Killer" } },
					new Card { Name = "test 2", Keywords = new List<string> { "Decoder" } },
					new Card { Name = "test 3", Keywords = new List<string> { "Fractor" } },
					new Card { Name = "test 4", Keywords = new List<string> { "Sentry" } },
					new Card { Name = "test 5", Keywords = new List<string> { "Code Gate" } },
					new Card { Name = "test 6", Keywords = new List<string> { "Barrier" } },
				};
			}

			[TestMethod]
			public void GivenMatchFound_ThenBuilderIsCalled()
			{
				// arrange
				_mockInterrogator.Setup(x => x.IcebreakerKeyword).Returns("Killer");
				_mockInterrogator.Setup(x => x.IceKeyword).Returns("Sentry");

				// act
				_mockInterrogator.Object.Interrogate(_cards.AsQueryable());

				// assert
				_mockRelationshipBuilder
					.Verify(x => x.BuildOneVsMany(
						It.Is<Card>(c => c.Keywords.Contains("Killer")),
						It.Is<IQueryable<Card>>(cs => cs.All(c => c.Keywords.Contains("Sentry"))),
						RelationshipType.Breaks), Times.Once());
				_mockRelationshipBuilder
					.Verify(x => x.BuildOneVsMany(
						It.Is<Card>(c => c.Keywords.Contains("Sentry")),
						It.Is<IQueryable<Card>>(cs => cs.All(c => c.Keywords.Contains("Killer"))),
						RelationshipType.IsBrokenBy), Times.Once());
			}

			[TestMethod]
			public void GivenNoMatchesFound_ThenBuilderIsNotCalled()
			{
				// arrange
				_mockInterrogator.Setup(x => x.IcebreakerKeyword).Returns("bob the icebreaker");
				_mockInterrogator.Setup(x => x.IceKeyword).Returns("bob the ice");

				// act
				_mockInterrogator.Object.Interrogate(_cards.AsQueryable());

				// assert
				_mockRelationshipBuilder
					.Verify(x => x.BuildOneVsMany(
						It.IsAny<Card>(),
						It.IsAny<IQueryable<Card>>(),
						It.IsAny<RelationshipType>()), Times.Never);
			}
		}
	}
}
