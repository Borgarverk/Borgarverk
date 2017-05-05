using System;
using System.Collections.Generic;
using Borgarverk.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Text;

namespace Borgarverk
{
	public class SendService : ISendService
	{
		// token: 56a88b8eb1f26a81cd4a8341b03ed5485a40ae5f;
		HttpClient client;

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

		public async Task<Boolean> SendEntry(EntryModel entry)
		{
			// Initializing the http client and adding authorization header
			client = new HttpClient(new HttpClientHandler
			{
				UseProxy = false
			});

			client.DefaultRequestHeaders.Add("Accept", "application/json");


			// GET request
			var address = $"https://floti.trackwell.com/api/mobiles/{entry.Car}";

			try
			{
				var response = await client.GetAsync(address).ConfigureAwait(continueOnCapturedContext: false);
				Debug.WriteLine(response.StatusCode);
			}
			catch (Exception)
			{
				return false;
			}


			// POST request
			entry.TimeSent = DateTime.Now;
			var myContent = JsonConvert.SerializeObject(entry);
			var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
			var byteContent = new ByteArrayContent(buffer);
			byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			try
			{
				var response = await client.PostAsync("https://httpbin.org/post", byteContent).ConfigureAwait(continueOnCapturedContext: false);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}