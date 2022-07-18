using CodeTestingPlatform.DatabaseEntities.Clara;
using CodeTestingPlatform.DatabaseEntities.Local;
using CodeTestingPlatform.Models.Enums;
using CodeTestingPlatform.Services.Interfaces;
using LoginService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models {
    [ExcludeFromCodeCoverageAttribute]
    public class CurrentSession : ICurrentSession {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoginSoapClient _client = new(LoginSoapClient.EndpointConfiguration.LoginSoap);
        //private readonly ClaraCSStudent _claraStudent;
        private readonly IClaraService _claraService;
        readonly ISession Session;

        public CurrentSession(IHttpContextAccessor httpContextAccessor, IClaraService claraService) {
            _httpContextAccessor = httpContextAccessor;
            Session = _httpContextAccessor.HttpContext.Session;
            //_claraStudent = new();
            _claraService = claraService;
        }

        public async Task Login(Login login) {
            AuthenticateResponse authenticateResponse = await _client.AuthenticateAsync(login.Username, login.Password);

            if (authenticateResponse.Body.AuthenticateResult) {
                AuthorizeResponse authorizeResponse = await _client.AuthorizeAsync(login.Username, "CTP");

                UserBLL userBLL = authorizeResponse.Body.AuthorizeResult;

                if (!userBLL.HasError) {
                    // default roles to false
                    Session.SetString("IsTeacher", bool.FalseString);
                    Session.SetString("IsCoordinator", bool.FalseString);
                    Session.SetString("IsStudent", bool.FalseString);

                    foreach (RoleBLL item in userBLL.RoleList) {
                        string itemCode = item.Code.Trim();

                        // set the roles depending on valid rules
                        // Teacher
                        if (itemCode.Equals("TE")) { 
                            Session.SetString("IsTeacher", bool.TrueString);
                            Session.SetString("EmployeeId", userBLL.EmployeeId.ToString());
                        // Coordinator
                        }
                        else if (itemCode.Equals("CO")) { 
                            Session.SetString("IsCoordinator", bool.TrueString);
                            Session.SetString("EmployeeId", userBLL.EmployeeId.ToString());
                        // Student
                        }
                        else if (itemCode.Equals("ST")) { 
                            Session.SetString("IsStudent", bool.TrueString);
                            Session.SetString("StudentId", userBLL.StudentId.ToString());
                        }
                    }
                    if (IsUserAStudent() || IsUserATeacher() || IsUserACoordinator()) {
                        if (IsUserAStudent()) {
                            await SetCompSciStudent();
                            if (IsCompSciStudent()) {
                                Session.SetString("IsAuthorized", bool.TrueString);
                            } else {
                                Session.SetString("IsAuthorized", bool.FalseString);
                            }
                        } else {
                            Session.SetString("IsAuthorized", bool.TrueString);
                        }
                        Session.SetString("Username", userBLL.Username);
                        Session.SetString("FirstName", userBLL.FirstName);
                        Session.SetString("LastName", userBLL.LastName);
                        SetSemesterId();
                        SetSemesterName(GetSemesterId());
                        await SetClaims(userBLL);
                    } else {
                        Session.SetString("IsAuthorized", bool.FalseString);
                    }
                }
            }
        }

        public void SetCoordinatorDepartmentName(string deptName) {
            Session.SetString("Department", deptName);
        }

        public void SetCoordinatorDepartmentId(string deptid) {
            Session.SetString("DepartmentId", deptid);
        }

        public void SetSemesterName(string semesterId) {
            char semesterChar = 'F';
            switch (semesterId[^1..]) {
                case "1":
                    semesterChar = 'W';
                    break;
                case "3":
                    semesterChar = 'F';
                    break;
            }
            string semesterName = semesterChar + semesterId[0..^1];
            Session.SetString("SemesterName", semesterName);
        }

        public string GetSemesterName() {
            return Session.GetString("SemesterName");
        }

        public string GetSemesterId()
        {
            // Will be removed when clara DB is refreshed
            return "20213"; 
            //return Session.GetString("SemesterId");
        }

        public void SetSemesterId() {
            DateTime currDate = DateTime.Now;
            if (currDate.Month >= (int)Month.August && currDate.Month <= (int)Month.December) {
                Session.SetString("SemesterId", $"{currDate.Year}{(int)SemesterTerm.Fall}");
            } else if (currDate.Month >= (int)Month.January && currDate.Month <= (int)Month.June) {
                Session.SetString("SemesterId", $"{currDate.Year}{(int)SemesterTerm.Winter}");
            } else {
                Session.SetString("SemesterId", "-1");
            }
        }

        private async Task SetCompSciStudent() {
            List<ClaraCSStudent> s = new();
            s = await _claraService.GetClaraCSStudents();
            int studentId = GetStudentId();
            if (s.FirstOrDefault(s => s.StudentId == studentId) != null) {
                Session.SetString("IsCompSci", bool.TrueString);
            } else {
                Session.SetString("IsCompSci", bool.FalseString);
            }
        }

        public bool IsCompSciStudent() {
            return Convert.ToBoolean(Session.GetString("IsCompSci"));
        }

        public bool IsAuthorized() {
            if (Session.GetString("IsAuthorized") != null)
                return Convert.ToBoolean(Session.GetString("IsAuthorized"));
            else
                return false;
        }

        public bool IsUserAStudent() {
            return Convert.ToBoolean(Session.GetString("IsStudent"));
        }

        public bool IsUserATeacher() {
            return Convert.ToBoolean(Session.GetString("IsTeacher"));
        }

        public bool IsUserACoordinator() {
            return Convert.ToBoolean(Session.GetString("IsCoordinator"));
        }

        public string GetFullName() {
            if (Session.GetString("FirstName") == null || Session.GetString("LastName") == null)
                return "";
            return $"{Session.GetString("FirstName")} {Session.GetString("LastName")}";
        }

        public string GetCoordinatorDepartmentName() {
            if (Session.GetString("Department") == null) {
                return "";
            }
            return Session.GetString("Department");
        }

        public string GetCoordinatorDepartmentId() {
            if (Session.GetString("DepartmentId") == null) {
                return "-1";
            }
            return Session.GetString("DepartmentId");
        }

        public string GetFirstName() {
            if (Session.GetString("FirstName") == null)
                return "";
            return Session.GetString("FirstName");
        }

        public string GetLastName() {
            if (Session.GetString("LastName") == null)
                return "";
            return Session.GetString("LastName");
        }

        public string GetUsername() {
            return Session.GetString("Username");
        }

        public void Logout() {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        public int GetEmployeeId() {
            return Convert.ToInt32(Session.GetString("EmployeeId"));
        }

        public int GetStudentId() {
            return Convert.ToInt32(Session.GetString("StudentId"));
        }

        private async Task SetClaims(UserBLL userBll) {
            // set the user information in a list of claims
            List<Claim> claims = new() {
                new Claim(ClaimTypes.Surname, userBll.FirstName),
                new Claim(ClaimTypes.GivenName, userBll.LastName),
                new Claim("id", IsUserATeacher() ? GetEmployeeId().ToString() : GetUsername())
            };
            // add the roles to the claims
            claims.AddRange(userBll.RoleList.Select(role => new Claim(ClaimTypes.Role, role.Code.Trim())));
            // specify the authentication type
            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties authProperties = new();

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public string GetPreviousPage() {
            return Session.GetString("PreviousPage");
        }

        public void SetPreviousPage(string previousPage) {
            Session.SetString("PreviousPage", previousPage);
        }
    }
}
