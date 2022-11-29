using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Azure.Storage.Blobs;
using WebPage_Uploads.DataDB;

namespace WebPage_Uploads.Controllers
{
    internal class AzureBlobClient
    {
        //public static async Task UploadBlob(string imgpath, string filename)
        //{
        //    var connectionStr = "DefaultEndpointsProtocol=https;AccountName=sanidhyastorage;AccountKey=TCRpncR3imOIQwD4QD/t78IeC5dagZVrIoSysbvm/F4hYVVyNn1XP/bqSBNbCKwtI3W/XQR2qVfd+AStU8HMuA==;EndpointSuffix=core.windows.net";
        //    var containerName = "jamesbond";
        //    var client_service = new BlobServiceClient(connectionStr);
        //    var client_container = client_service.GetBlobContainerClient(containerName);
        //    string imgfullpath = "";
        //    //var localpath = Path.GetFullPath(filename);
        //    var localpath = Path.Combine(filename);

        //    //await Task.Run(()=>File.WriteAllText(localpath, "Uplaoding A New File From a User."));
        //    var blobclient = client_container.GetBlobClient(@"Sanidhya/" + filename);

        //    Console.WriteLine($"Uploading to {containerName}/Sanidhya");

        //    //Uploading
        //    //blobclient.UploadAsync(filename);
        //    FileStream uploadFileStream = File.OpenRead(localpath);
        //    await blobclient.UploadAsync(uploadFileStream, true);

        //    //uploadFileStream.Close();
        //    Console.WriteLine($"Uploaded- {filename} in: \ncontainer :\t {containerName}/Sanidhya\n");
        //}

        public static async Task<BlobRecord> UploadBlob(HttpPostedFileBase file, string filename)
        {
            var connectionStr = "DefaultEndpointsProtocol=https;AccountName=sanidhyastorage;AccountKey=TCRpncR3imOIQwD4QD/t78IeC5dagZVrIoSysbvm/F4hYVVyNn1XP/bqSBNbCKwtI3W/XQR2qVfd+AStU8HMuA==;EndpointSuffix=core.windows.net";
            var containerName = "jamesbond";

            var client_service = new BlobServiceClient(connectionStr);
            var client_container = client_service.GetBlobContainerClient(containerName);
            var blobclient = client_container.GetBlobClient(@"Sanidhya/" + filename);

            Console.WriteLine($"Uploading to {containerName}/Sanidhya");
            ////About Content
            //int nFileLen = file.ContentLength;
            //byte[] scriptData = new byte[nFileLen];

            // Read uploaded file from the Stream
            //file.InputStream.Read(scriptData, 0, nFileLen);
            //string filePath = "~\\App_Data\\";
            //filePath = filePath + "\\" + filename;
            await blobclient.UploadAsync(file.InputStream);
            var blob_uri = blobclient.Uri;
            //var blob = new BlobClient(connectionStr, containerName, filename);

            return new BlobRecord()
            {
                blob_name = filename,
                date = DateTime.Now,
                url = blob_uri.ToString()
            };
           
            //BlobRecordsController.Create(new BlobRecord() { 
            //                                                blob_name = filename, 
            //                                                date = DateTime.Now, 
            //                                                url = blob_uri.ToString() 
            //                                                }
            //                            );
            Console.Write("Haha");
            /*(Guid.NewGuid(), filename, DateTime.Now, blob_uri);*/


            //    //Uploading

        }

    }
}