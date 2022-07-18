using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class ResultService : IResultService {
        private readonly IResultRepository _repository;
        public ResultService(IResultRepository repository) {
            _repository = repository;
        }
        public async Task<Result> FindByIdAsync(int id) {
            return await _repository.FindOneAsync(predicate: r=>r.ResultId == id);
        }

        public async Task<IList<Result>> ListAsync(int codeUploadId) {

            return await _repository.GetAllAsync(
                predicate: r=>r.CodeUploadId == codeUploadId,
                orderBy: q=>q.OrderBy(r=>r.TestCaseId));
        }

        public async Task<IPagedList<Result>> ListAsync(int pageIndex, int pageSize) {

            return await _repository.GetListAsync(pageIndex: pageIndex, pageSize: pageSize);
        }

        public async Task CreateAsync(int codeUploadId, Result result) {
            _repository.Add(result);
            await _repository.SaveChangesAsync();
        }

        public async Task CreateAsync(int codeUploadId, params Result[] results) {

            foreach(Result result in results) {
                result.CodeUpload = null;
                result.TestCase = null;
            }

            var original_results = await _repository.GetAllAsync(predicate: r => r.CodeUploadId == codeUploadId);
            foreach (var result in original_results) {
                await _repository.DeleteAsync(result.ResultId);
            }
            _repository.AddRange(results);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Result result) {
            _repository.Update(result);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id) {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _repository.ExistsAsync(a => a.ResultId == id);
        }
    }
}
