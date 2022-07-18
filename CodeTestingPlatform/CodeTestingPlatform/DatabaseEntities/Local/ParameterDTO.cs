using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public class ParameterDTO {
        public int ParameterId { get; set; }
        [StringLength(100)]
        public string ParameterName { get; set; }
        [Required]
        [StringLength(500)]
        public string Value { get; set; }
        public int? SignatureParameterId { get; set; }
        [Required]
        public int? ParameterPosition { get; set; }
        [Required]
        public bool InputParameter { get; set; }
        [Required]
        public int TestCaseId { get; set; }
        [Required]
        public int DataTypeId { get; set; }

        public ParameterDTO(Parameter parameter) {
            ParameterId = parameter.ParameterId;
            ParameterName = parameter.SignatureParameter.ParameterName;
            Value = parameter.Value;
            SignatureParameterId = parameter.SignatureParameterId;
            ParameterPosition = parameter.SignatureParameter.ParameterPosition;
            InputParameter = parameter.SignatureParameter.InputParameter;
            TestCaseId = parameter.TestCaseId;
            DataTypeId = parameter.SignatureParameter.DataTypeId;    
        }

        public Parameter GetParameter() {
            return new Parameter {
                ParameterId = ParameterId,
                SignatureParameterId = SignatureParameterId,
                Value = Value,
                TestCaseId = TestCaseId
            };
        }
    }

}
