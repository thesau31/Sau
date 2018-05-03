using System.Linq;
using Sau.Mackey.NR.Contracts;

namespace Sau.Mackey.NR.Services.Interfaces
{
    /// <summary>
    /// A class for building relationships
    /// </summary>
    public interface IRelationshipBuilder
    {
        void BuildOneVsMany(Card source, IQueryable<Card> targets, RelationshipType type);
    }
}