using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Repositories;
using CodeTestingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Services {
    public class ParameterService : IParameterService {
        private readonly IParameterRepository _parameterRepository;
        public ParameterService(IParameterRepository parameterRepository) {
            _parameterRepository = parameterRepository;
        }

        public async Task<Parameter> FindByIdAsync(int id) {
            return await _parameterRepository.FindByIdAsync(id);
        }

        public async Task<List<Parameter>> ListAsync() {
            return await _parameterRepository.ListAsync();
        }

        public async Task UpdateAsync(Parameter param) {
            await _parameterRepository.UpdateAsync(param);
        }

        public async Task CreateAsync(Parameter param) {
            await _parameterRepository.CreateAsync(param);
        }

        public async Task DeleteAsync(Parameter param) {
            await _parameterRepository.DeleteAsync(param);
        }

        public void DetachEntities() {
            _parameterRepository.DetachEntities();
        }
    }
}
