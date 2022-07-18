using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class Parameter {
        public Parameter() {
        }
        [Key]
        public int ParameterId { get; set; }
        [StringLength(500)]
        public string Value { get; set; }
        [Required]
        public int TestCaseId { get; set; }
        public int? SignatureParameterId { get; set; }
        public virtual TestCase TestCase { get; set; }
        public virtual SignatureParameter SignatureParameter { get; set; }
    }
}
