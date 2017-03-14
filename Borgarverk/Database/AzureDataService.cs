using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Borgarverk.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Linq;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.IO;

namespace Borgarverk
{
	public class AzureDataService
	{
		private IMobileServiceSyncTable<EntryModel> entryTable;
		private static AzureDataService instance = new AzureDataService();

		public static AzureDataService Instance
		{
			get
			{
				return instance;
			}
		}

		public MobileServiceClient MobileService { get; private set;}

		/*public AzureDataService()
		{
			await Initialize();
		}*/

		public async Task Initialize()
		{
			string path = "borgarverk.db";
			path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

			this.MobileService = new MobileServiceClient("https://borgarverk.azurewebsites.net");
			
			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<EntryModel>();

  			await this.MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

			this.entryTable = this.MobileService.GetSyncTable<EntryModel>();
		}

		public async Task<ObservableCollection<EntryModel>> GetEntries()
		{
			await Initialize();
			await this.SyncEntries();
			IEnumerable<EntryModel> items = await this.entryTable.OrderByDescending(c => c.TimeCreated).ToEnumerableAsync();

			return new ObservableCollection<EntryModel>(items);
		}

		public async Task SyncEntries()
		{
			try
			{
				await entryTable.PullAsync("allEntries", this.entryTable.CreateQuery());
				await MobileService.SyncContext.PushAsync();
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"Sync Failed: {0}", e.Message);
			}
		}


		public async Task AddItemAsync(EntryModel item)
		{
			await Initialize();
			//await entryTable.InsertAsync(item);
			await MobileService.GetTable<EntryModel>().InsertAsync(item);

			await SyncEntries();
		}
	}
}
