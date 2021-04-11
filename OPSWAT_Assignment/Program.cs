using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OPSWAT_Assignment
{
    public class Program
    {        
        static void Main(string[] args)
        {
            try
            {
                string filePath = @"C:\Users\Ravi Garimella\source\repos\OPSWAT_Assignment\OPSWAT_Assignment\SampleFile.txt";
                //if (args != null && args.Length != 0)
                //{
                //using (StreamReader sr = new StreamReader(args[0]))
                //    {
                        // Get the API key from the config file
                        var apiKey = ConfigurationManager.AppSettings["apiKey"];

                        // Get the URL for GET request from the config file
                        var getUrl = ConfigurationManager.AppSettings["apiGetUrl"];

                        IFileHashService fileHash = new FileHashService();
                        //String filePath = sr.ReadToEnd();

                        // Calculate the sha256 hash of given file
                        byte[] fileHashValue = fileHash.CalculateHashOfGivenFile(filePath);

                        // Convert the calculated hash value to string
                        string byteToStringValue = fileHash.ByteArrayToString(fileHashValue);

                        // Use the HttpClient to initiate the GET request to the Metadefender API
                        using var client = new HttpClient();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(apiKey);
                        var response = client.GetAsync(getUrl);

                        if(response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string message = "The Website is up, Please find the details below: ";
                            string responseMessage = fileHash.GenerateAPIResponse(response, message);
                            Console.WriteLine(responseMessage);
                        }
                        else
                        {
                            string message = "Error occurred, Please find the details below: ";
                            string responseMessage = fileHash.GenerateAPIResponse(response, message);
                            Console.WriteLine(responseMessage);
                        }

                // Get the File Upload URL for POST request from the config file
                var postUrl = ConfigurationManager.AppSettings["apiPostUrl"];

                fileHash.UploadFilesToServer(postUrl, byteToStringValue);
                    //}
                //}
            }
            catch(Exception ex)
            {
                Console.WriteLine($"The File scan is Unsuccessful. The error message is: {ex.Message}");
            }
        }        
    }

    //static TextReader input = Console.In;
    //var path = args[0];
    //if(File.Exists(path))
    //{
    //    input = File.OpenText(path);
    //}
    //foreach (string s in args)
    //string fileName = @"C:\Users\Ravi Garimella\source\repos\OPSWAT_Assignment\OPSWAT_Assignment\SampleFile.txt";
}
