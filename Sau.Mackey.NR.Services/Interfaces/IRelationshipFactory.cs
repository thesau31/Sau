using Sau.Mackey.NR.Contracts;

namespace Sau.Mackey.NR.Services.Interfaces
{
	public interface IRelationshipFactory
	{
		Relationship Create(RelationshipType type, Card source, Card target);
	}
}
