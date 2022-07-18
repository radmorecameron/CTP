using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models.Validation;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class TestCaseService : ITestCaseService {
        private readonly ITestCaseRepository _testCaseRepository;
        public TestCaseService(ITestCaseRepository testCaseRepository) {
            _testCaseRepository = testCaseRepository;
        }

        public async Task CreateAsync(TestCase testCase) {
            await _testCaseRepository.CreateAsync(testCase);
        }

        public async Task DeleteAsync(TestCase testCase) {
            await _testCaseRepository.DeleteAsync(testCase);
        }

        public async Task<TestCase> FindByIdAsync(int id) {
            return await _testCaseRepository.FindByIdAsync(id);
        }

        public async Task<List<TestCase>> ListAsync(int? signatureId) {
            return await _testCaseRepository.ListAsync(signatureId);
        }

        public async Task UpdateAsync(TestCase testCase) {
            await _testCaseRepository.UpdateAsync(testCase);
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _testCaseRepository.ExistsAsync(id);
        }

        public async Task<TestCase> FindBySigIdAndNameAsync(int signatureId, string testCaseName) {
            return await _testCaseRepository.FindBySigIdAndNameAsync(signatureId, testCaseName);
        }
    }
}
