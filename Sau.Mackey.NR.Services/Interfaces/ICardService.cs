using Sau.Mackey.NR.Contracts;
using System;
using System.Collections.Generic;

namespace Sau.Mackey.NR.Services.Interfaces
{
	/// <summary>
	/// Service for handling card interactions
	/// </summary>
	public interface ICardService
	{
		/// <summary>
		/// Initializes the relationships.
		/// </summary>
		void InitializeRelationships();

		/// <summary>
		/// Gets the card.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Card GetCard(Guid id);

		/// <summary>
		/// Gets the card by set and card.
		/// </summary>
		/// <param name="set">The set.</param>
		/// <param name="card">The card.</param>
		/// <returns></returns>
		Card GetCardBySetAndCard(string set, string card);

		/// <summary>
		/// Gets the cards by keyword.
		/// </summary>
		/// <param name="keywords">The keywords.</param>
		/// <returns></returns>
		IEnumerable<Card> GetCardsByKeywords(IEnumerable<string> keywords);

		/// <summary>
		/// Gets the name of the set number by.
		/// </summary>
		/// <param name="set">The set.</param>
		/// <returns></returns>
		string GetSetNumberByName(string set);

		/// <summary>
		/// Bulk saves the cards
		/// </summary>
		/// <param name="cards">The cards.</param>
		void BulkSave(List<Card> cards);
	}
}