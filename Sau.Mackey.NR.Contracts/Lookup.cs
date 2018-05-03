using System;

namespace Sau.Mackey.NR.Contracts
{
	/// <summary>
	/// A lookup in the Netrunner system
	/// </summary>
	public class Lookup : IMackeyEntity
	{
		public Guid DbId { get; set; }
		public string LookupName { get; set; }
		public string Key { get; set; }
		public string Value { get; set; }
	}
}
