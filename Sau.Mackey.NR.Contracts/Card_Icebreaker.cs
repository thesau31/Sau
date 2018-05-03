using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sau.Mackey.NR.Contracts
{
	public partial class Card
	{
		public bool IsIcebreaker { get { return Keywords.Contains("Icebreaker"); } }

		private const string StrengthPattern = @"(.*)([0-9]+)€: \+([0-9]+) strength(.*)";
		public int CostToRaiseStrength
		{
			get
			{
				var match = Regex.Match(Effect, StrengthPattern);
				if (match.Success)
					return int.Parse(match.Groups[2].Value);
				return 0;
			}
		}

		public int StrengthRaisedPerCost
		{
			get
			{
				var match = Regex.Match(Effect, StrengthPattern);
				if (match.Success)
					return int.Parse(match.Groups[3].Value);
				return 0;
			}
		}

		internal IEnumerable<IcebreakerEconomicRelationship> IcebreakerRelationships
		{
			get { return Relationships.OfType<IcebreakerEconomicRelationship>(); }
		}

		internal string OverallAverage(bool includeFirstRun, int numberOfRuns)
		{
			if (!IsIcebreaker) return string.Empty;

			var avg = Relationships.OfType<IcebreakerEconomicRelationship>()
				.Select(x => x.CalcRunCost(includeFirstRun, numberOfRuns))
				.Where(x => x != "n/a")
				.Select(double.Parse)
				.Average();
			return Math.Round(avg, 2).ToString(CultureInfo.InvariantCulture);
		}

		public string ToString_IcebreakerAggregation()
		{
			if (!IsIcebreaker) return string.Empty;

			var firstRun = IcebreakerRelationships.Any(x => x.CalcRunCost(true) != "n/a")
				? OverallAverage(true, 1) + "c"
				: "n/a";
			var nthRun = IcebreakerRelationships.Any(x => x.CalcRunCost(false) != "n/a")
				? OverallAverage(false, 1) + "c"
				: "n/a";
			var avgRun = IcebreakerRelationships.Any(x => x.CalcRunCost(true, 5) != "n/a")
				? OverallAverage(true, 5) + "c"
				: "n/a";

			return string.Format("AVG r(1): {0} r(n): {1} r*5(avg): {2}",
				firstRun.PadLeft(6, ' '),
				nthRun.PadLeft(6, ' '),
				avgRun.PadLeft(6, ' ')
				);
		}

		[ExcludeFromCodeCoverage]
		private string ToString_IcebreakerOptions()
		{
			if (!IsIcebreaker) return string.Empty;

			return string.Format(", s:{0}(+{1}/{2}c)", Strength, CostToRaiseStrength, StrengthRaisedPerCost);
		}
	}
}
