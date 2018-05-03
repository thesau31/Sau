using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Sau.Mackey.NR.Contracts.Tests
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class Card_IcebreakerTests
	{
		[TestClass]
		public class Card_Icebreaker_CostToRaiseStrength_Get
		{
			[TestMethod]
			public void GivenPatternNotMatched_ThenReturnZero()
			{
				// arrange
				var card = new Card
				{
					Effect = "not a match"
				};

				// act
				var result = card.CostToRaiseStrength;

				// assert
				Assert.AreEqual(0, result);
			}

			[TestMethod]
			public void GivenPatternMatched_ThenReturnValue()
			{
				// arrange
				var card = new Card
				{
					Effect = "1€: Break barrier subroutine.  2€: +3 strength."
				};

				// act
				var result = card.CostToRaiseStrength;

				// assert
				Assert.AreEqual(2, result);
			}
		}

		[TestClass]
		public class Card_Icebreaker_StrengthRaisedPerCost_Get
		{
			[TestMethod]
			public void GivenPatternNotMatched_ThenReturnZero()
			{
				// arrange
				var card = new Card
				{
					Effect = "not a match"
				};

				// act
				var result = card.StrengthRaisedPerCost;

				// assert
				Assert.AreEqual(0, result);
			}

			[TestMethod]
			public void GivenPatternMatched_ThenReturnValue()
			{
				// arrange
				var card = new Card
				{
					Effect = "1€: Break barrier subroutine.  2€: +3 strength."
				};

				// act
				var result = card.StrengthRaisedPerCost;

				// assert
				Assert.AreEqual(3, result);
			}
		}

		[TestClass]
		public class Card_Icebreaker_IcebreakerRelationships_Get
		{
			[TestMethod]
			public void GivenNonIcebreakerRelationships_ThenReturnEmptyEnumerable()
			{
				// arrange
				var card = new Card
				{
					Relationships = new List<Relationship>
					{
						new Relationship()
					}
				};

				// act
				var result = card.IcebreakerRelationships;

				// assert
				Assert.AreEqual(0, result.Count());
			}

			[TestMethod]
			public void GivenIcebreakerRelationships_ThenReturnEnumerable()
			{
				// arrange
				var card = new Card
				{
					Relationships = new List<Relationship>
					{
						new Relationship(),
						new IcebreakerEconomicRelationship(),
						new IcebreakerEconomicRelationship()
					}
				};

				// act
				var result = card.IcebreakerRelationships;

				// assert
				Assert.AreEqual(2, result.Count());
			}
		}

		[TestClass]
		public class Card_Icebreaker_OverallAverage
		{
			[TestMethod]
			public void GivenNotAnIcebreaker_ThenReturnEmptyString()
			{
				// arrange
				var card = new Card
				{
					Keywords = { "not an icebreaker" }
				};

				// act
				var result = card.OverallAverage(true, 1);

				// assert
				Assert.IsNotNull(result);
				Assert.AreEqual(string.Empty, result);
			}

			[TestMethod]
			public void GivenAnIcebreaker_ThenReturnAverage()
			{
				// arrange
				var mockRelationship = new Mock<IcebreakerEconomicRelationship>(MockBehavior.Strict);
				var card = new Card
				{
					Keywords = { "Icebreaker" },
					Relationships = new List<Relationship> { mockRelationship.Object }
				};

				mockRelationship
					.Setup(x => x.CalcRunCost(true, 3))
					.Returns("3.375");

				// act
				var result = card.OverallAverage(true, 3);

				// assert
				Assert.IsNotNull(result);
				Assert.AreEqual("3.38", result);
			}
		}

		[TestClass]
		public class Card_Icebreaker_ToString_IcebreakerAggregation
		{
			[TestMethod]
			public void GivenNotAnIcebreaker_ThenReturnEmptyString()
			{
				// arrange
				var card = new Card
				{
					Keywords = { "not an icebreaker" }
				};

				// act
				var result = card.ToString_IcebreakerAggregation();

				// assert
				Assert.IsNotNull(result);
				Assert.AreEqual(string.Empty, result);
			}

			[TestMethod]
			public void GivenAnIcebreaker_ThenReturnSomething()
			{
				// arrange
				var mockRelationship = new Mock<IcebreakerEconomicRelationship>(MockBehavior.Strict);
				var card = new Card
				{
					Keywords = { "Icebreaker" },
					Relationships = new List<Relationship>
					{
						mockRelationship.Object,
					}
				};

				mockRelationship
					.Setup(x => x.CalcRunCost(It.IsAny<bool>(), It.IsAny<int>()))
					.Returns("3.375");
				
				// act
				var result = card.ToString_IcebreakerAggregation();

				// assert
				Assert.IsNotNull(result);
				Assert.AreNotEqual(string.Empty, result);
			}

			[TestMethod]
			public void GivenAnIcebreakerWithNAResults_ThenReturnSomething()
			{
				// arrange
				var mockRelationship = new Mock<IcebreakerEconomicRelationship>(MockBehavior.Strict);
				var card = new Card
				{
					Keywords = { "Icebreaker" },
					Relationships = new List<Relationship>
					{
						mockRelationship.Object,
					}
				};

				mockRelationship
					.Setup(x => x.CalcRunCost(It.IsAny<bool>(), It.IsAny<int>()))
					.Returns("n/a");

				// act
				var result = card.ToString_IcebreakerAggregation();

				// assert
				Assert.IsNotNull(result);
				Assert.AreNotEqual(string.Empty, result);
			}
		}
	}
}
