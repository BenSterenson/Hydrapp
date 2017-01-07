using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Plugin.Connectivity;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Hydrapp.Client.Modules;

namespace Hydrapp.Client.Services
{
    public class AzureDBService : IService
    {
        public MobileServiceClient mobileService { get; set; }
        private IMobileServiceSyncTable<TestItem> testItemTable;
        bool isInit;
        
        public async Task Initialize()
        {
            if (isInit)
                return;

            // create the client
            mobileService = new MobileServiceClient("https://hydrapp.azurewebsites.net", null)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings()
                {
                    CamelCasePropertyNames = true
                }
            };
           
            try
            {
                var store = new MobileServiceSQLiteStore("test.db");
                store.DefineTable<TestItem>();
                await mobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
                testItemTable = mobileService.GetSyncTable<TestItem>();
                isInit = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("can't init Azure DB Service", e);
            }
            
        }

        public async Task SyncTable()
        {
            var connected = await Plugin.Connectivity.CrossConnectivity.Current.IsReachable("google.com",10000);
            if (connected == false)
                return;
            // I have connection
            try
            {
                await mobileService.SyncContext.PushAsync();
                await testItemTable.PullAsync("allTestItems", testItemTable.CreateQuery());
            }
            catch (Exception e)
            {
                Debug.WriteLine("unable to sync data although I'm Online. reason: ", e);
            }

        }
        public async Task<TestItem> addTestItem(string name, DateTime date)
        {
            await Initialize();
            TestItem item = new TestItem(name, date);
            try
            {
                await testItemTable.InsertAsync(item);
                await SyncTable();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Couldn't add Test Item", e);
                throw e;
            }
            return item;
        }

        public async Task<bool> deleteTestItem(TestItem item)
        {
            await Initialize();
            try
            {
                await testItemTable.DeleteAsync(item);
                await SyncTable();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Couldn't delete TestItem, exception: ", e);
                return false;
            }
            return true;
        }

        public Task<IEnumerable<TestItem>> getTestItems()
        {
            throw new NotImplementedException();
        }
    }
}
