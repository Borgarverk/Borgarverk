using System;
using System.Collections.Generic;
using Borgarverk.Models;
using Newtonsoft.Json;

namespace Borgarverk
{
	public class SendService : ISendService
	{
		public SendService()
		{
		}

		public string EntryToJSon(EntryModel entry)
		{
			var json = JsonConvert.SerializeObject(entry);
			return json;
		}

		public string EntriesToJson(List<EntryModel> entries)
		{
			var json = JsonConvert.SerializeObject(entries);
			return json;
		}

		public bool SendEntry(EntryModel entry)
		{
			entry.TimeSent = DateTime.Now;
			entry.Sent = true;
			return false;
		}

		public bool SendEntries(List<EntryModel> entries)
		{
			return false;
		}
	}
}
