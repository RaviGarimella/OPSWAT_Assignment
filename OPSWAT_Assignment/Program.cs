using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Threading;

namespace OPSWAT_Assignment
{
    public class Program
    {        
        static void Main(string[] args)
        {
            try
            {
                if (args != null && args.Length != 0)
                {  
                    // Get the file path location
                    string filePath = args[0];

                    // Get the API key from the config file
                    var apiKey = ConfigurationManager.AppSettings["apiKey"];

                    // Get the URL for GET request from the config file
                    var getUrl = ConfigurationManager.AppSettings["apiGetUrl"];

                    IFileHashService fileHash = new FileHashService();

                    // Calculate the MD5 hash value of the given file
                    string md5HashValue = fileHash.CalculateMD5(filePath);                    

                    // Use the HttpClient to initiate the GET request to the Metadefender API
                    using var client = new HttpClient();
                    client.BaseAddress = new Uri(getUrl);
                    client.DefaultRequestHeaders.Add("apiKey", apiKey);
                    
                    string response = fileHash.GetFileByHashValue(md5HashValue, filePath);

                    // If previously cached results of given file is present, display its response
                    if (response != string.Empty)
                    {
                        Console.WriteLine(response);
                    }
                    else  // If the file is scanned for first time, upload the file to server and make GET request to API in intervals
                    {
                        var responseObj = fileHash.UploadFile(filePath);
                        Thread.Sleep(10000);
                        string dataResponse = fileHash.GetFileById(responseObj.data_id, filePath);
                        Console.WriteLine(dataResponse);
                    }
                }
                else
                {
                    Console.WriteLine("Please provide a file to scan");
                }
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"The File scan is Unsuccessful. The error message is: {ex.Message}");
            }
        }        
    }
}
