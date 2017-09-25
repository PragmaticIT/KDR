using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Kdr.Database.MongoDB.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Connect()
        {
            var client = new MongoClient("mongodb://sa:kasztan1#@mycluster0-shard-00-00-wpeiv.mongodb.net:27017,mycluster0-shard-00-01-wpeiv.mongodb.net:27017,mycluster0-shard-00-02-wpeiv.mongodb.net:27017/admin?ssl=true&replicaSet=Mycluster0-shard-0&authSource=admin");
            var database = client.GetDatabase("test");
            var haha = client.Cluster.ClusterId;
            Assert.IsNotNull(haha);
        }

        [TestMethod]
        public void InsertDocument()
        {
            var client = new MongoClient("mongodb://sa:kasztan1#@mycluster0-shard-00-00-wpeiv.mongodb.net:27017,mycluster0-shard-00-01-wpeiv.mongodb.net:27017,mycluster0-shard-00-02-wpeiv.mongodb.net:27017/admin?ssl=true&replicaSet=Mycluster0-shard-0&authSource=admin");
            var database = client.GetDatabase("test");
            //var document = new BsonDocument(new Dictionary<string, string>{
            //    { "FirstName", "Jan" },
            //    { "LastName" ,"Kowalski" }
            //});
            database.CreateCollection("test");
            var collection=database.GetCollection<object>("test");
            collection.InsertOne(new { Name = "Jan Kowalski" });
           
        }
    }
}
