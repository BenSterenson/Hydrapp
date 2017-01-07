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
        Task<IEnumerable<TestItem>> getTestItems();

        Task<TestItem> addTestItem(string name, DateTime date);

        Task<bool> deleteTestItem(TestItem id);



    }
}
