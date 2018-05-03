using System;
using Sau.Mackey.NR.Contracts;
using Sau.Mackey.NR.Services.Interfaces;

namespace Sau.Mackey.NR.Services.Relationships
{
	/// <summary>
	/// Factory that is responsible for creating relationship objects
	/// </summary>
	public class RelationshipFactory : IRelationshipFactory
	{
		/// <summary>
		/// Creates the specified relationship.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="source">The source.</param>
		/// <param name="target">The target.</param>
		/// <returns></returns>
		public Relationship Create(RelationshipType type, Card source, Card target)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (target == null) throw new ArgumentNullException("target");

			var relationship = type == RelationshipType.Breaks 
				? new IcebreakerEconomicRelationship() 
				: new Relationship();

			relationship.Type = type;
			relationship.Source = source;
			relationship.Target = target;

			return relationship;
		}
	}
}
