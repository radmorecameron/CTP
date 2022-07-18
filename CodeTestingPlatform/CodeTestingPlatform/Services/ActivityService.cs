using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Models.Validation;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class ActivityService : IActivityService {
        private readonly IActivityRepository _activityRepository;
        public ActivityService(IActivityRepository activeRepository) {
            _activityRepository = activeRepository;
        }

        public async Task<Activity> FindByIdAsync(int id) {
            return await _activityRepository.FindOneAsync(
                predicate: a => a.ActivityId == id,
                include: q => q.Include(a => a.ActivityType)
                    .Include(a => a.CodeUploads)
                        .ThenInclude(c => c.Results)
                .Include(a => a.Course)
                .Include(a => a.Language)
                .Include(a => a.MethodSignatures)
                    .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                        .ThenInclude(a => a.DataType)
                        .AsNoTracking()
                .Include(a => a.MethodSignatures)
                    .ThenInclude(a => a.ReturnType)
                .Include(s => s.MethodSignatures)
                    .ThenInclude(s => s.TestCases)
                        .ThenInclude(e => e.TestCaseException)
                            .ThenInclude(e => e.Exception)
                .Include(a => a.MethodSignatures)
                    .ThenInclude(s => s.TestCases)
                        .ThenInclude(s => s.Parameters.OrderBy(s => s.SignatureParameter.ParameterPosition))
                            .ThenInclude(s => s.SignatureParameter)
                                .ThenInclude(p => p.DataType));
        }

        public async Task<IList<Activity>> ListAsync(int? courseId = null) {

            return await _activityRepository.GetAllAsync(
                predicate: courseId.HasValue ? (a => a.CourseId == courseId) : null,
                orderBy: q => q.OrderBy(a => a.ActivityId),
                include: source => source.Include(a => a.ActivityType)
                .Include(a => a.Course)
                .Include(a => a.Language)
                .Include(a => a.MethodSignatures)
                    .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                        .ThenInclude(a => a.DataType)
                .Include(a => a.MethodSignatures)
                    .ThenInclude(a => a.ReturnType)
                .Include(a => a.MethodSignatures)
                    .ThenInclude(s => s.TestCases)
                        .ThenInclude(s => s.Parameters.OrderBy(s => s.SignatureParameter.ParameterPosition))
                            .ThenInclude(s => s.SignatureParameter)
                                .ThenInclude(p => p.DataType));
        }

        public async Task<IPagedList<Activity>> ListAsync(int pageIndex, int pageSize, int? courseId = null) {

            return await _activityRepository.GetListAsync(
                pageIndex: pageIndex, pageSize: pageSize,
                predicate: courseId.HasValue ? (a => a.CourseId == courseId) : null,
                orderBy: q => q.OrderBy(a => a.ActivityId),
                include: q => q.Include(a => a.ActivityType)
                                .Include(a => a.Course)
                                .Include(a => a.Language)
                                .Include(a => a.MethodSignatures)
                                    .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                                        .ThenInclude(a => a.DataType));
        }

        public async Task CreateAsync(Activity activity) {
            _activityRepository.Add(activity);
            await _activityRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Activity activity) {
            _activityRepository.Update(activity);
            await _activityRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id) {
            await _activityRepository.DeleteAsync(id);
            await _activityRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _activityRepository.ExistsAsync(a => a.ActivityId == id);
        }

        public Dictionary<int, List<string>> FindInvalidTestCases(IEnumerable<MethodSignature> methodSignatures, out Dictionary<int, int> invalidSignatures) {
            invalidSignatures = new();
            Dictionary<int, List<string>> invalidTestCases = new();
            foreach (MethodSignature signature in methodSignatures) {
                var testCaseErrors = 0;
                foreach (TestCase tc in signature.TestCases) {
                    if (tc.ValidateTestCase) {
                        bool isValid = true;
                        List<string> errorMessages = new();
                        foreach (Parameter param in tc.Parameters) {
                            if (param.Value == null && param.SignatureParameter.RequiredParameter) { // If param is missing and signature parameter is required
                                isValid = false;
                                errorMessages.Add($"{param.SignatureParameter.ParameterName}: Missing value for required field. <br> ");
                            } else if (!ValueDataTypeValidator.CheckParamDataType(param.Value, param.SignatureParameter.DataType.DataType1) && param.SignatureParameter.RequiredParameter) { // If Param does not follow correct DataType
                                isValid = false;
                                errorMessages.Add($"{param.SignatureParameter.ParameterName}: Doesn't match Data Type ({param.SignatureParameter.DataType.DataType1}). <br> ");
                            }
                        }
                        if (!ValueDataTypeValidator.CheckParamDataType(tc.ExpectedValue, tc.MethodSignature.ReturnType.DataType1)) {
                            isValid = false;
                            errorMessages.Add($"Expected Result: Doesn't match Data Type ({tc.MethodSignature.ReturnType.DataType1}). <br> ");
                        }
                        if (!isValid) {
                            testCaseErrors++;
                            invalidTestCases.Add(tc.TestCaseId, errorMessages);
                        }
                    }
                }
                if (testCaseErrors > 0)
                    invalidSignatures.Add(signature.SignatureId, testCaseErrors);
            }
            return invalidTestCases;
        }

        public async Task<IPagedList<Activity>> Demo(int pageIndex, int pageSize, int? courseId = null, string term = "") {

            //int? courseId = null;
            //string term = "";

            /*--- Find One ---*/
            // Demo about Find one with projection
            var projection_item = await _activityRepository.FindOneAsync(a => new { Name = a.Title, Language = a.Language.LanguageName }, predicate: x => x.Title.Contains(term));

            // Demo about Find one with include
            Activity item = await _activityRepository.FindOneAsync(
                predicate: x => x.Title.Contains(term),
                include: source => source.Include(b => b.MethodSignatures).ThenInclude(s => s.ReturnType));

            // Demo about Find one  without include");
            item = await _activityRepository.FindOneAsync(predicate: x => x.Title.Contains(term), orderBy: source => source.OrderByDescending(b => b.ActivityId));

           
            /*--- Get All ---*/
            // Demo about Get all with projection
            var projection_itmes = await _activityRepository.GetAllAsync(a => new { Name = a.Title, Language = a.Language.LanguageName });

            // Demo about Get all with predicate
            IList<Activity> itmes = await _activityRepository.GetAllAsync(predicate: x => x.Title.Contains(term));

            // Demo about Get all with predicate, Include, ThenInclude and orderBy
            itmes = await _activityRepository.GetAllAsync(
                predicate: courseId.HasValue ? (a => a.CourseId == courseId) : null,
                orderBy: q => q.OrderBy(a => a.ActivityId),
                include: q => q.Include(a => a.ActivityType)
                                .Include(a => a.Course)
                                .Include(a => a.Language)
                                .Include(a => a.MethodSignatures)
                                    .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                                        .ThenInclude(a => a.DataType));


            /*--- Get Paged List ---*/
            // Demo about Get paged list with projection (default: pageIndex 0, pageSize 20)
            var projection_items = await _activityRepository.GetListAsync(a => new { Name = a.Title, Language = a.Language.LanguageName });

            // Demo about Get Paged List with predicate (default: pageIndex 0, pageSize 20)
            IPagedList<Activity> page = await _activityRepository.GetListAsync(predicate: x => x.Title.Contains(term));

            // Demo about Get Paged List with pageIndex, pageSize, predicate, Include, ThenInclude and orderBy
            //int pageIndex = 0;
            //int pageSize = 50;

            page = await _activityRepository.GetListAsync(
                pageIndex: pageIndex, pageSize: pageSize,
                predicate: courseId.HasValue ? (a => a.CourseId == courseId) : null,
                orderBy: q => q.OrderBy(a => a.ActivityId),
                include: q => q.Include(a => a.ActivityType)
                                .Include(a => a.Course)
                                .Include(a => a.Language)
                                .Include(a => a.MethodSignatures)
                                    .ThenInclude(a => a.SignatureParameters.OrderBy(s => s.ParameterPosition))
                                        .ThenInclude(a => a.DataType));

            return page;
        }
    }
}
