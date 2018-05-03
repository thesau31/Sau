using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sau.Mackey.NR.Contracts
{
	/// <summary>
	/// An icebreaker relationship that also contains economic information
	/// </summary>
	public class IcebreakerEconomicRelationship : Relationship
	{
		private const string SubroutinePattern = @"(.*)([0-9]+)€: Break (code gate |barrier |sentry )subroutine(.*)";
		private const string SubroutinePatternUpTo = @"(.*)([0-9]+)€: Break up to ([0-9]+) (code gate |barrier |sentry )subroutines(.*)";
		public int CostToBreakSubroutine
		{
			get
			{
				var match = Regex.Match(Source.Effect, SubroutinePattern);
				if (match.Success
					&& Target.Keywords.Any(x => x.Equals(match.Groups[3].Value.Trim(), StringComparison.OrdinalIgnoreCase)))
					return int.Parse(match.Groups[2].Value);

				match = Regex.Match(Source.Effect, SubroutinePatternUpTo);
				if (match.Success
					&& Target.Keywords.Any(x => x.Equals(match.Groups[4].Value.Trim(), StringComparison.OrdinalIgnoreCase)))
					return int.Parse(match.Groups[2].Value);

				return 0;
			}
		}

		public int NumberOfTargetSubroutines
		{
			get { return Target.Effect.ToCharArray().Count(x => x == '»'); }
		}

		public int StrengthToRaise
		{
			get
			{
				var diff = Target.Strength - Source.Strength;
				return (diff >= 0)
					? diff
					: 0;
			}
		}

		public virtual string CalcRunCost(bool includeFirstRun, int numberOfRuns = 1)
		{
			if (StrengthToRaise > 0 && Source.StrengthRaisedPerCost == 0)
				return "n/a";

			double totalCost = 0;
			for (var i = 0; i < numberOfRuns; i++)
			{
				var cost = includeFirstRun && i == 0 ? Source.Cost : 0;
				cost = cost + (NumberOfTargetSubroutines * CostToBreakSubroutine);
				cost = StrengthToRaise == 0
					? cost
					: (cost + ((int)Math.Ceiling((double)StrengthToRaise / Source.StrengthRaisedPerCost) * Source.CostToRaiseStrength));
				totalCost += cost;
			}
			totalCost = Math.Round(totalCost / numberOfRuns, 2);

			return totalCost.ToString(CultureInfo.InvariantCulture);
		}

		[ExcludeFromCodeCoverage]
		public override string ToString()
		{
			var firstRun = CalcRunCost(true) != @"n/a" ? CalcRunCost(true) + "c" : "n/a";
			var nthRun = CalcRunCost(false) != @"n/a" ? CalcRunCost(false) + "c" : "n/a";
			var avgRun = CalcRunCost(true, 5) != @"n/a" ? CalcRunCost(true, 5) + "c" : "n/a";

			return string.Format("{0}{1}      r(1): {2} r(n): {3} r*5(avg): {4}",
				base.ToString(),
				Environment.NewLine,
				firstRun.PadLeft(6, ' '),
				nthRun.PadLeft(6, ' '),
				avgRun.PadLeft(6, ' ')
				);
		}
	}
}