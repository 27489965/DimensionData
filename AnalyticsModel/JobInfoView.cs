using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Britehouse.AnalyticsModel
{
    public class JobInfoView
    {
        // summary stats per role
        public int salesRepCount { get; set; }
        public int researchScientistCount { get; set; }
        public int labTechCount { get; set; }
        public int managerCount { get; set; }
        public int manufacturingDirectorCount { get; set; }
        public int healthcareRepresentativeCount { get; set; }
        public int salesExecutiveCount { get; set; }
        public int hrCount { get; set; }
       

    }
}