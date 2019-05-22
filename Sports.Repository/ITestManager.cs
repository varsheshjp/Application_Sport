using Sports.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Repository
{
    public interface ITestManager
    {
        Task DeleteTest(int Id);
        Task CreateTest(Test test);
        Task<DetailTestModel> GetTestDetails(int Id);
        Task<Test> GetTest(int Id);
        Task EditTest(Test test);
        Task<DashboardModel> GetTestList(string Id);
    }
}
