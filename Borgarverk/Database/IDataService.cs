using System;
using System.Collections.Generic;

namespace Borgarverk
{
	public interface IDataService
	{
		IEnumerable<EntryModel> GetEntries();
		EntryModel GetEntry(int id);
		void DeleteEntry(int id);
		void AddEntry(EntryModel model);
	}
}
