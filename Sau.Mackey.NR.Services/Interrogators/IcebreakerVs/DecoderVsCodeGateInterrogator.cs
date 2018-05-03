using System.Diagnostics.CodeAnalysis;
using Sau.Mackey.NR.Services.Interfaces;

namespace Sau.Mackey.NR.Services.Interrogators.IcebreakerVs
{
	/// <summary>
	/// Builds relationships between decoder icebreakers and code gate ice
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class DecoderVsCodeGateInterrogator : IcebreakerVsRelationshipInterrogator
	{
		public override string IcebreakerKeyword
		{
			get { return "Decoder"; }
		}

		public override string IceKeyword
		{
			get { return "Code Gate"; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DecoderVsCodeGateInterrogator"/> class.
		/// </summary>
		/// <param name="relationshipBuilder">The builder helper.</param>
		public DecoderVsCodeGateInterrogator(IRelationshipBuilder relationshipBuilder)
			: base(relationshipBuilder) { }
	}
}
