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
        public MobileServiceClient mobileService = new MobileServiceClient("https://hydrapp.azurewebsites.net", null)
        {
            SerializerSettings = new MobileServiceJsonSerializerSettings()
            {
                CamelCasePropertyNames = true
            }
        };

        //public MobileServiceClient mobileService = new MobileServiceClient("https://hydrapp.azurewebsites.net");

        private IMobileServiceSyncTable<User> userTable;
        private bool isInit;
        
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
                var store = new MobileServiceSQLiteStore("Noam.db");
                store.DefineTable<User>();
                await mobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
                userTable = mobileService.GetSyncTable<User>();
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
                await userTable.PullAsync("allusers", userTable.CreateQuery());
            }
            catch (Exception e)
            {
                
                Debug.WriteLine("unable to sync some data", e);
                throw e;
            }
            
        }
        public async Task<User> addUser(User user)
        {
            await Initialize();
            try
            {
                await userTable.InsertAsync(user);
                await SyncTable();
                User result = await userTable.LookupAsync(user.Id);
                user.UserId = result.UserId;
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
        public async Task<bool> deleteUser(User user)
        {
            await Initialize();
            try
            {
                await userTable.DeleteAsync(user);
                await SyncTable();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Couldn't delete user: " + user + ", exception: ", e);
                return false;
            }
            return true;
        }

        public async Task<int> loginUser(string userName, string password)
        {
            List<int> userIDList = await mobileService.GetTable<User>().Where(user => user.userName == userName && user.password == password).Select(user => user.UserId).ToListAsync();
            if (userIDList.Count == 0)
            {
                return 0;
            }
            return userIDList.ElementAt(0);
        }

        public async Task<int> joinGroup(string userName, string groupID, string groupPassword)
        {
            // check if credentials are correct in Groups table (groupID + password)
            // insert new entry in GroupMembers table for this user
            
            return 0;
        }

        public async Task<int> createGroup(string userId, string groupName, string groupPassword)
        {
            // create new entry in group table
            // insert new entry in GroupMembers table for this ADMIN!

            return 0;
        }

    }
}
