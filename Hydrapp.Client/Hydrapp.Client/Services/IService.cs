﻿using Hydrapp.Client.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydrapp.Client.Services
{
    public interface IService
    {

        Task Initialize();
        Task SyncTable();
        Task<User> addUser(User user);
        Task<User> loginUser(string userName, string password);
        Task<int> joinGroup(int userId, int groupID, string groupPassword);
        Task<int> createGroup(int userId, string groupName, string groupPassword);
        Task<int> getBandIdForUserId(int userId, int groupId, string bandName);
        Task<int> addBandEntry(BandEntry bandEntry);
        Task<List<User>> getNewMembers(List<int> currentMembersList, int groupId);
        Task<BandEntry> getLatestBandEntryForUser(int userId);
        Task<List<BandEntry>> getBandEntriesForUser(User user);
        Task deleteBandEntriesForGroup(int groupId);
        Task groupLogout(int groupId, int activityId);
        Task updateUser(User user);
        Task<int> createActivity(int Lvl, int groupId);
        Task<int> updateUserHydrateAvg(int activityId, List<int> userIds);
        Task<int>  getActivityForGroup(int groupId);
        Task<long> getDehydrateAVGForUser(int userId, int activityLvl);
    }
}
