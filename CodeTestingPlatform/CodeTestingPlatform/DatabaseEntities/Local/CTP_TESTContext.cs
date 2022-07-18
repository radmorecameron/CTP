using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeTestingPlatform.DatabaseEntities.Clara;
using CodeTestingPlatform.DatabaseEntities.Local;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace CodeTestingPlatform.DatabaseEntities.Local {
    public partial class CTP_TESTContext : DbContext {
        public CTP_TESTContext() {
        }

        public CTP_TESTContext(DbContextOptions<CTP_TESTContext> options)
            : base(options) {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<CodeUpload> CodeUploads { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseSetting> CourseSettings { get; set; }
        public virtual DbSet<Ctpuser> Ctpusers { get; set; }
        public virtual DbSet<DataType> DataTypes { get; set; }
        public virtual DbSet<Exception> Exceptions { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<MethodSignature> MethodSignatures { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<SignatureException> SignatureExceptions { get; set; }
        public virtual DbSet<SignatureParameter> SignatureParameters { get; set; }
        public virtual DbSet<SignatureUserDefinedException> SignatureUserDefinedExceptions { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TestCase> TestCases { get; set; }
        public virtual DbSet<TestCaseException> TestCaseExceptions { get; set; }
        public virtual DbSet<UserCourse> UserCourses { get; set; }
        public virtual DbSet<UserDefinedException> UserDefinedExceptions { get; set; }
        public virtual DbSet<ClaraTeacherCourse> ClaraTeacherCourses { get; set; }
        public virtual DbSet<ClaraStudentCourse> ClaraStudentCourses { get; set; }
        public virtual DbSet<ClaraCSCourse> ClaraCSCourses { get; set; }
        public virtual DbSet<ClaraCSStudent> ClaraCSStudents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263. */
                IConfigurationRoot configuration = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json")
                               .Build();
                string connectionString = configuration.GetConnectionString("CTPConnection");
                optionsBuilder.UseSqlServer(connectionString); // REMOVE DATALOGGING FOR PROD
                optionsBuilder.UseExceptionProcessor();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            EntityTypeBuilder<ClaraTeacherCourse> ctc = modelBuilder.Entity<ClaraTeacherCourse>();
            EntityTypeBuilder<ClaraStudentCourse> csc = modelBuilder.Entity<ClaraStudentCourse>();
            EntityTypeBuilder< ClaraCSCourse> ccc = modelBuilder.Entity<ClaraCSCourse>();
            EntityTypeBuilder<ClaraCSStudent> ccs = modelBuilder.Entity<ClaraCSStudent>();

            if (Database.IsSqlServer()) {
                ctc.HasNoKey().ToView("vwCSTeacherCourses");
                csc.HasNoKey().ToView("vwCSStudentCourses");
                ccc.HasNoKey().ToView("vwCSCourses");
                ccs.HasNoKey().ToView("vwCSStudents");
            } else { // create primary key when in test environment
                ctc.HasKey(e => new { e.CSTeacherCourseId });
                csc.HasKey(e => new { e.CSStudentCourseId });
                ccc.HasKey(e => new { e.CSCoursesId });
                ccs.HasKey(e => new { e.CSStudentId });
            }
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Activity>(entity => {
                entity.ToTable("Activity");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("Activity_ActivityType_FK");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Activity_Course_FK");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Activity_Language_FK");
            });

            modelBuilder.Entity<ActivityType>(entity => {
                entity.ToTable("ActivityType");

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CodeUpload>(entity => {
                entity.ToTable("CodeUpload");

                entity.Property(e => e.CodeUploadFileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CodeUploadFile).HasColumnType("image");

                entity.Property(e => e.CodeUploadText)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.UploadDate).HasColumnType("datetime2");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.CodeUploads)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("CodeUpload_Activity_FK");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CodeUploads)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("CodeUpload_Student_FK");
            });

            modelBuilder.Entity<Course>(entity => {
                entity.ToTable("Course");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                //entity.HasOne(cs => cs.CourseSetting)
                //    .WithOne(c => c.Course)
                //    .HasForeignKey<Course>(k => k.CourseCode)
                //    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CourseSetting>(entity => {
                entity.ToTable("CourseSetting");

                //entity.HasOne(d => d.Course)
                //    .WithOne(p => p.CourseSetting)
                //    .OnDelete(DeleteBehavior.NoAction);

                //entity.HasOne(l => l.Language)
                //    .WithMany(c => c.CourseSettings)
                //    .HasForeignKey(cs => cs.DefaultLanguageId)
                //    .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<Ctpuser>(entity => {
                entity.HasKey(e => e.UserId)
                    .HasName("User_PK");

                entity.ToTable("CTPUser");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DataType>(entity => {
                entity.ToTable("DataType");

                entity.Property(e => e.DataType1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DataType");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.DataTypes)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("DataType_Language_FK");
            });

            modelBuilder.Entity<Exception>(entity => {
                entity.ToTable("Exception");

                entity.Property(e => e.ExceptionName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ExceptionName");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Exceptions)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Exception_Language_FK");
            });

            modelBuilder.Entity<Language>(entity => {
                entity.ToTable("Language");

                entity.Property(e => e.LanguageName)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MethodSignature>(entity => {
                entity.HasKey(e => e.SignatureId)
                    .HasName("Signature_PK");

                entity.ToTable("MethodSignature");

                entity.Property(e => e.MethodName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime2");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.MethodSignatures)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("MethodSignature_Activity_FK");
            });

            modelBuilder.Entity<Parameter>(entity => {
                entity.ToTable("Parameter");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.SignatureParameter)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.SignatureParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Parameter_SignatureParameter_FK");

                entity.HasOne(d => d.TestCase)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.TestCaseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Parameter_TestCase_FK");
            });

            modelBuilder.Entity<Result>(entity => {
                entity.ToTable("Result");

                entity.Property(e => e.ErrorMessage)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeUpload)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.CodeUploadId)
                    .HasConstraintName("Results_CodeUpload_FK");

                entity.HasOne(d => d.TestCase)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.TestCaseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Result_TestCase_FK");
            });

            modelBuilder.Entity<SignatureException>(entity => {
                entity.ToTable("SignatureException");

                entity.HasOne(d => d.MethodSignature)
                    .WithMany(p => p.SignatureExceptions)
                    .HasForeignKey(d => d.SignatureId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SignatureException_Signature_FK");

                entity.HasOne(d => d.Exception)
                    .WithMany(p => p.SignatureExceptions)
                    .HasForeignKey(d => d.ExceptionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SignatureException_Exception_FK");
            });

            modelBuilder.Entity<SignatureParameter>(entity => {
                entity.ToTable("SignatureParameter");

                entity.Property(e => e.DefaultValue)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.DataType)
                    .WithMany(p => p.SignatureParameters)
                    .HasForeignKey(d => d.DataTypeId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("SignatureParameter_DataType_FK");

                entity.HasOne(d => d.MethodSignature)
                    .WithMany(p => p.SignatureParameters)
                    .HasForeignKey(d => d.MethodSignatureId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SignatureParameter_MethodSignature_FK");
            });

            modelBuilder.Entity<SignatureUserDefinedException>(entity => {
                entity.ToTable("SignatureUserDefinedException");

                entity.HasOne(d => d.MethodSignature)
                    .WithMany(p => p.SignatureUserDefinedExceptions)
                    .HasForeignKey(d => d.SignatureId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SignatureUserDefinedException_Signature");

                entity.HasOne(d => d.UserDefinedException)
                    .WithMany(p => p.SignatureUserDefinedExceptions)
                    .HasForeignKey(d => d.UserDefinedExceptionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SignatureUserDefinedException_UserDefinedException");
            });

            modelBuilder.Entity<Student>(entity => {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Teacher>(entity => {
                entity.ToTable("Teacher");

                entity.Property(e => e.TeacherId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TestCase>(entity => {
                entity.ToTable("TestCase");

                entity.HasOne(d => d.MethodSignature)
                    .WithMany(p => p.TestCases)
                    .HasForeignKey(d => d.MethodSignatureId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TestCase_MethodSignature_FK");
            });

            modelBuilder.Entity<TestCaseException>(entity => {
                entity.ToTable("TestCaseException");

                entity.HasOne(d => d.Exception)
                    .WithMany(p => p.TestCaseExceptions)
                    .HasForeignKey(d => d.ExceptionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TestCaseException_Exception_FK");
            });

            modelBuilder.Entity<UserCourse>(entity => {
                entity.ToTable("UserCourse");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.UserCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UserCourse_Course_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCourses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UserCourse_User_FK");
            });

            modelBuilder.Entity<UserDefinedException>(entity => {
                entity.ToTable("UserDefinedException");

                entity.Property(e => e.UserDefinedExceptionName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("UserDefinedExceptionName");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.UserDefinedExceptions)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserDefinedException_Language");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
