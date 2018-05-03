using System;
using System.Diagnostics.CodeAnalysis;

namespace Sau.Mackey.NR.Contracts
{
	/// <summary>
	/// The relationship between two cards
	/// </summary>
	public class Relationship : IMackeyEntity
	{
		public Guid DbId { get; set; }

		public Card Source { get; set; }
		public Card Target { get; set; }
		
		public RelationshipType Type { get; set; }

		[ExcludeFromCodeCoverage]
		public override string ToString()
		{
			return string.Format(" .. {0} {1}",
				SourceToTargetText.Strings[Type],
				Target.Name);
		}
	}
}
