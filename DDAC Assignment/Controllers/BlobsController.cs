using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.AspNetCore.Http;

namespace DDAC_Assignment.Controllers
{
    public class BlobsController : Controller
    {
        private CloudBlobContainer getBlobContainerInformation()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfiguration configure = builder.Build();

            CloudStorageAccount accountdetails = CloudStorageAccount.Parse(configure["ConnectionStrings:blobstorageconnection"]);
            CloudBlobClient clientagent = accountdetails.CreateCloudBlobClient();
            CloudBlobContainer container = clientagent.GetContainerReference("container");
            return container;
        }

        public IActionResult CreateContainer()
        {
            CloudBlobContainer container = getBlobContainerInformation();

            ViewBag.result = container.CreateIfNotExistsAsync().Result;
            ViewBag.ContainerName = container.Name;
            return View();
        }

        public string UploadTextFile()
        {
            CloudBlobContainer container = getBlobContainerInformation();

            CloudBlockBlob blobitem = container.GetBlockBlobReference("mytextfile.txt");

            try
            {
                var filestream = System.IO.File.OpenRead(@"C:\\Users\\Justin Siaw\\Desktop\\ODL Assignmnet.txt");
                using (filestream)
                {
                    blobitem.UploadFromStreamAsync(filestream).Wait();
                }
            }
            catch (Exception ex)
            {
                return "Technical issue: " + ex.ToString() + ". Please upload the file again!";
            }

            return blobitem.Name + "is successfully uploaded in the blob storage now. The URL for the blob as below. URL: " + blobitem.Uri;
        }


        //4. how to use form data to upload a file
        public ActionResult UploadFileFromForm(string Message = null)
        {
            ViewBag.msg = Message;
            return View();
        }

        [HttpPost]
        public String UploadFileFromForm(List<IFormFile>files)
        {
            
            CloudBlobContainer container = getBlobContainerInformation();

            CloudBlockBlob blobitem = null;
            string message = null;
            
            foreach (var file in files)
            {
                try
                {
                    blobitem = container.GetBlockBlobReference(file.FileName);
                    var stream = file.OpenReadStream();
                    blobitem.UploadFromStreamAsync(stream).Wait();
                    message = "The file of " + blobitem.Name + " is uploaded now!"; 
                }
                catch(Exception ex)
                {
                    message = " The file of " + blobitem.Name + " is not able to upload tot he storage!";
                }
            }

            return blobitem.Name;
        }

        //learn how to view the blob item in a page
        public ActionResult ListBlobsAsGallery(string Message = null)
        {
            ViewBag.msg = Message;
            CloudBlobContainer container = getBlobContainerInformation();

            //create empty list
            List<string> bloblist = new List<string>();

            //get the blob list from storage
            BlobResultSegment listing = container.ListBlobsSegmentedAsync(null).Result;

            //read blob by blob from listing
            foreach(IListBlobItem item in listing.Results)
            {
                //check the blob type (page blob / block blob / append blob / directory
                if(item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    if(Path.GetExtension(blob.Name) == ".jpg" || Path.GetExtension(blob.Name) == ".png")
                    {
                        bloblist.Add(blob.Name + '#' + blob.Uri); 
                    }
                }
                else if(item.GetType() == typeof(CloudPageBlob))
                {

                }
                else if(item.GetType() == typeof(CloudBlobDirectory))
                {

                }
            }
            return View(bloblist);
        }

        //how to delete a blob from blob storage 
        public ActionResult deleteblob(string imagename)
        {
            string message = null;

            CloudBlobContainer container = getBlobContainerInformation();
            
            try
            {
                CloudBlockBlob item = container.GetBlockBlobReference(imagename);
                message = " The blob of " + item.Name + "is deleted from the storage";
                item.DeleteIfExistsAsync().Wait();
            }
            catch(Exception ex)
            {
                message = "The selected blob of " + imagename + " is unable to delete";
            }
            return RedirectToAction("ListsBlobsAsGallery", "Blobs", new { Message = message});
        }

        //how to download the blob from the blob storage
        public ActionResult downloadblob(string imagename)
        {
            string message = null;
            CloudBlobContainer container = getBlobContainerInformation();

            try
            {
                CloudBlockBlob item = container.GetBlockBlobReference(imagename);
                var outputitem = System.IO.File.OpenWrite(@"C:\\Users\\Justin Siaw\\Desktop\\" + imagename);
                item.DownloadToStreamAsync(outputitem).Wait();
                message = imagename + " is successfully downloaded to your desktop!";
                outputitem.Close();

            }
            catch (Exception ex)
            {
                message = "The selected blob of " + imagename + " is unable to download";
            }
            return RedirectToAction("ListsBlobsAsGallery", "Blobs", new { Message = message });
        }
    }
}
