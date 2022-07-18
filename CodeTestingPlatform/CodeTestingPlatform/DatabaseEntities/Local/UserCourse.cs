using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class UserCourse {
        public UserCourse() {
        }

        public UserCourse(int userId, int courseId) : this() {
            UserId = userId;
            CourseId = courseId;
        }

        [Key]
        [Required]
        public int UserCourseId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Ctpuser User { get; set; }
    }
}
