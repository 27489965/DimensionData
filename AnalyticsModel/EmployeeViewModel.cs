using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Britehouse.AnalyticsModel
{
    public class EmployeeViewModel
    {
        // summary for 3 tabs
        public int employeesTotal { get; set; }
        public int totalOver18 { get; set; }
        public int totalLess18 { get; set; }

        //pieChart 
        public int totalFemales { get; set; }
        public int totalMales { get; set; }
       
    }
}