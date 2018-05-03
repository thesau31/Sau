using System.Linq;
using Sau.Mackey.NR.Contracts;

namespace Sau.Mackey.NR.Services.Interfaces
{
    /// <summary>
    /// Interrogator of cards to determine relationships
    /// </summary>
    public interface IInterrogator
    {
        /// <summary>
        /// Interrogates the specified cards to find relationships.
        /// </summary>
        /// <param name="cards">The cards.</param>
        void Interrogate(IQueryable<Card> cards);
    }
}