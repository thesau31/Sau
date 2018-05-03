using System.Diagnostics.CodeAnalysis;
using Sau.Mackey.NR.Services.Interfaces;

namespace Sau.Mackey.NR.Services.Interrogators.IcebreakerVs
{
	/// <summary>
	/// Builds relationships between fracter icebreakers and barrier ice
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class FracterVsBarrierInterrogator : IcebreakerVsRelationshipInterrogator
	{
		public override string IcebreakerKeyword
		{
			get { return "Fracter"; }
		}

		public override string IceKeyword
		{
			get { return "Barrier"; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FracterVsBarrierInterrogator"/> class.
		/// </summary>
		/// <param name="relationshipBuilder">The builder helper.</param>
		public FracterVsBarrierInterrogator(IRelationshipBuilder relationshipBuilder)
			: base(relationshipBuilder) { }
	}
}
