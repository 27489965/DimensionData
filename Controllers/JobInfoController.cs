using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Britehouse.App_Start;
using Britehouse.Models;
using Britehouse.AnalyticsModel;
using MongoDB.Driver;
using MongoDB.Bson;
using PagedList;
namespace Britehouse.Controllers
{
    public class JobInfoController : Controller
    {
        private MongoContext dbContext;
        private IMongoCollection<JobInfo> jobInfoCollection;
        JobInfoView infoView = new JobInfoView();

        public JobInfoController()
        {
            dbContext = new MongoContext();
            jobInfoCollection = dbContext.database.GetCollection<JobInfo>("JobInfo");
        }

        public ActionResult DataAnalytics()
        { // Taking data from the collections/database to the data visualization tool(Bar Graph)
            List<JobInfo> info = jobInfoCollection.AsQueryable<JobInfo>().ToList();

            int salesRepCount = (from x in info.Where(x => x.JobRole.Contains("Sales Representative")) select x.EmployeeCount).Count();
            int researchScientistCount = (from x in info.Where(x => x.JobRole.Contains("Research Scientist")) select x.EmployeeCount).Count();
            int labTechCount = (from x in info.Where(x => x.JobRole.Contains("Laboratory Technician")) select x.EmployeeCount).Count();
            int managerCount = (from x in info.Where(x => x.JobRole.Contains("Manager")) select x.EmployeeCount).Count();
            int manufacturingDirectorCount = (from x in info.Where(x => x.JobRole.Contains("Manufacturing Director")) select x.EmployeeCount).Count();
            int healthcareRepresentativeCount = (from x in info.Where(x => x.JobRole.Contains("Healthcare Representative")) select x.EmployeeCount).Count();
            int salesExecutiveCount = (from x in info.Where(x => x.JobRole.Contains("Sales Executive")) select x.EmployeeCount).Count();
            int hrCount = (from x in info.Where(x => x.JobRole.Contains("Human Resources")) select x.EmployeeCount).Count();



            // Bar graph contents
            infoView.hrCount = hrCount;
            infoView.salesExecutiveCount = salesExecutiveCount;
            infoView.salesRepCount = salesRepCount;
            infoView.managerCount = managerCount;
            infoView.labTechCount = labTechCount;
            infoView.manufacturingDirectorCount = manufacturingDirectorCount;
            infoView.healthcareRepresentativeCount = healthcareRepresentativeCount;
            infoView.researchScientistCount = researchScientistCount;


            return View(infoView);
        }




        // GET: JobInfo
        [Authorize]
        public ActionResult Index(int? page)
        {
            List<JobInfo> Info =jobInfoCollection.AsQueryable<JobInfo>().ToList();
            var pageNumber = page ?? 1;
            var onePage = Info.ToPagedList(pageNumber, 15);
            return View(onePage);
        }

        // GET: JobInfo/Details/5
        public ActionResult Details(string id)
        {
            ObjectId jobinfoID = new ObjectId(id);
            var info = jobInfoCollection.AsQueryable<JobInfo>().SingleOrDefault(x => x._id == jobinfoID);
            return View(info);
        }

        // GET: JobInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobInfo/Create
        [HttpPost]
        public ActionResult Create(JobInfo jobInfo)
        {
            try
            {
                // TODO: Add insert logic here

               jobInfoCollection.InsertOneAsync(jobInfo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobInfo/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                ObjectId jobinfoID = new ObjectId(id);
                var info = jobInfoCollection.AsQueryable<JobInfo>().SingleOrDefault(x => x._id == jobinfoID);
                return View(info);
            }
        }

        // POST: JobInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, JobInfo jobInfo)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<JobInfo>.Filter.Eq("_id", ObjectId.Parse(id));

                var Update = Builders<JobInfo>.Update
                   .Set("JobID", jobInfo.JobID)
                   .Set("JobRole", jobInfo.JobRole)
                   .Set("Department", jobInfo.Department)
                   .Set("JobLevel", jobInfo.JobLevel)
                   .Set("StandardHours", jobInfo.StandardHours)
                   .Set("EmployeeCount", jobInfo.EmployeeCount)
                   .Set("BusinessTravel", jobInfo.BusinessTravel)
                   .Set("StockOptionLevel", jobInfo.StockOptionLevel);
                  

                var results = jobInfoCollection.UpdateOne(filter, Update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobInfo/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                ObjectId jobinfoID = new ObjectId(id);
                var info = jobInfoCollection.AsQueryable<JobInfo>().SingleOrDefault(x => x._id == jobinfoID);
                return View(info);
            }
        }

        // POST: JobInfo/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, JobInfo jobInfo)
        {
            try
            {
                // TODO: Add delete logic here
                jobInfoCollection.DeleteOne(Builders<JobInfo>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
