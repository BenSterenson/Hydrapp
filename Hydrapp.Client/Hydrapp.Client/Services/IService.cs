using Hydrapp.Client.Modules;
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
        Task<int> loginUser(string userName, string password);
        Task<int> joinGroup(string userName, string groupID, string groupPassword);
        Task<int> createGroup(string userId, string groupName, string groupPassword);
    }
}
