using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sau.Mackey.NR.Contracts
{
	/// <summary>
	/// A Card for Netrunner
	/// </summary>
	public partial class Card : IMackeyEntity
	{
		public Guid DbId { get; set; }

		public string Set { get; set; }
		public string Number { get; set; }
		public string Name { get; set; }
		public string Faction { get; set; }
		public string CardType { get; set; }
		public List<string> Keywords { get; internal set; }
		public int Cost { get; set; }
		public int Strength { get; set; }
		public int Influence { get; set; }

		public string Effect { get; set; }

		public List<Relationship> Relationships { get; internal set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Card"/> class.
		/// </summary>
		public Card()
		{
			Keywords = new List<string>();
			Relationships = new List<Relationship>();
		}

		[ExcludeFromCodeCoverage]
		public override string ToString()
		{
			var options = string.Empty;

			if (IsIcebreaker)
				options = ToString_IcebreakerOptions();

			return string.Format("{0}.{1} {2} - {3}:{4} [{5}: {6}] c:{7}{8}",
				Set ?? string.Empty,
				Number ?? string.Empty,
				Name,
				Faction != null ? Faction.Substring(0,2) : string.Empty,
				Influence,
				CardType ?? string.Empty,
				string.Join("|", Keywords),
				Cost,
				options);
		}
	}
}
