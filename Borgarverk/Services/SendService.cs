using System;
using System.Collections.Generic;
using Borgarverk.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

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

		/*
		 * var client = new HttpClient();
			var response = await client.GetAsync(new Uri("http://apis.is/petrol"));
			if (response.IsSuccessStatusCode)
			{
				var jsonString = response.Content.ReadAsStringAsync();
				jsonString.Wait();
				var result = GetSingleStations(JsonConvert.DeserializeObject<RootObject>(jsonString.Result));
				return result;
			}
			return null;
			*/

		public string EntriesToJson(List<EntryModel> entries)
		{
			var json = JsonConvert.SerializeObject(entries);
			return json;
		}

		public async Task<Boolean> SendEntry(EntryModel entry)
		{
			entry.TimeSent = DateTime.Now;
			entry.Sent = true;
			var myContent = JsonConvert.SerializeObject(entry);
			var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
			var byteContent = new ByteArrayContent(buffer);
			byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			var client = new HttpClient();
			var result = client.PostAsync("http://www.mbl.is", byteContent).Result;
			//string sendString = EntryToJSon(entry);
			//var stringContent = new StringContent(sendString);
			//var response = await client.PostAsync("http://www.mbl.is/", stringContent);
			////System.Diagnostics.Debug.WriteLine(entry.ToString());
			return false;
		}

		public bool SendEntries(List<EntryModel> entries)
		{
			foreach (var entry in entries)
			{
				entry.TimeSent = DateTime.Now;
				entry.Sent = true;
			}

			string sendString = EntriesToJson(entries);

			return true;
		}
	}
}
