using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Britehouse.AnalyticsModel
{
    public class CostsToCompanyViewModel
    {
        // summary for 3 tabs insights
        public double totalSalary { get; set; }
      
        //PieChart for number of overtimes count by employees
        public int overtimeCount { get; set; }
        public int noOverTimeCount { get; set; }
    }
}