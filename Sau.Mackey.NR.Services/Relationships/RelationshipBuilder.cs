using System;
using System.Linq;
using Sau.Mackey.NR.Contracts;
using Sau.Mackey.NR.Services.Interfaces;

namespace Sau.Mackey.NR.Services.Relationships
{
	/// <summary>
	/// A class for building relationships
	/// </summary>
	public class RelationshipBuilder : IRelationshipBuilder
	{
		private readonly IRelationshipFactory _relationshipFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="RelationshipBuilder"/> class.
		/// </summary>
		/// <param name="relationshipFactory">The relationship factory.</param>
		public RelationshipBuilder(IRelationshipFactory relationshipFactory)
		{
			_relationshipFactory = relationshipFactory;
		}

		/// <summary>
		/// Builds relationships for one targeting many.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="targets">The targets.</param>
		/// <param name="type">The type.</param>
		public void BuildOneVsMany(Card source, IQueryable<Card> targets, RelationshipType type)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (targets == null) throw new ArgumentNullException("targets");

			var relationships = targets.Select(target => 
				_relationshipFactory.Create(type, source, target));
			source.Relationships.AddRange(relationships);
		}
	}
}
