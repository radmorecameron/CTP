using CodeTestingPlatform.CompilerClient;
using CodeTestingPlatform.DatabaseEntities.Clara;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models;
using CodeTestingPlatform.Models.Validation;
using CodeTestingPlatform.Repositories;
using CodeTestingPlatform.Repositories.Interfaces;
using CodeTestingPlatform.Services;
using CodeTestingPlatform.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform {
    [ExcludeFromCodeCoverageAttribute]
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(options => {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            services.AddControllersWithViews().AddFluentValidation();

            services.AddHttpContextAccessor();
            #region Configure Authentication
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.Cookie.IsEssential = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
                    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                    options.Cookie.Name = "Cookie";
                    //options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Login/AccessDenied");
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login");
                    options.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Login/Logout");
                });
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.Configure<CookiePolicyOptions>(options => {
                options.Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
                options.ConsentCookie.IsEssential = true;
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
            });
            #endregion
            services.AddDbContext<CTP_TESTContext>(options => {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(Configuration.GetConnectionString("CTPConnection"));
            });

            #region Scoped Models
            services.AddTransient<ICurrentSession, CurrentSession>();
            services.AddScoped<Activity>();
            services.AddScoped<ActivityType>();
            services.AddScoped<CodeUpload>();
            services.AddScoped<Course>();
            services.AddScoped<CourseSetting>();
            services.AddScoped<Ctpuser>();
            services.AddScoped<DataType>();
            services.AddScoped<Parameter>();
            services.AddScoped<Student>();
            services.AddScoped<TestCase>();
            services.AddScoped<Teacher>();
            services.AddScoped<UserCourse>();
            services.AddScoped<Language>();
            services.AddScoped<ClaraCSStudent>();
            services.AddScoped<ClaraCSCourse>();
            services.AddScoped<ClaraStudentCourse>();
            services.AddScoped<ClaraTeacherCourse>();
            services.AddScoped<ICompiler, Compiler>();
            #endregion

            #region Repository Pattern
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IActivityTypeRepository, ActivityTypeRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseSettingRepository, CourseSettingRepository>();
            services.AddScoped<IClaraRepository, ClaraRepository>();
            services.AddScoped<IDataTypeRepository, DataTypeRepository>();
            services.AddScoped<IExceptionRepository, ExceptionRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IMethodSignatureRepository, MethodSignatureRepository>();
            services.AddScoped<IParameterRepository, ParameterRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITestCaseRepository, TestCaseRepository>();
            services.AddScoped<ICodeUploadRepository, CodeUploadRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserDefinedExceptionRepository, UserDefinedExceptionRepository>();
            #endregion

            #region Application Service
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IActivityTypeService, ActivityTypeService>();
            services.AddScoped<ICodeUploadService, CodeUploadService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseSettingService, CourseSettingService>();
            services.AddScoped<IClaraService, ClaraService>();
            services.AddScoped<IDataTypeService, DataTypeService>();
            services.AddScoped<IExceptionService, ExceptionService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IMethodSignatureService, MethodSignatureService>();
            services.AddScoped<IParameterService, ParameterService>();
            services.AddScoped<IResultService, ResultService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ITestCaseService, TestCaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDefinedExceptionService, UserDefinedExceptionService>();
            #endregion

            #region FluentValidation
            services.AddTransient<IValidator<Activity>, ActivityValidator>();
            services.AddTransient<IValidator<MethodSignature>, SignatureValidator>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseSerilogRequestLogging();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Home/Status", "?code={0}");
            app.UseStaticFiles();

            var supportedCultures = new[] { "en", "fr" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseWebSockets();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseCookiePolicy(new CookiePolicyOptions {
            //    MinimumSameSitePolicy = SameSiteMode.Strict,
            //});

            app.UseSession();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
