using System.Diagnostics.CodeAnalysis;
using Sau.Mackey.NR.Services.Interfaces;

namespace Sau.Mackey.NR.Services.Interrogators.IcebreakerVs
{
	/// <summary>
	/// Builds relationships between Killer Icebreakers and Sentry Ice
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class KillerVsSentryInterrogator : IcebreakerVsRelationshipInterrogator
	{
		public override string IcebreakerKeyword
		{
			get { return "Killer"; }
		}

		public override string IceKeyword
		{
			get { return "Sentry"; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="KillerVsSentryInterrogator"/> class.
		/// </summary>
		/// <param name="relationshipBuilder">The builder helper.</param>
		public KillerVsSentryInterrogator(IRelationshipBuilder relationshipBuilder)
			: base(relationshipBuilder) { }
	}
}
