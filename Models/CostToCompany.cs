using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;


namespace Britehouse.Models
{
    public class CostToCompany
    {

        public ObjectId _id { get; set; }
        public int PayId { get; set; }
        public int EmployeeNumber { get; set; }
        public double HourlyRate { get; set; }
        public double MonthlyRate { get; set; }
        public double MonthlyIncome { get; set; }
        public double DailyRate { get; set; }
        public string OverTime { get; set; }
        public double PercentSalaryHike { get; set; }


    }
}
