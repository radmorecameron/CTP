using CodeTestingPlatform.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local
{
    public partial class Activity
    {
        private readonly IDateTimeProvider IDateTime;

        public Activity()
        {
            CodeUploads = new HashSet<CodeUpload>();
            MethodSignatures = new HashSet<MethodSignature>();
        }

        public Activity(IDateTimeProvider fakeTime) {
            CodeUploads = new HashSet<CodeUpload>();
            MethodSignatures = new HashSet<MethodSignature>();
            IDateTime = fakeTime;
        }

        [Key]
        public int ActivityId { get; set; }
        
        [Display(Name = "Title: ")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Start Date: ")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "End Date: ")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime? EndDate { get; set; } = null;
        
        [Required]
        [Display(Name = "Choose Course: ")]
        public int CourseId { get; set; }
        [Required]

        [Display(Name = "Choose Activity Type: ")]
        public int ActivityTypeId { get; set; }
        
        [Required]
        [Display(Name = "Choose Language: ")]
        public int LanguageId { get; set; }

        [Display(Name = "Activity Type: ")]
        public virtual ActivityType ActivityType { get; set; }

        [Display(Name = "Course Name: ")]
        public virtual Course Course { get; set; }

        [Display(Name = "Coding Language: ")]
        public virtual Language Language { get; set; }
        public virtual ICollection<CodeUpload> CodeUploads { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<MethodSignature> MethodSignatures { get; set; }

        //public async Task<List<Activity>> GetActivities() {   
        //    return await _db.Activities
        //        .Include(a => a.ActivityType)
        //        .ToListAsync();
        //}

        //public async Task<List<Activity>> GetActivitiesByCourseId(int courseId) {
        //    return await _db.Activities
        //        .Include(a => a.ActivityType)
        //        .Where(a => a.CourseId == courseId)
        //        .ToListAsync();
        //}

        //public async Task<Activity> GetActivity(int id) {
        //    return await _db.Activities
        //        .Include(a => a.ActivityType)
        //        .Include(a => a.Course)
        //        .Include(a => a.Language)
        //        .Include(a => a.MethodSignatures)
        //            .ThenInclude(a => a.SignatureParameters)
        //                .ThenInclude(a => a.DataType)
        //        .Include(a => a.MethodSignatures)
        //            .ThenInclude(a => a.ReturnType)
        //        .Include(a => a.MethodSignatures)
        //            .ThenInclude(s => s.TestCases)
        //                .ThenInclude(s => s.Parameters)
        //                    .ThenInclude(s => s.DataType)
        //        .FirstOrDefaultAsync(a => a.ActivityId == id);
        //}

        //public async Task<List<Activity>> GetRecentActivities() {
        //    var activities = GetActivities();
        //    var result = await _db.Activities
        //        .Where(a => a.StartDate >= IDateTime.Now && a.StartDate <= IDateTime.Now.AddDays(7))
        //        .ToListAsync();
        //    return result;
        //}

        //public async Task RemoveActivity() {
        //    _db.Activities.Remove(this);
        //    await _db.SaveChangesAsync();
        //}

        //public async Task UpdateActivity() {
        //    _db.Activities.Update(this);
        //    await _db.SaveChangesAsync();
        //}

        //public async Task AddActivity() {
        //    _db.Activities.Add(this);
        //    await _db.SaveChangesAsync();
        //}

        public string GetStrEndDate() {
            if (EndDate != null) {
                DateTime date = (DateTime)EndDate;
                return date.ToString("yyyy\\/MM\\/dd");
            }
            return "";
        }

        public string GetStrStartDate() {
            return StartDate.ToString("yyyy\\/MM\\/dd");
        }
    }
}
