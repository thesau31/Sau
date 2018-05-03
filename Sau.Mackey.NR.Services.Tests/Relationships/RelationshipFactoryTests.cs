using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sau.Mackey.NR.Contracts;
using Sau.Mackey.NR.Services.Relationships;

namespace Sau.Mackey.NR.Services.Tests.Relationships
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class RelationshipFactoryTests
	{
		[TestClass]
		public class RelationshipFactory_Create
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullSource_ThenThrowArgumentNullException()
			{
				// arrange
				var factory = new RelationshipFactory();

				// act
				factory.Create(RelationshipType.Breaks, null, new Card());

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullTarget_ThenThrowArgumentNullException()
			{
				// arrange
				var factory = new RelationshipFactory();

				// act
				factory.Create(RelationshipType.Breaks, new Card(), null);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}

			[TestMethod]
			public void GivenRelationshipTypeIsBreaks_ThenReturnIcebreakerEconomicCard()
			{
				// arrange
				var factory = new RelationshipFactory();

				// act
				var relationship = factory.Create(RelationshipType.Breaks, new Card(), new Card());

				// assert
				Assert.IsInstanceOfType(relationship, typeof(IcebreakerEconomicRelationship));
			}

			[TestMethod]
			public void GivenOtherwise_ThenReturnRelationship()
			{
				// arrange
				var factory = new RelationshipFactory();
				var source = new Card();
				var target = new Card();

				// act
				var relationship = factory.Create(RelationshipType.IsBrokenBy, source, target);

				// assert
				Assert.IsNotInstanceOfType(relationship, typeof(IcebreakerEconomicRelationship));
				Assert.AreEqual(RelationshipType.IsBrokenBy, relationship.Type);
				Assert.AreEqual(source, relationship.Source);
				Assert.AreEqual(target, relationship.Target);
			}
		}
	}
}
