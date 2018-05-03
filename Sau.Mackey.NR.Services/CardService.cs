using Sau.Mackey.NR.Contracts;
using Sau.Mackey.NR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sau.Mackey.NR.Services
{
	/// <summary>
	/// Service for handling card interactions
	/// </summary>
	public class CardService : ICardService
	{
		private readonly IRepository _repository;
		private readonly List<IInterrogator> _interrogators;

		/// <summary>
		/// Initializes a new instance of the <see cref="CardService" /> class.
		/// </summary>
		/// <param name="repository">The repository.</param>
		/// <param name="interrogators">The interrogators.</param>
		/// <exception cref="System.ArgumentNullException">repository
		/// or
		/// interrogators</exception>
		public CardService(IRepository repository, List<IInterrogator> interrogators)
		{
			if (repository == null) throw new ArgumentNullException("repository");
			if (interrogators == null) throw new ArgumentNullException("interrogators");

			_repository = repository;
			_interrogators = interrogators;
		}

		/// <summary>
		/// Initializes the relationships.
		/// </summary>
		public void InitializeRelationships()
		{
			var cards = _repository.GetAll<Card>();

			foreach (var card in cards)
				card.Relationships.RemoveAll(x => true);

			foreach (var interrogator in _interrogators)
				interrogator.Interrogate(cards);
		}

		/// <summary>
		/// Gets the card.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public Card GetCard(Guid id)
		{
			return _repository.GetById<Card>(id);
		}

		/// <summary>
		/// Gets the card by set and card.
		/// </summary>
		/// <param name="set">The set.</param>
		/// <param name="card">The card.</param>
		/// <returns></returns>
		public Card GetCardBySetAndCard(string set, string card)
		{
			if (set == null) throw new ArgumentNullException("set");
			if (card == null) throw new ArgumentNullException("card");

			return _repository.GetAll<Card>().FirstOrDefault(x =>
				GetSetNumberByName(x.Set) == set
				&& x.Number.TrimStart(new[] { '0' }) == card);
		}

		/// <summary>
		/// Gets the cards by keyword.
		/// </summary>
		/// <param name="keywords">The keywords.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">keyword</exception>
		public IEnumerable<Card> GetCardsByKeywords(IEnumerable<string> keywords)
		{
			if (keywords == null) throw new ArgumentNullException("keywords");

			var cards = _repository.GetAll<Card>();

			return cards.Where(card => 
				keywords.All(keyword => card.Keywords.Contains(keyword)));
		}

		/// <summary>
		/// Gets the name of the set number by.
		/// </summary>
		/// <param name="set">The set.</param>
		/// <returns></returns>
		public string GetSetNumberByName(string set)
		{
			if (set == null) throw new ArgumentNullException("set");

			return GetLookupValue("Sets", set);
		}

		/// <summary>
		/// Bulk saves the cards
		/// </summary>
		/// <param name="cards">The cards.</param>
		public void BulkSave(List<Card> cards)
		{
			if (cards == null) throw new ArgumentNullException("cards");
			if (cards.Any(x => x == null)) throw new InvalidOperationException();

			foreach (var card in cards)
				_repository.Save(card);
		}

		internal string GetLookupValue(string lookupName, string key)
		{
			if (lookupName == null) throw new ArgumentNullException("lookupName");
			if (key == null) throw new ArgumentNullException("key");

			var lookup = _repository.GetAll<Lookup>()
				.Where(x => x.LookupName == lookupName)
				.FirstOrDefault(x => x.Key == key);

			return lookup != null
				? lookup.Value
				: null;
		}
	}
}
