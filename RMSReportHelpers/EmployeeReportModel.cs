using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSReportHelpers {
    public class EmployeeReportModel {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Address { get; set; }
        public string NRC { get; set; }
        public string Position { get; set; }
        public decimal BaseSalary { get; set; }
    }
}
