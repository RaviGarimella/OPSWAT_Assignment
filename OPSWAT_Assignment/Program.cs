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
                if (args.Length != 0)
                {
                    using (StreamReader sr = new StreamReader(args[0]))
                    {
                        // Get the API key from the config file
                        var apiKey = ConfigurationManager.AppSettings["apiKey"];

                        IFileHashService fileHash = new FileHashService();
                        String filePath = sr.ReadToEnd();

                        // Calculate the sha256 hash of given file
                        byte[] fileHashValue = fileHash.CalculateHashOfGivenFile(filePath);

                        // Convert the calculated hash value to string
                        string byteToStringValue = fileHash.ByteArrayToString(fileHashValue);

                        // use the HttpClient to initiate the GET request to the Metadefender API
                        using var client = new HttpClient();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(apiKey);
                        var result = client.GetAsync("https://api.metadefender.com/v4/file");

                        if(result.Status == TaskStatus.RanToCompletion)
                        {
                            Console.WriteLine(result.ToString());
                        }
                        else
                        {
                            Console.WriteLine($"Error occurred, the status code is: {result.Status}");
                        }
                    }
                }
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
