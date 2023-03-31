using System;
using System.Collections.Generic;
using System.Text;

namespace N5Company.Entities.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }     
        public DateTime PermissionDate { get; set; }
        public int PermissionTypeId { get; set; }
        public PermissionType PermissionType { get; set; }
    }
}
