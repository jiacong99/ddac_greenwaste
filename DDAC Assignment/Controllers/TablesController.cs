using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DDAC_Assignment.Models;

namespace DDAC_Assignment.Controllers
{
    public class TablesController : Controller
    {
            private CloudTable getBlobContainerInformation()
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                IConfiguration configure = builder.Build();

                CloudStorageAccount accountdetails = CloudStorageAccount.Parse(configure["ConnectionStrings:blobstorageconnection"]);
                CloudTableClient clientagent = accountdetails.CreateCloudTableClient();
                CloudTable table = clientagent.GetTableReference("Driver");
                return table;
            }
        public IActionResult CreateTable()
        {
            CloudTable table = getBlobContainerInformation();

            ViewBag.result = table.CreateIfNotExistsAsync().Result;
            ViewBag.TableName = table.Name;
            return View();
        }

        //insert data into table storage
        public ActionResult AddSingleEntity(string PartitionKey, string RowKey, string Address, string Email, string PhonNumber)
        {
            DriverEntity driver  = new DriverEntity (PartitionKey, RowKey);
            driver.Address = Address;
            driver.Email = Email;
            driver.PhoneNumber = PhonNumber;

            CloudTable table = getBlobContainerInformation();
            try
            {
                TableOperation insertOperation = TableOperation.Insert(driver); // create the action
                TableResult insertresult = table.ExecuteAsync(insertOperation).Result; //run actiuon and result
                ViewBag.result = insertresult.HttpStatusCode; // get the network sucess code = 204
                ViewBag.TableName = table.Name;
            }
            catch (Exception ex)
            {
                ViewBag.result = 100;
                ViewBag.message = "Error: " + ex.ToString();
            }
            return View();

        }

        //2. how to insert multiple information at once using the same form 
        public ActionResult AddEntities()
        {
            CloudTable table = getBlobContainerInformation();
            //same group of data can use the batch operation
            string[,] driver =
            {
                {"ah", "cong", "Sarwak", "cong@gamil.com", "018-9980766" },
                {"ah", "Lu", "China", "Ah@gamil.com", "018-9930766" },
            };// array

            TableBatchOperation batchOperation = new TableBatchOperation();

            for (int i = 0; i < 2; i++)
            {
                DriverEntity dv = new DriverEntity(driver[i,0], driver[i,1]);
                dv.Address = driver[i,2];
                dv.Email = driver[i, 3];
                dv.PhoneNumber = driver[i,4];
                batchOperation.Insert(dv); //collect the data
            }
            //execute
            try
            {
                IList<TableResult> results = table.ExecuteBatchAsync(batchOperation).Result;
                return View(results); //display in the front end - to show what been insert.
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Error: " + ex.ToString();
            }
            return View();
        }

        //3. how to user a form to get the user input and submit to the table storage
        public ActionResult RegisterDriver()
        {

            return View();
        }

        //4. how to make a search page for searching a specific driver information
        public ActionResult SearchIndividual(string Message = null)
        {
            ViewBag.msg = Message;
            return View();
        }

        //5. how to get the single entity values from the table storage
        public ActionResult getsingleentity(string PartitionName, string RowName)
        {
            CloudTable table = getBlobContainerInformation();
            string message = null;

            try
            {
                TableOperation retrieveOpeartion = TableOperation.Retrieve<DriverEntity>(PartitionName,RowName);
                TableResult result = table.ExecuteAsync(retrieveOpeartion).Result;
                if(result.Etag != null)
                {
                    var driver = result.Result as DriverEntity; // convert the driver information from table result to become driver entity type
                    return View(driver);
                }
                else
                {
                    message = "No such Data Exists";
                }
            }
            catch(Exception ex)
            {
                message = "Unable to get the data from the table. Error : " + ex.ToString();
            }
            return RedirectToAction("SearchIndividual", "Tables", new { Message = message });
        }

        //6. How to delete entity from table storage
        public ActionResult delete_entity(string PartitionKey, string RowKey)
        {
            CloudTable table = getBlobContainerInformation();
            string message = null;
            DriverEntity deleteddriverinfo = new DriverEntity(PartitionKey, RowKey) { ETag = "*" }; // 

            try
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteddriverinfo);
                table.ExecuteAsync(deleteOperation);
                message = "Driver " + PartitionKey + "" + RowKey + " is deleted from the table! ";
            }
            catch(Exception ex)
            {
                message = "Unable to delete the data. Error : " + ex.ToString();
            }
            return RedirectToAction("SearchIndividual","Tables", new { Message = message});
        }

        //7. How to edit entity from table storage
        public ActionResult edit_entity(string PartitionKey, string RowKey)
        {
            CloudTable table = getBlobContainerInformation();
            string message = null;

            try
            {
                TableOperation retrieveOpeartion = TableOperation.Retrieve<DriverEntity>(PartitionKey, RowKey);
                TableResult result = table.ExecuteAsync(retrieveOpeartion).Result;
                if (result.Etag != null)
                {
                    var driver = result.Result as DriverEntity; // convert the driver information from table result to become driver entity type
                    return View(driver);
                }
                else
                {
                    message = "No such Data Exists";
                }
            }
            catch (Exception ex)
            {
                message = "Unable to get the data from the table. Error : " + ex.ToString();
            }
            return RedirectToAction("SearchIndividual", "Tables", new { Message = message });
        }

        //8. update action function
        [HttpPost]
        public ActionResult edit_entity([Bind("PartitionKey, RowKey, Address, Email, PhoneNumber")] DriverEntity driver)
        {
            CloudTable table = getBlobContainerInformation();
            string message = null;
            if(ModelState.IsValid)
            {
                driver.ETag = "*";
                try
                {
                    TableOperation editOperation = TableOperation.Replace(driver);
                    table.ExecuteAsync(editOperation);
                    message = "Driver Information: " + driver.PartitionKey + " " + driver.RowKey + " is updated!"; 
                }
                catch(Exception ex)
                {

                }
            }
            else
            {
                return RedirectToAction("edit_entity", "Tables", new { PartitionKey = driver.PartitionKey, RowKey = driver.RowKey });
            }
            return RedirectToAction("SearchIndividual", "Tables", new { Message = message });
        }
    }
}
