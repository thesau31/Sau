using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sau.Mackey.NR.Contracts
{
	/// <summary>
	/// Lookup Dictionary for source to target text strings
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static class SourceToTargetText
	{
		public static readonly Dictionary<RelationshipType, string> Strings = new Dictionary<RelationshipType, string>
		{
			{ RelationshipType.Breaks, RelationshipStrings.Breaks },
			{ RelationshipType.IsBrokenBy, RelationshipStrings.IsBrokenBy }
		};
	}
}
