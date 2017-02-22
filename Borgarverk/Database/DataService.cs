using System;
using System.Diagnostics;

namespace Borgarverk
{
	public class DataService : IDataService
	{
		public void AddEntry(EntryModel model)
		{
			Debug.WriteLine("Added Entry");
			return;
		}
	}
}
