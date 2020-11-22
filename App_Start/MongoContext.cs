using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using System.Configuration;
namespace Britehouse.App_Start
{
    public class MongoContext
    {
        public IMongoDatabase database;
        public MongoContext()        //constructor   
        {

            // Reading credentials from Web.config file   
            var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoHost"]);
            database = mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDatabaseName"]);



        }
    }
}