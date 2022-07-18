using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public class CourseSetting {
        [Key]
        [Required]
        [Display(Name = "Default Course Language:")]
        public string CourseCode { get; set; }
        public int DefaultLanguageId { get; set; }
        
        //[NotMapped]
        //public string DefaultLanguageName { get { return Language.LanguageFullName; } }

       // public virtual Course Course { get; set; }
       // public virtual Language Language { get; set; }
    }
}
