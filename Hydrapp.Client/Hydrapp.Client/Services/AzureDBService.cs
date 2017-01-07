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
        private IMobileServiceSyncTable<User> userTable;
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
                var store = new MobileServiceSQLiteStore("Test4.db");
                store.DefineTable<TestItem>();
                //store.DefineTable<User>();
                await mobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
                testItemTable = mobileService.GetSyncTable<TestItem>();
                userTable = mobileService.GetSyncTable<User>();
                await testItemTable.PullAsync("allTestItems", testItemTable.CreateQuery());
                await userTable.PullAsync("allUsers", userTable.CreateQuery());
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
                
                Debug.WriteLine("unable to sync some data", e);
                throw e;
            }
            
        }
        public async Task<TestItem> addTestItem(TestItem item)
        {
            await Initialize();
            if (item.date.CompareTo(new DateTime()) == 0)
            {
                throw new Exception("Incompatible date");
            }
            try
            {
                await testItemTable.InsertAsync(item);
                await SyncTable();
            }
            catch (Exception e)
            {
                await testItemTable.DeleteAsync(item);
                await testItemTable.PullAsync("allTestItems", testItemTable.CreateQuery());
                Debug.WriteLine("Couldn't add Test Item:" + item ,  e);
                throw e;
            }
            return item;
        }

        public async Task<User> addUser(User user)
        {
            await Initialize();
            try
            {
                await userTable.InsertAsync(user);
                await SyncTable();
            }
            catch (Exception e)
            {
                await userTable.DeleteAsync(user);
                await userTable.PullAsync("allUsers", userTable.CreateQuery());
                Debug.WriteLine("Couldn't add user:" + user, e);
                throw e;
            }
            return user;
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
