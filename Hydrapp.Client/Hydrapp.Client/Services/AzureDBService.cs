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
            UserHydrateLvl userHydratelvl = new UserHydrateLvl(user.UserId);
            await mobileService.GetTable<UserHydrateLvl>().InsertAsync(userHydratelvl);
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

        public async Task<User> loginUser(string userName, string password)
        {
            List<User> userIDList = await mobileService.GetTable<User>().Where(user => user.userName == userName && user.password == password).ToListAsync();
            if (userIDList.Count == 0)
            {
                return null;
            }
            return userIDList.ElementAt(0);
        }
         
        public async Task<int> joinGroup(int userId, int groupID, string groupPassword)
        {
            // check if credentials are ok
            List<Group> result = await mobileService.GetTable<Group>().Where(group => group.GroupId == groupID && group.Password == groupPassword).ToListAsync();
            if (result.Count == 0)
            {
                return 0;
            }

            // check if the user already exist in group
            List<GroupMember> groupMembersList = await mobileService.GetTable<GroupMember>().Where(groupMember => groupMember.UserId == userId && groupMember.GroupId == groupID).ToListAsync();
            if (groupMembersList.Count > 0)
            {
                if (groupMembersList.ElementAt(0).Admin == true) //user is admin!
                {
                    return 2;
                }
                else // user is regular
                {
                    return 1;
                }
            }
            GroupMember newGroupMember = new GroupMember(userId, groupID, false);
            await mobileService.GetTable<GroupMember>().InsertAsync(newGroupMember);
            return 1;
        }

        public async Task<int> createGroup(int userId, string groupName, string groupPassword)
        {
            Group group = new Group(groupName, groupPassword);
            await mobileService.GetTable<Group>().InsertAsync(group);
            GroupMember groupMember = new GroupMember(userId, group.GroupId, true);
            await mobileService.GetTable<GroupMember>().InsertAsync(groupMember);
            return group.GroupId;
        }

        public async Task<int> createActivity(int Lvl, int groupId)
        {
            Activity activity = new Activity();
            activity.ActivityLvl = Lvl;
            activity.ActivityTime = DateTime.Now;
            activity.GroupId = groupId;
            await mobileService.GetTable<Activity>().InsertAsync(activity);
            return activity.ActivityId;
        }

        public async Task<int> updateUserHydrateAvg(int activityId, List<int> userIds)
        {
            List<Activity> activityList = await mobileService.GetTable<Activity>().Where(x => x.ActivityId == activityId).ToListAsync();
            Activity activity = activityList.ElementAt(0);
            try
            {
                foreach (int userId in userIds)
                {
                    bool dehydrated = false;
                    TimeSpan dehydrationTime;
                    List<BandEntry> result = await mobileService.GetTable<BandEntry>().Where(bandEntry => bandEntry.UserId == userId).OrderBy(entry => entry.TimeStamp).ToListAsync();
                    foreach (BandEntry entry in result)
                    {
                        if (entry.IsDehydrated && entry.TimeStamp > activity.ActivityTime)
                        {
                            dehydrated = true;
                            dehydrationTime = entry.TimeStamp - activity.ActivityTime;
                            break;
                        }
                    }
                    if (dehydrated)
                    {
                        long currentAvg = 0;
                        int currentActivityCount = 0;
                        List<UserHydrateLvl> userHydrateList = await mobileService.GetTable<UserHydrateLvl>().Where(UserHydrateLvl => UserHydrateLvl.UserId == userId).ToListAsync();
                        UserHydrateLvl userHydrateLvl = userHydrateList.ElementAt(0);
                        switch (activity.ActivityLvl) {
                            case 1: currentAvg = userHydrateLvl.Lvl1Avg;
                                    currentActivityCount = userHydrateLvl.Lvl1Count;
                                    break;
                            case 2: currentAvg = userHydrateLvl.Lvl2Avg;
                                    currentActivityCount = userHydrateLvl.Lvl2Count;
                                    break;
                            case 3: currentAvg = userHydrateLvl.Lvl3Avg;
                                    currentActivityCount = userHydrateLvl.Lvl3Count;
                                    break;
                        }
                        currentAvg = ((currentAvg * currentActivityCount) + dehydrationTime.Ticks) / (currentActivityCount + 1);
                        switch (activity.ActivityLvl)
                        {
                            case 1:
                                userHydrateLvl.Lvl1Avg = currentAvg;
                                userHydrateLvl.Lvl1Count = currentActivityCount + 1;
                                break;
                            case 2:
                                userHydrateLvl.Lvl2Avg = currentAvg;
                                userHydrateLvl.Lvl2Count = currentActivityCount + 1;
                                break;
                            case 3:
                                userHydrateLvl.Lvl3Avg = currentAvg;
                                userHydrateLvl.Lvl3Count = currentActivityCount + 1;
                                break;
                        }
                        await mobileService.GetTable<UserHydrateLvl>().UpdateAsync(userHydrateLvl);
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return 1;
        }
        
        public async Task<int> addBandEntry(BandEntry bandEntry)
        {
            try
            {
                await mobileService.GetTable<BandEntry>().InsertAsync(bandEntry);
            }
            catch (Exception e)
            {
                return -1;
            }
            return 1;
        }

        public async Task<List<User>> getNewMembers(List<int> currentMembersList, int groupId)
        {
            List<User> newUsers = new List<User>();
            List<int> newUsersIds = new List<int>();
            List<int> dbMembersIds = await mobileService.GetTable<GroupMember>().Where(groupMember => groupMember.GroupId == groupId && groupMember.Admin == false).Select(groupMember => groupMember.UserId).ToListAsync();
            foreach (var memberId in dbMembersIds)
            {
                if (!currentMembersList.Contains(memberId)) // new member
                {
                    newUsersIds.Add(memberId);
                }
            }
            if (newUsersIds.Count > 0)
            {
                newUsers = await mobileService.GetTable<User>().Where(user => newUsersIds.Contains(user.UserId)).ToListAsync();
            }
            return newUsers;
        }

        public async Task<int> getBandIdForUserId(int userId, int groupId, string bandName)
        {
            var bandId = 0;
            List<Band> bandsList;
            try
            {
                bandsList = await mobileService.GetTable<Band>().Where(band => band.UserId == userId).ToListAsync();
            }
            catch (Exception e)
            {
                return -1;
            }
            if (bandsList.Count > 0) // this user already have bandId
            {
                bandId = bandsList.ElementAt(0).BandId;
            }
            else // adding Band to DB
            {
                var newBand = new Band(userId, bandId, bandName);
                await mobileService.GetTable<Band>().InsertAsync(newBand);
                bandId = newBand.BandId;
            }
            return bandId;
        }

        public async Task<BandEntry> getLatestBandEntryForUser(int userId)
        {
            List<BandEntry> result = await mobileService.GetTable<BandEntry>()
                .Where(bandEntry => bandEntry.UserId == userId)
                .OrderByDescending(entry => entry.TimeStamp)
                .ToListAsync();
            if (result.Count > 0)
            {
                return result.ElementAt(0);
            }
            return null;
        }

        public async Task deleteBandEntriesForGroup(int groupId)
        {
            List<BandEntry> result = await mobileService.GetTable<BandEntry>().Where(bandEntry => bandEntry.GroupId == groupId).ToListAsync();
            foreach (var entry in result)
            {
                await mobileService.GetTable<BandEntry>().DeleteAsync(entry);
            }
        }

        public async Task groupLogout(int groupId, int activityId)
        {
            List<GroupMember> result = await mobileService.GetTable<GroupMember>().Where(groupMember => groupMember.GroupId == groupId && groupMember.Admin == false).ToListAsync();
            foreach (var groupMember in result)
            {
                await mobileService.GetTable<GroupMember>().DeleteAsync(groupMember);
            }
            await deleteBandEntriesForGroup(groupId);
            await finishActivitiesForGroup(groupId);
        }

        private async Task finishActivitiesForGroup(int groupId)
        {
            // we want to make sure every activity signed to this group will be finished, not only the current
            List<Activity> result = await mobileService.GetTable<Activity>().Where(activity => activity.GroupId == groupId && activity.Done == 0).ToListAsync();
            foreach (var activity in result)
            {
                activity.Done = 1;
                await mobileService.GetTable<Activity>().UpdateAsync(activity);
            }
        }

        public async Task updateUser(User user)
        {
            List<User> result = await mobileService.GetTable<User>().Where(x => x.UserId == user.UserId).ToListAsync();
            user.Id = result.ElementAt(0).Id;
            await mobileService.GetTable<User>().UpdateAsync(user);
        }

        public async Task<List<BandEntry>> getBandEntriesForUser(User user)
        {
            List<BandEntry> result = await mobileService.GetTable<BandEntry>().Where(bandEntry => bandEntry.UserId == user.UserId).OrderByDescending(entry => entry.TimeStamp).ToListAsync();
            return result;
        }

        public async Task<int> getActivityForGroup(int groupId)
        {
            List<Activity> result = await mobileService.GetTable<Activity>().Where(activity => activity.GroupId == groupId && activity.Done == 0).ToListAsync();
            if (result.Count > 0)
            {
                return result.ElementAt(0).ActivityId;
            }
            return -1;
        }

        public async Task<long> getDehydrateAVGForUser(int userId, int activityLvl)
        {
            List<UserHydrateLvl> result = await mobileService.GetTable<UserHydrateLvl>().Where(x => x.UserId == userId).ToListAsync();
            long ticks;
            switch (activityLvl)
            {
                case 1:
                    return result.ElementAt(0).Lvl1Avg;
                case 2:
                    return result.ElementAt(0).Lvl2Avg;
                case 3:
                    return result.ElementAt(0).Lvl3Avg;
            }
            return -1;
        }
        
    }
}
