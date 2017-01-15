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
        Task<int> joinGroup(int userId, int groupID, string groupPassword);
        Task<int> createGroup(int userId, string groupName, string groupPassword);
    }
}
