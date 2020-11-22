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
    public class EmployeeController : Controller
    {

        private MongoContext dbContext;
        private IMongoCollection<Employee> employeesCollection;
        EmployeeViewModel employeeView = new EmployeeViewModel();

        public EmployeeController()
        {
            dbContext = new MongoContext();
            employeesCollection = dbContext.database.GetCollection<Employee>("Employees");
        }



        public ActionResult DataAnalytics()
        {
            List<Employee> info = employeesCollection.AsQueryable<Employee>().ToList();

            //Tabs to show total count
            int totalEmployees = (from x in info select x.EmployeeID).Count();
            int totalOver18 = (from x in info.Where(x => x.Over18.Contains("Y")) select x.EmployeeID).Count();
            int totalLess18 = (from x in info.Where(x => x.Over18.Contains("N")) select x.EmployeeID).Count();


            //Pie chart for Genders
            int totalFemale = (from x in info.Where(x => x.Gender.Contains("Female")) select x.EmployeeID).Count();
            int totalMales = (from x in info.Where(x => x.Gender.Contains("Male")) select x.EmployeeID).Count();


            employeeView.employeesTotal = totalEmployees;
            employeeView.totalFemales = totalFemale;
            employeeView.totalMales = totalMales;
            employeeView.totalOver18 = totalOver18;
            employeeView.totalLess18 = totalLess18;

            return View(employeeView);
        }


        // GET: Employee
        [Authorize]
        public ActionResult Index(int? page)
        {
            List<Employee> employees = employeesCollection.AsQueryable<Employee>().ToList();
            var pageNumber = page ?? 1;
            var onePage =employees.ToPagedList(pageNumber, 15);
            return View(onePage);
        }

        // GET: Employee/Details/5
        public ActionResult Details(string id)
        {
            ObjectId empID = new ObjectId(id);
            var employees = employeesCollection.AsQueryable<Employee>().SingleOrDefault(x => x._id == empID);
            return View(employees);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                // TODO: Add insert logic here

               employeesCollection.InsertOneAsync(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                ObjectId empID = new ObjectId(id);
                var employees = employeesCollection.AsQueryable<Employee>().SingleOrDefault(x => x._id == empID);
                return View(employees);
            }
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Employee employee)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<Employee>.Filter.Eq("_id", ObjectId.Parse(id));

                var Update = Builders<Employee>.Update
                   .Set("EmployeeID", employee.EmployeeID)
                   .Set("EmployeeNumber", employee.EmployeeNumber)
                   .Set("Age", employee.Age)
                   .Set("Over18", employee.Over18)
                   .Set("Gender", employee.Gender)
                   .Set("MaritalStatus", employee.MaritalStatus)
                   .Set("EducationField", employee.EducationField)
                   .Set("BusinessTravel", employee.BusinessTravel)
                   .Set("DistanceFromHome", employee.DistanceFromHome)
                   .Set("Attrition", employee.Attrition)
                   .Set("RelationshipSatisfaction", employee.RelationshipSatisfaction)
                   .Set("EnvironmentSatisfaction", employee.EnvironmentSatisfaction)
                   .Set("JobSatisfication", employee.JobSatisfication)
                   .Set("PerformanceRating", employee.PerformanceRating)
                   .Set("Department", employee.Department)
                   .Set("YearsInCurrentRole", employee.YearsInCurrentRole)
                   .Set("MonthlyIncome", employee.MonthlyIncome)
                    .Set("MonthlyRate", employee.MonthlyRate)
                   .Set("NumCompaniesWorked", employee.NumCompaniesWorked)
                   .Set("TotalWorkingYears", employee.TotalWorkingYears)
                   .Set("TrainingTimesLastYear", employee.TrainingTimesLastYear)
                   .Set("YearsAtCompany", employee.YearsAtCompany)
                   .Set("YearsWithCurrManager", employee.YearsWithCurrManager)
                .Set("YearsSinceLastPromotion ", employee.YearsSinceLastPromotion);


                var results = employeesCollection.UpdateOne(filter, Update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                ObjectId empID = new ObjectId(id);
                var employees = employeesCollection.AsQueryable<Employee>().SingleOrDefault(x => x._id == empID);
                return View(employees);
            }
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Employee employee)
        {
            try
            {
                // TODO: Add delete logic here
                employeesCollection.DeleteOne(Builders<Employee>.Filter.Eq("_id", ObjectId.Parse(id)));;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
