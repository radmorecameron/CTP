using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class DataType {
        public DataType() {
            SignatureParameters = new HashSet<SignatureParameter>();
        }

        [Key]
        [Required]
        public int DataTypeId { get; set; }
        [StringLength(20)]
        [Required]
        [Display(Name = "Data Type")]
        public string DataType1 { get; set; }
        [Required]
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
        [JsonIgnore]
        public virtual ICollection<SignatureParameter> SignatureParameters { get; set; }
    }
}
