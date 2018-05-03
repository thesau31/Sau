using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sau.Mackey.NR.Contracts;
using Sau.Mackey.NR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Sau.Mackey.NR.Services.Relationships;

namespace Sau.Mackey.NR.Services.Tests.Interrogators
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class RelationshipBuilderTests
	{
		[TestClass]
		public class RelationshipBuilder_BuildOneVsMany
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullCard_ThenThrowsArgumentNullException()
			{
				// arrange
				var targets = new List<Card>();
				const RelationshipType type = RelationshipType.Breaks;
				var mockRelationshipFactory = new Mock<IRelationshipFactory>(MockBehavior.Strict);
				var relationshipBuilder = new RelationshipBuilder(mockRelationshipFactory.Object);

				// act
				relationshipBuilder.BuildOneVsMany(null, targets.AsQueryable(), type);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullTargetList_ThenThrowsArgumentNullException()
			{
				// arrange
				var source = new Card();
				const RelationshipType type = RelationshipType.Breaks;
				var mockRelationshipFactory = new Mock<IRelationshipFactory>(MockBehavior.Strict);
				var relationshipBuilder = new RelationshipBuilder(mockRelationshipFactory.Object);

				// act
				relationshipBuilder.BuildOneVsMany(source, null, type);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			public void GivenValid_ThenRelationshipsAreCreated()
			{
				// arrange
				var source = new Card { Name = "Parent" };
				var targets = new List<Card>
				{
					new Card { Name = "Child 1" },
					new Card { Name = "Child 2" },
					new Card { Name = "Child 3" },
				};
				const RelationshipType type = RelationshipType.Breaks;
				var mockRelationshipFactory = new Mock<IRelationshipFactory>(MockBehavior.Strict);
				var relationshipBuilder = new RelationshipBuilder(mockRelationshipFactory.Object);

				mockRelationshipFactory
					.Setup(x => x.Create(type, source, It.IsAny<Card>()))
					.Returns((RelationshipType a, Card b, Card target) => 
						new Relationship
						{
							Source = source, 
							Target = target, 
							Type = type
						});

				// act
				relationshipBuilder.BuildOneVsMany(source, targets.AsQueryable(), type);

				// assert
				Assert.AreEqual(3, source.Relationships.Count);
				Assert.IsNotNull(source.Relationships.FirstOrDefault(x => x.Target.Name == "Child 1"));
				Assert.IsNotNull(source.Relationships.FirstOrDefault(x => x.Target.Name == "Child 2"));
				Assert.IsNotNull(source.Relationships.FirstOrDefault(x => x.Target.Name == "Child 3"));
			}
		}
	}
}
