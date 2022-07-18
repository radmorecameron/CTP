﻿using CodeTestingPlatform.DatabaseEntities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services.Interfaces {
    public interface ITestCaseService {
        Task<TestCase> FindByIdAsync(int id);
        Task<List<TestCase>> ListAsync(int? signatureId=null);
        Task CreateAsync(TestCase testCase);
        Task UpdateAsync(TestCase testCase);
        Task DeleteAsync(TestCase testCase);
        Task<bool> ExistsAsync(int id);
        Task<TestCase> FindBySigIdAndNameAsync(int signatureId, string testCaseName);
    }
}
