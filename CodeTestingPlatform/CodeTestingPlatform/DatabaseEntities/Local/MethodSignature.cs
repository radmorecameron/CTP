using CodeTestingPlatform.Models.Enums;
using CodeTestingPlatform.Models.Validation.LanguageValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class MethodSignature {
        public MethodSignature() {
            SignatureParameters = new HashSet<SignatureParameter>();
            TestCases = new HashSet<TestCase>();
        }

        [Key]
        public int SignatureId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Method Name ")]
        public string MethodName { get; set; }
        [StringLength(255)]
        [Display(Name = "Description: ")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Return Type: ")]
        public int ReturnTypeId { get; set; }
        [Display(Name = "Data Type:")]
        public string ReturnObjectType { get; set; }
        [Required]
        public int ActivityId { get; set; }
        public DateTime? LastUpdated { get; set; }
        [Display(Name = "Return Type: ")]
        public virtual DataType ReturnType { get; set; }
        [Display(Name = "Activity: ")]
        public virtual Activity Activity { get; set; }
        public virtual ICollection<SignatureException> SignatureExceptions { get; set; }
        public virtual ICollection<SignatureParameter> SignatureParameters { get; set; }
        public virtual ICollection<TestCase> TestCases { get; set; }
        public virtual ICollection<SignatureUserDefinedException> SignatureUserDefinedExceptions { get; set; }

        public string MethodFormat() {
            ILanguageValidator validator = LanguageValidator.GetLanguageValidator(Activity.LanguageId);
            return validator.MethodFormat(this);
        }

        public void FilterReturnObjType() {
            //Do not save the ReturnObjectType if other(ReturnType) is not selected
            if (ReturnTypeId != (int)Data.Types.Other) {
                ReturnObjectType = null;
            }
        }

        public static ICollection<SignatureParameter> FilterObjType(SignatureParameter[] signatureParameters) {
            foreach (var parameter in signatureParameters) {
                if (parameter.DataTypeId != (int)Data.Types.Other) {
                    //Do not save the ObjectDataType if other(DataType) is not selected
                    parameter.ObjectDataType = null;
                }
            }

            return signatureParameters;
        }

    }
}
