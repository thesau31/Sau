using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sau.Mackey.NR.Contracts.Tests
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class IcebreakerEconomicRelationshipTests
	{
		[TestClass]
		public class IcebreakerEconomicRelationship_CostToBreakSubroutine_Get
		{
			[TestMethod]
			public void GivenPatternNotMatched_ThenReturnZero()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card
					{
						Effect = "not a match"
					}
				};

				// act
				var result = relationship.CostToBreakSubroutine;

				// assert
				Assert.AreEqual(0, result);
			}

			[TestMethod]
			public void GivenWrongKeyword_ThenReturnZero()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card
					{
						Effect = "1€: Break barrier subroutine.  1€: +1 strength."
					},
					Target = new Card
					{
						Keywords = { "Sentry" }
					}
				};

				// act
				var result = relationship.CostToBreakSubroutine;

				// assert
				Assert.AreEqual(0, result);
			}

			[TestMethod]
			public void GivenPatternMatched_ThenReturnValue()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card
					{
						Effect = "1€: Break barrier subroutine.  2€: +3 strength."
					},
					Target = new Card
					{
						Keywords = { "barrier" }
					}
				};

				// act
				var result = relationship.CostToBreakSubroutine;

				// assert
				Assert.AreEqual(1, result);
			}

			[TestMethod]
			public void GivenUpToPatternMatched_ThenReturnValue()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card
					{
						Effect = "1€: Break up to 4 barrier subroutines.  2€: +3 strength."
					},
					Target = new Card
					{
						Keywords = { "barrier" }
					}
				};

				// act
				var result = relationship.CostToBreakSubroutine;

				// assert
				Assert.AreEqual(1, result);
			}
		}

		[TestClass]
		public class IcebreakerEconomicRelationship_NumberOfTargetSubroutines_Get
		{
			[TestMethod]
			public void GivenPatternNotMatched_ThenReturnZero()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Target = new Card
					{
						Effect = "not a match"
					}
				};

				// act
				var result = relationship.NumberOfTargetSubroutines;

				// assert
				Assert.AreEqual(0, result);
			}

			[TestMethod]
			public void GivenPatternMatched_ThenReturnValue()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Target = new Card
					{
						Effect = "» sub 1 » sub 2"
					}
				};

				// act
				var result = relationship.NumberOfTargetSubroutines;

				// assert
				Assert.AreEqual(2, result);
			}
		}

		[TestClass]
		public class IcebreakerEconomicRelationship_StrengthToRaise_Get
		{
			[TestMethod]
			public void GivenTargetStrengthGreaterThanOrEqualToSourceStrength_ThenReturnDifference()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card { Strength = 3 },
					Target = new Card { Strength = 4 }
				};

				// act
				var result = relationship.StrengthToRaise;

				// assert
				Assert.AreEqual(1, result);
			}

			[TestMethod]
			public void GivenTargetStrengthLessthanSourceStrength_ThenReturnZero()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card { Strength = 4 },
					Target = new Card { Strength = 3 }
				};

				// act
				var result = relationship.StrengthToRaise;

				// assert
				Assert.AreEqual(0, result);
			}
		}

		[TestClass]
		public class IcebreakerEconomicRelationship_CalcRunCost
		{
			[TestMethod]
			public void GivenStrengthToRaiseIsNonzeroAndStrengthRaisedPerCostIsZero_ThenReturnNA()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card
					{
						Strength = 3,
						Effect = "4€: Break barrier subroutine."
					},
					Target = new Card
					{
						Keywords = { "barrier" },
						Strength = 6,
						Effect = "» sub 1 » sub 2"
					}
				};

				// act
				var result = relationship.CalcRunCost(false);

				// assert
				Assert.AreEqual("n/a", result);
			}

			[TestMethod]
			public void GivenStrengthToRaiseIsZero_ThenReturnSubroutineCost()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card
					{
						Strength = 3,
						Effect = "4€: Break barrier subroutine.  2€: +3 strength."
					},
					Target = new Card
					{
						Keywords = { "barrier" },
						Strength = 3,
						Effect = "» sub 1 » sub 2"
					}
				};

				// act
				var result = relationship.CalcRunCost(false);

				// assert
				// (2 subs * 4c per) = 8
				Assert.AreEqual("8", result);
			}

			[TestMethod]
			public void GivenStrengthToRaiseIsZeroFirstRun_ThenReturnSubroutineCostAndSourceCost()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card
					{
						Cost = 1,
						Strength = 3,
						Effect = "4€: Break barrier subroutine.  2€: +3 strength."
					},
					Target = new Card
					{
						Keywords = { "barrier" },
						Strength = 3,
						Effect = "» sub 1 » sub 2"
					}
				};

				// act
				var result = relationship.CalcRunCost(true);

				// assert
				// (1c install) = 1
				// (2 subs * 4c per) = 8
				Assert.AreEqual("9", result);
			}

			[TestMethod]
			public void GivenStrengthToRaise_ThenReturnSubroutineAndStrengthCost()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card
					{
						Strength = 3,
						Effect = "4€: Break barrier subroutine.  2€: +3 strength."
					},
					Target = new Card
					{
						Keywords = { "barrier" },
						Strength = 6,
						Effect = "» sub 1 » sub 2"
					}
				};

				// act
				var result = relationship.CalcRunCost(false);

				// assert
				// (2 subs * 4c per) = 8
				// (3 str to raise, 2c per 3 str) = 2
				Assert.AreEqual("10", result);
			}

			[TestMethod]
			public void GivenStrengthToRaiseNotEven_ThenReturnSubroutineAndStrengthCost()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card
					{
						Strength = 3,
						Effect = "4€: Break barrier subroutine.  2€: +3 strength."
					},
					Target = new Card
					{
						Keywords = { "barrier" },
						Strength = 7,
						Effect = "» sub 1 » sub 2"
					}
				};

				// act
				var result = relationship.CalcRunCost(false);

				// assert
				// (2 subs * 4c per) = 8
				// (4 str to raise, 2c per 3 str) = 4
				Assert.AreEqual("12", result);
			}

			[TestMethod]
			public void GivenTwoRuns_ThenReturnAverageOfTwoRuns()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card
					{
						Strength = 3,
						Effect = "4€: Break barrier subroutine.  2€: +3 strength."
					},
					Target = new Card
					{
						Keywords = { "barrier" },
						Strength = 6,
						Effect = "» sub 1 » sub 2"
					}
				};

				// act
				var result = relationship.CalcRunCost(false, 2);

				// assert
				// (2 subs * 4c per) = 8
				// (3 str to raise, 2c per 3 str) = 2
				// total = 20 / 2 = 10
				Assert.AreEqual("10", result);
			}

			[TestMethod]
			public void GivenTwoRunsWithFirstRun_ThenReturnAverageOfTwoRuns()
			{
				// arrange
				var relationship = new IcebreakerEconomicRelationship
				{
					Source = new Card
					{
						Cost = 17,
						Strength = 3,
						Effect = "4€: Break barrier subroutine.  2€: +3 strength."
					},
					Target = new Card
					{
						Keywords = { "barrier" },
						Strength = 6,
						Effect = "» sub 1 » sub 2"
					}
				};

				// act
				var result = relationship.CalcRunCost(true, 2);

				// assert
				// (2 subs * 4c per) = 8
				// (3 str to raise, 2c per 3 str) = 2
				// * 2 runs = 20
				// + source cost = 37 / 2 = 18.5
				Assert.AreEqual("18.5", result);
			}
		}
	}
}
