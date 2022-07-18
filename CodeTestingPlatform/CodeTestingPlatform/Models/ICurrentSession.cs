using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models {
    public interface ICurrentSession {
        //*****************Ajust these as needed*************
        Task Login(Login login);
        bool IsAuthorized();
        bool IsUserAStudent();
        bool IsUserATeacher();
        bool IsUserACoordinator();
        void SetCoordinatorDepartmentName(string deptname);
        void SetCoordinatorDepartmentId(string deptid);
        void SetSemesterName(string semesterName);
        void SetSemesterId();
        int GetEmployeeId();
        int GetStudentId();
        string GetFullName();
        string GetUsername();
        string GetFirstName();
        string GetLastName();
        string GetCoordinatorDepartmentName();
        string GetCoordinatorDepartmentId();
        string GetSemesterId();
        string GetSemesterName();
        void Logout();
        public bool IsCompSciStudent();
        string GetPreviousPage();
        void SetPreviousPage(string previousPage);
    }
}
