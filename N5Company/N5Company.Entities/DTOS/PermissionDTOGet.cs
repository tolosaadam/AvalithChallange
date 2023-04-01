using System;
using System.Collections.Generic;
using System.Text;

namespace N5Company.Entities.DTOS
{
    public class PermissionDTOGet
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
