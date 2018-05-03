using System.Linq;
using Sau.Mackey.NR.Contracts;
using Sau.Mackey.NR.Services.Interfaces;
using System;
using System.Linq.Expressions;

namespace Sau.Mackey.NR.Services.Interrogators
{
	/// <summary>
	/// Abstract class for interrogating two way relationships
	/// </summary>
	public abstract class TwoWayRelationshipInterrogator : IInterrogator
	{
		private readonly IRelationshipBuilder _relationshipBuilder;

		/// <summary>
		/// Expression for getting the source card(s)
		/// </summary>
		/// <value>
		/// The source card expression.
		/// </value>
		protected abstract Expression<Func<Card, bool>> SourceCardExpression { get; }
		
		/// <summary>
		/// Expression for getting the target card(s)
		/// </summary>
		/// <value>
		/// The target card expression.
		/// </value>
		protected abstract Expression<Func<Card, bool>> TargetCardExpression { get; }
		
				/// <summary>
		/// Initializes a new instance of the <see cref="TwoWayRelationshipInterrogator"/> class.
		/// </summary>
		/// <param name="relationshipBuilder">The builder helper.</param>
		protected TwoWayRelationshipInterrogator(IRelationshipBuilder relationshipBuilder)
		{
			if (relationshipBuilder == null) throw new ArgumentNullException("relationshipBuilder");

			_relationshipBuilder = relationshipBuilder;
		}

		/// <summary>
		/// Interrogates the specified cards to find relationships.
		/// </summary>
		/// <param name="cards">The cards.</param>
		public virtual void Interrogate(IQueryable<Card> cards)
		{
			if (cards == null) throw new ArgumentNullException("cards");

			var sourceCards = cards.Where(SourceCardExpression);
			var targetCards = cards.Where(TargetCardExpression);

			foreach (var source in sourceCards)
				_relationshipBuilder.BuildOneVsMany(source, targetCards, RelationshipType.Breaks);

			foreach (var target in targetCards)
				_relationshipBuilder.BuildOneVsMany(target, sourceCards, RelationshipType.IsBrokenBy);
		}
	}
}