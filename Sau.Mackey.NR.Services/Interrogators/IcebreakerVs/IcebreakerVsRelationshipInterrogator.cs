using Sau.Mackey.NR.Contracts;
using Sau.Mackey.NR.Services.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Sau.Mackey.NR.Services.Interrogators.IcebreakerVs
{
	/// <summary>
	/// Builds relationships for icebreakers vs ice
	/// </summary>
	public abstract class IcebreakerVsRelationshipInterrogator : TwoWayRelationshipInterrogator
	{
		public abstract string IcebreakerKeyword { get; }
		public abstract string IceKeyword { get; }

		protected override Expression<Func<Card, bool>> SourceCardExpression
		{
			get { return card => card.Keywords.Any(x => x == IcebreakerKeyword); }
		}

		protected override Expression<Func<Card, bool>> TargetCardExpression
		{ 
			get { return card => card.Keywords.Any(x => x == IceKeyword); } 
		}

		protected IcebreakerVsRelationshipInterrogator(IRelationshipBuilder relationshipBuilder)
			: base(relationshipBuilder) { }
	}
}
