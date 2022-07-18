using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class Language {
        public Language() {
            Activities = new HashSet<Activity>();
            DataTypes = new HashSet<DataType>();
            Exceptions = new HashSet<Exception>();
            UserDefinedExceptions = new HashSet<UserDefinedException>();
        }
        [Key]
        public int LanguageId { get; set; }
        [Required]
        [StringLength(25)]
        public string LanguageName { get; set; }

        public string LanguageVersion { get; set; }

        [NotMapped]
        public string LanguageFullName {
            get { return $"{LanguageName} v{LanguageVersion}"; }
        }
        
        [JsonIgnore]
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<DataType> DataTypes { get; set; }
        public virtual ICollection<Exception> Exceptions { get; set; }
        //public virtual ICollection<CourseSetting> CourseSettings { get; set; }
        public virtual ICollection<UserDefinedException> UserDefinedExceptions { get; set; }
    }
}
