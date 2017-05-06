using System;
using System.Collections.Generic;
using Borgarverk.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace Borgarverk
{
	public class SendService : ISendService
	{
		// token: 56a88b8eb1f26a81cd4a8341b03ed5485a40ae5f

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
			// setup http client
			var client = new HttpClient(new HttpClientHandler
			{
				UseProxy = false
			});
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", "56a88b8eb1f26a81cd4a8341b03ed5485a40ae5f");
			Debug.WriteLine(client.DefaultRequestHeaders.ToString());

			// Get mobileId
			var getAddress = $"https://floti.trackwell.com/api/mobiles/{entry.Car}";

			Debug.WriteLine(getAddress);

			try
			{
				var response = await client.GetAsync(getAddress).ConfigureAwait(continueOnCapturedContext: false);
				if (response.IsSuccessStatusCode) 
				{
					var content = await response.Content.ReadAsStringAsync();
					Debug.WriteLine(content);
				}
				Debug.WriteLine(response.Content.ToString());
				Debug.WriteLine(response.Headers.ToString());

			}
			catch (Exception)
			{
				return false;
			}


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