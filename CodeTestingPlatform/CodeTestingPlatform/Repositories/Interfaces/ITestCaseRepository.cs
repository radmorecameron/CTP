using CodeTestingPlatform.DatabaseEntities.Local;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Repositories.Interfaces {
    public interface ITestCaseRepository {
        Task<TestCase> FindByIdAsync(int id);
        Task<List<TestCase>> ListAsync(int? signatureId);
        Task CreateAsync(TestCase testCase);
        Task UpdateAsync(TestCase testCase);
        Task DeleteAsync(TestCase testCase);
        Task<bool> ExistsAsync(int id);
        Task<TestCase> FindBySigIdAndNameAsync(int signatureId, string testCaseName);
    }
}
