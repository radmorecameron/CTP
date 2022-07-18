using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeTestingPlatform.Repositories {
    public class MethodSignatureRepository : GenericRepository<MethodSignature>, IMethodSignatureRepository {
        public MethodSignatureRepository(CTP_TESTContext context) : base(context) {

        }

        public async Task<List<MethodSignature>> ListAsync(int? activityId) {
            IQueryable<MethodSignature> signatures = _context.MethodSignatures
                .Include(s => s.Activity)
                .Include(s => s.SignatureExceptions)
                    .ThenInclude(s => s.Exception)
                .Include(s => s.SignatureUserDefinedExceptions)
                    .ThenInclude(s => s.UserDefinedException)
                .Include(s => s.SignatureParameters.OrderBy(s => s.ParameterPosition))
                    .ThenInclude(s => s.DataType)
                .Include(s => s.TestCases)
                    .ThenInclude(e => e.TestCaseException)
                        .ThenInclude(e => e.Exception)
                .Include(s => s.TestCases)
                    .ThenInclude(s => s.Parameters.OrderBy(s => s.SignatureParameter.ParameterPosition))
                        .ThenInclude(s => s.SignatureParameter)
                            .ThenInclude(p => p.DataType);

            if (activityId.HasValue)
                signatures = signatures.Where(s => s.ActivityId == activityId);

            return await signatures
                        .OrderBy(s => s.ActivityId)
                        .ThenBy(s => s.MethodName)
                        .ToListAsync();
        }

        public async Task<MethodSignature> FindByIdAsync(int id) {
            var signature = await _context.MethodSignatures
                .Include(s => s.Activity).ThenInclude(a => a.Language)
                .Include(s => s.Activity).ThenInclude(a => a.Course)
                .Include(s => s.ReturnType)
                .Include(s => s.SignatureUserDefinedExceptions.OrderBy(e => e.UserDefinedException.UserDefinedExceptionName))
                    .ThenInclude(e => e.UserDefinedException)
                .Include(s => s.SignatureExceptions.OrderBy(e => e.Exception.ExceptionName))
                    .ThenInclude(e => e.Exception)
                .Include(s => s.TestCases)
                    .ThenInclude(e => e.TestCaseException)
                        .ThenInclude(e => e.Exception)
                .Include(s => s.TestCases)
                    .ThenInclude(s => s.Parameters.OrderBy(s => s.SignatureParameter.ParameterPosition))
                        .ThenInclude(s => s.SignatureParameter)
                            .ThenInclude(p => p.DataType)
                .Include(s => s.SignatureParameters.OrderBy(s => s.ParameterPosition)).ThenInclude(p => p.DataType)
                .FirstOrDefaultAsync(s => s.SignatureId == id);
            if (signature != null) {
                var parameters = signature.SignatureParameters.ToList();
                for (int i = 0; i < parameters.Count; i++) {
                    parameters[i].ParameterPosition = i;
                }
            }
            return signature;
        }

        public async Task<SignatureParameter> FindSignatureParameterByIdAsync(int id) {
            return await _context.SignatureParameters.Include(p => p.DataType)
                                                       .Include(p => p.MethodSignature)
                                                       .AsNoTracking()
                                                       .FirstOrDefaultAsync(s => s.SignatureParameterId == id);
        }

        public async Task CreateAsync(MethodSignature signature) {
            var parameters = signature.SignatureParameters.ToList();
            for (int i = 0; i < parameters.Count; i++) {
                parameters[i].ParameterPosition = i;
            }
            _context.MethodSignatures.Add(signature);
            await _context.SaveChangesAsync();
            await UpdateDate(signature.SignatureId);
        }

        public async Task UpdateAsync(MethodSignature signature) {
            var parameters = signature.SignatureParameters.ToList();

            for (int i = 0; i < parameters.Count; i++) {
                parameters[i].ParameterPosition = i; // Set parameters position
            }

            var original_signature = _context.MethodSignatures
                .Where(s => s.SignatureId == signature.SignatureId)
                .Include(s => s.SignatureParameters.OrderBy(p => p.ParameterPosition))
                    .ThenInclude(s => s.DataType)
                .SingleOrDefault();

            // Update signature
            _context.Entry(original_signature).CurrentValues.SetValues(signature);

            // Delete signature parameters not in new signature parameters
            foreach (var param in original_signature.SignatureParameters.ToList()) {
                if (!signature.SignatureParameters.Any(c => c.SignatureParameterId == param.SignatureParameterId))
                    _context.SignatureParameters.Remove(param);
            }

            // Update and Insert signature parameters
            foreach (var param in signature.SignatureParameters.ToList()) {
                var original_parameter = original_signature.SignatureParameters
                    .Where(p => p.SignatureParameterId == param.SignatureParameterId && p.SignatureParameterId != default(int))
                    .SingleOrDefault();

                if (original_parameter != null) {
                    // Update existing parameters
                    _context.Entry(original_parameter).CurrentValues.SetValues(param);
                } else {
                    // Insert new parameter
                    original_signature.SignatureParameters.Add(param);
                }
            }

            await _context.SaveChangesAsync();
            await UpdateDate(signature.SignatureId);
        }

        public async Task DeleteAsync(MethodSignature signature) {
            _context.SignatureParameters.RemoveRange(_context.SignatureParameters.Where(p => p.MethodSignatureId == signature.SignatureId));
            await _context.SaveChangesAsync();
            _context.MethodSignatures.Remove(signature);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) {
            return await _context.MethodSignatures.AnyAsync(e => e.SignatureId == id);
        }

        public async Task UpdateDate(int id) {
            MethodSignature signature = await _context.MethodSignatures.FindAsync(id);
            signature.LastUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<List<MethodSignature>> FindUpdatedSignatures(DateTime codeUploadDate) {
            return await _context.MethodSignatures.Where(s => s.LastUpdated > codeUploadDate).ToListAsync();
        }
    }
}
