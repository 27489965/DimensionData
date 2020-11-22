using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Britehouse.Models
{
    public class JobInfo
    {
        public ObjectId _id { get; set; }

        public int  JobID { get; set; }
    public string JobRole { get; set; }
public string Department { get; set; }
  public string JobLevel { get; set; }
   public int StandardHours { get; set; }
   public int EmployeeCount { get; set; }
    public string BusinessTravel { get; set; }
     public int StockOptionLevel { get; set; }


}
}