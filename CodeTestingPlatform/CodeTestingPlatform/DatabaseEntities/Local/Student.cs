using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class Student {
        public Student() {
            CodeUploads = new HashSet<CodeUpload>();
        }
        public Student(int studentId) : this() {
            StudentId = studentId;
        }

        public Student(int studentId, int userId) : this(studentId) {
            UserId = userId;
        }

        [Key]
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int UserId { get; set; }

        public virtual Ctpuser User { get; set; }
        public virtual ICollection<CodeUpload> CodeUploads { get; set; }
    }
}
