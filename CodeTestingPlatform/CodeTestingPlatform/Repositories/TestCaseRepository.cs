using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeTestingPlatform.Repositories {
    public class TestCaseRepository : GenericRepository<TestCase>, ITestCaseRepository {
        private readonly IMethodSignatureRepository _methodSignatureRepository;

        public TestCaseRepository(CTP_TESTContext context, IMethodSignatureRepository methodSignatureRepository) : base(context) {
            _methodSignatureRepository = methodSignatureRepository;
        }

        public async Task<List<TestCase>> ListAsync(int? signatureId = null) {
            List<TestCase> result = await _context.TestCases
                .Include(tc => tc.MethodSignature)
                    .ThenInclude(s => s.Activity)
                        .ThenInclude(a => a.Course)
                .Include(tc => tc.MethodSignature)
                    .ThenInclude(s => s.Activity)
                        .ThenInclude(a => a.Language)
                .Include(tc => tc.Results)
                .Include(tc => tc.Parameters.OrderBy(p => p.SignatureParameter.ParameterPosition))
                    .ThenInclude(p => p.SignatureParameter)
                        .ThenInclude(p => p.DataType)
                .Include(tc => tc.TestCaseException!)
                    .ThenInclude(e => e.Exception)
                .Where(tc => signatureId == null || tc.MethodSignatureId == signatureId)
                .ToListAsync();
            return result;
        }

        public async Task<TestCase> FindByIdAsync(int id) {
            var testCase = await _context.TestCases
                        .Include(tc => tc.MethodSignature)
                            .ThenInclude(s => s.Activity)
                                 .ThenInclude(a => a.Course)
                .Include(tc => tc.MethodSignature)
                    .ThenInclude(s => s.Activity)
                        .ThenInclude(a => a.Language)
                .Include(tc => tc.Parameters.OrderBy(p => p.SignatureParameter.ParameterPosition))
                    .ThenInclude(p => p.SignatureParameter)
                        .ThenInclude(p => p.DataType)
                .Include(tc => tc.Results)
                .Include(tc => tc.TestCaseException!)
                    .ThenInclude(e => e.Exception)
                .FirstOrDefaultAsync(tc => tc.TestCaseId == id);
            return testCase;
        }

        public async Task CreateAsync(TestCase testCase) {
            _context.TestCases.Add(testCase);
            await _context.SaveChangesAsync();
            await _methodSignatureRepository.UpdateDate(testCase.MethodSignatureId);
        }

        public async Task DeleteAsync(TestCase testCase) {
            _context.TestCases.Remove(testCase);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TestCase testCase) {
            _context.TestCases.Update(testCase);
            await _context.SaveChangesAsync();
            await _methodSignatureRepository.UpdateDate(testCase.MethodSignatureId);
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _context.TestCases.AnyAsync(e => e.TestCaseId == id);
        }

        public async Task<TestCase> FindBySigIdAndNameAsync(int signatureId, string testCaseName) {
            return await _context.TestCases.Where(e => e.MethodSignatureId == signatureId && e.TestCaseName.ToLower() == testCaseName.ToLower()).FirstOrDefaultAsync();
        }
    }
}
