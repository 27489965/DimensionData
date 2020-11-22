using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Britehouse.Models
{
    public class Employee
    {
        public ObjectId _id { get; set; }


        public string EmployeeID { get; set; }
    public string EmployeeNumber { get; set; }
           public string Age { get; set; }
            public string Over18 { get; set; }
        public string Gender { get; set; }
      public string MaritalStatus { get; set; }
      public string EducationField { get; set; }
      public string BusinessTravel { get; set; }
       public int DistanceFromHome { get; set; }
        public string Attrition { get; set; }
        public string RelationshipSatisfaction  { get; set; }
         public int  EnvironmentSatisfaction { get; set; }
        public int  JobSatisfication { get; set; }
        public int  PerformanceRating { get; set; }
        public string Department { get; set; }
       public int YearsInCurrentRole { get; set; }
       public double MonthlyIncome { get; set; }
        public double  MonthlyRate { get; set; }
        public int NumCompaniesWorked { get; set; }
         public int TotalWorkingYears { get; set; }
         public int  TrainingTimesLastYear { get; set; }
         public int YearsAtCompany { get; set; }
         public int YearsWithCurrManager { get; set; }
         public int YearsSinceLastPromotion { get; set; }







}
}