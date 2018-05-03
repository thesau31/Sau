using System.Collections.Generic;
using System.Linq;
using Ninject;
using Sau.Mackey.NR.Console.Ninject;
using Sau.Mackey.NR.Contracts;
using Sau.Mackey.NR.Services.Interfaces;
using Out = System.Console;

namespace Sau.Mackey.NR.Console
{
	static class Program
	{
		private const string BoldLine = @"===============================================================================";
		private const string Line = @"-------------------------------------------------------------------------------";
		private static readonly IKernel Container = new StandardKernel();
		private static readonly Bootstrapper Bootstrapper = new Bootstrapper();
		private static ICardService _service;

		// ReSharper disable once UnusedParameter.Local
		static void Main(string[] args)
		{
			Bootstrapper.Initialize(Container);

			// initialize
			_service = Container.Get<ICardService>();
			_service.InitializeRelationships();

			Out.Clear();

			Out.WriteLine(@"Netrunner Relationship Builder");
			Out.WriteLine(BoldLine);
			Out.WriteLine(@"""help"" for commands, ""clear"" to clear, ""exit"" to quit");
			Out.WriteLine(BoldLine);

			// wait for input
			WaitForInput();
		}

		private static void WaitForInput()
		{
			var input = string.Empty;

			while (input != "exit")
			{
				Out.Write(@"? ");
				input = System.Console.ReadLine();

				if (input == null) continue;

				switch (input)
				{
					case "help":
						ShowHelp();
						break;
					case "clear":
						Out.Clear();
						break;
					default:
						if (input.Contains(":"))
							RunQuery(input);
						break;
				}
			}
		}

		private static void RunQuery(string input)
		{
			var index = input.IndexOf(":", System.StringComparison.Ordinal);
			var command = input.Substring(0, index);
			var args = input.Substring(index + 1, input.Length - (index + 1)).Split(new[] { '.' });
			var cards = new List<Card>();

			switch (command)
			{
				case "c":
					cards.Add(QueryBySetAndCard(args[0], args[1]));
					break;
				case "k":
					cards.AddRange(QueryByKeyword(args));
					break;
			}

			foreach (var card in cards)
				DisplayCard(card, cards.Count == 1);

			Out.WriteLine();
			Out.WriteLine();
			Out.WriteLine();
		}

		private static Card QueryBySetAndCard(string setNumber, string cardNumber)
		{
			return _service.GetCardBySetAndCard(setNumber, cardNumber);
		}

		private static IEnumerable<Card> QueryByKeyword(string[] keywords)
		{
			return _service.GetCardsByKeywords(keywords);
		}

		private static void ShowHelp()
		{
			Out.Clear();
			Out.WriteLine(BoldLine);
			Out.WriteLine(@"""help"" => display this list");
			Out.WriteLine(@"""exit"" => quit the program");
			Out.WriteLine(BoldLine);
			Out.WriteLine(@"   Query Syntax  ");
			Out.WriteLine(BoldLine);
			Out.WriteLine(@"""c:xx.yyy"" => look up card (xx = set #, yyy = card #)");
			Out.WriteLine(@"""k:xxxxxxxx"" => look up cards by keyword (xxxxxxxx = keyword)");
			Out.WriteLine(BoldLine);
		}

		private static void DisplayCard(Card card, bool verbose)
		{
			if (card == null) return;

			Out.WriteLine();
			Out.WriteLine(card);

			if (verbose)
			{
				Out.WriteLine(Line);
				var relationships = card.Relationships
					.OrderBy(x => x.Type)
					.ThenBy(x => x.Target.Name);
				foreach (var relationship in relationships)
					Out.WriteLine(relationship);
			}

			if (card.IsIcebreaker)
			{
				Out.WriteLine(Line);
				Out.WriteLine(@"  {0}", card.ToString_IcebreakerAggregation());
			}

			if (verbose)
			{
				Out.WriteLine(Line);
				Out.WriteLine();
			}
		}
	}
}
