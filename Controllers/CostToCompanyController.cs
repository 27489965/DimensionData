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
    public class CostToCompanyController : Controller
    {
        private MongoContext dbContext;
        private IMongoCollection<CostToCompany> costToCompanyCollection;
        CostsToCompanyViewModel costsToCompanyView = new CostsToCompanyViewModel();
        public CostToCompanyController()
        {
            dbContext = new MongoContext();
            costToCompanyCollection = dbContext.database.GetCollection<CostToCompany>("CostToCompany");
        }


        public ActionResult DataAnalytics()
        {
            List<CostToCompany> costs = costToCompanyCollection.AsQueryable<CostToCompany>().ToList();

            int overTime = (from x in costs.Where(x => x.OverTime.Contains("Yes")) select x.PayId).Count();
            int noOverTime = (from x in costs.Where(x => x.OverTime.Contains("No")) select x.PayId).Count();
            double totalCost = (from x in costs select x.MonthlyIncome).Sum();


            costsToCompanyView.totalSalary = Math.Round(totalCost, 2); ;
            costsToCompanyView.noOverTimeCount = noOverTime;
            costsToCompanyView.overtimeCount = overTime;

            return View(costsToCompanyView);
        }


        // GET: CostToCompany
        [Authorize]
        public ActionResult Index(int? page)
        {
            List<CostToCompany> costs = costToCompanyCollection.AsQueryable<CostToCompany>().ToList();
            var pageNumber = page ?? 1;
            var onePage =costs.ToPagedList(pageNumber, 15);
            return View(onePage);
        }

        // GET: CostToCompany/Details/5
        public ActionResult Details(string id)
        {
            ObjectId costID = new ObjectId(id);
            var costs = costToCompanyCollection.AsQueryable<CostToCompany>().SingleOrDefault(x => x._id == costID);
            return View(costs);
        }

        // GET: CostToCompany/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CostToCompany/Create
        [HttpPost]
        public ActionResult Create(CostToCompany costs)
        {
            try
            {
                // TODO: Add insert logic here

              costToCompanyCollection.InsertOneAsync(costs);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CostToCompany/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                ObjectId costID = new ObjectId(id);
                var costs = costToCompanyCollection.AsQueryable<CostToCompany>().SingleOrDefault(x => x._id == costID);
                return View(costs);
            }
        }

        // POST: CostToCompany/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CostToCompany costs)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<CostToCompany>.Filter.Eq("_id", ObjectId.Parse(id));

                var Update = Builders<CostToCompany>.Update
                   .Set("PayId", costs.PayId)
                   .Set("EmployeeNumber", costs.EmployeeNumber)
                   .Set("HourlyRate", costs.HourlyRate)
                   .Set("MonthlyRate", costs.MonthlyRate)
                   .Set("MonthlyIncome", costs.MonthlyIncome)
                   .Set("DailyRate", costs.DailyRate)
                   .Set("OverTime", costs.OverTime)
                   .Set("PercentSalaryHike", costs.PercentSalaryHike);
                  
                var results = costToCompanyCollection.UpdateOne(filter, Update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CostToCompany/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                ObjectId costID = new ObjectId(id);
                var costs = costToCompanyCollection.AsQueryable<CostToCompany>().SingleOrDefault(x => x._id == costID);
                return View(costs);
            }
        }

        // POST: CostToCompany/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, CostToCompany costs)
        {
            try
            {
                // TODO: Add delete logic here
                costToCompanyCollection.DeleteOne(Builders<CostToCompany>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
