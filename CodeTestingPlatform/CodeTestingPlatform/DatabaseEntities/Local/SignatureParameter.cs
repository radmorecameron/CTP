using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class SignatureParameter {

        public SignatureParameter() {
            Parameters = new HashSet<Parameter>();
        }

        [Key]
        public int SignatureParameterId { get; set; }
        //[Required]
        [StringLength(100)]
        public string ParameterName { get; set; }
        //[Required]
        public int? ParameterPosition { get; set; }
        //[Required]
        public bool InputParameter { get; set; }
        public bool RequiredParameter { get; set; }
        [StringLength(100)]
        public string DefaultValue { get; set; }
        public int? MethodSignatureId { get; set; }
        //[Required]
        public int DataTypeId { get; set; }

        //Object type name saved here if other selected as Data Type
        public string ObjectDataType { get; set; } 

        public virtual DataType DataType { get; set; }
        [JsonIgnore]
        public virtual MethodSignature MethodSignature { get; set; }
        [JsonIgnore]
        public virtual ICollection<Parameter> Parameters { get; set; }
    }
}
