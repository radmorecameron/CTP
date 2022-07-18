using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using CodeTestingPlatform.DatabaseEntities.Clara;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class Course {

        public Course() {
            Activities = new HashSet<Activity>();
            UserCourses = new HashSet<UserCourse>();
        }

        public Course(string courseName, string courseCode) : this() {
            CourseName = courseName;
            CourseCode = courseCode;
        }

        public Course(int courseId, string courseName, string courseCode) : this() {
            CourseId = courseId;
            CourseName = courseName;
            CourseCode = courseCode;
        }

        [Key]
        public int CourseId { get; set; }
        [StringLength(40)]
        [Display(Name = "Course Name: ")]
        public string CourseName { get; set; }
        [ForeignKey("CourseCode")]
        [StringLength(30)]
        [Display(Name = "Course Code: ")]
        public string CourseCode { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<UserCourse> UserCourses { get; set; }
        //public virtual CourseSetting CourseSetting { get; set; }
    }
}
