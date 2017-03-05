using System;
using System.Collections.Generic;
using Borgarverk.Models;

namespace Borgarverk
{
	public interface ISendService
	{
		string EntryToJSon(EntryModel entry);
		string EntriesToJson(List<EntryModel> entries);
		bool SendEntry(EntryModel entry);
		bool SendEntries(List<EntryModel> entries);
	}
}
