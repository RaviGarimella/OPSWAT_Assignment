using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OPSWAT_Assignment.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OPSWAT_Assignment
{
    /// <summary>
    /// This class contains the methods required for the project
    /// </summary>
    public class FileHashService : IFileHashService
    {

        /// <summary>
        /// This method is used to generate the hash of a given file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string CalculateMD5(string filename)
        {            
            string text = System.IO.File.ReadAllText(filename);

            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = File.ReadAllBytes(filename);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();            
            }            
        }


        /// <summary>
        /// This method is used to initiate a Post request to upload file to server and return response object
        /// </summary>
        /// <param name="path"></param>
        public RootObject UploadFile(string path)
        {
            var result = new RootObject();

            using (var httpClient = new HttpClient())
            {
                var apiKey = ConfigurationManager.AppSettings["apiKey"];
                var postUrl = ConfigurationManager.AppSettings["apiPostUrl"];

                httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
                
                byte[] fileBytes = File.ReadAllBytes(path);

                var content = new ByteArrayContent(fileBytes, 0, fileBytes.Length);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var response = httpClient.PostAsync(postUrl, content).Result;                
                if (response.IsSuccessStatusCode)
                {
                    var str = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<RootObject>(str);
                }
            }
            return result;
        }


        /// <summary>
        /// This method is used to make a Get Api call using the file hash value
        /// </summary>
        /// <param name="fileHashValue"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFileByHashValue(string fileHashValue, string fileName)
        {
            var content = string.Empty;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.metadefender.com/v4/hash/" + fileHashValue))
                {
                    var apiKey = ConfigurationManager.AppSettings["apiKey"];
                    request.Headers.TryAddWithoutValidation("apikey", apiKey);

                    var response = httpClient.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        JObject json = JObject.Parse(result);
                        return GenerateFileOutput(json, fileName);
                    }
                }
            }
            return content;
        }


        /// <summary>
        /// This method is used to make a Get Api call using the data Id received from uploading the file
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFileById(string id, string fileName)
        {
            var content = string.Empty;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.metadefender.com/v4/file/" + id))
                {
                    var apiKey = ConfigurationManager.AppSettings["apiKey"];
                    request.Headers.TryAddWithoutValidation("apikey", apiKey);

                    var response = httpClient.SendAsync(request).Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(result);
                        JObject json = JObject.Parse(result);
                        return GenerateFileOutput(json, fileName);
                    }
                }
            }
            return content;
        }


        /// <summary>
        /// This method is used to construct the file output response message
        /// </summary>
        /// <param name="json"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GenerateFileOutput(JObject json, string fileName)
        {
            JObject scanDetails = json["scan_results"]["scan_details"].Value<JObject>();

            List<string> keys = scanDetails.Properties().Select(p => p.Name).ToList();

            FileOutputData fileOutput = new FileOutputData
            {
                FileName = fileName,
                OverallStatus = json["scan_results"]["scan_all_result_a"].ToString(),
                EngineData = new List<EngineData>()
            };

            foreach (string key in keys)
            {
                EngineData engineData = new EngineData
                {
                    EngineName = key,
                    ThreatFound = scanDetails[key]["threat_found"].ToString(),
                    ScanTime = scanDetails[key]["scan_time"].ToString(),
                    ScanResult_i = scanDetails[key]["scan_result_i"].ToString(),
                    DefTime = scanDetails[key]["def_time"].ToString()
                };

                fileOutput.EngineData.Add(engineData);
            }

            return fileOutput.ToString();
        }
    }
}
