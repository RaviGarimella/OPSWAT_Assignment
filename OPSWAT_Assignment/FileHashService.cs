using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        // The cryptographic service provider
        private SHA256 sha256 = SHA256.Create();

        /// <summary>
        /// This method is used to generate the hash of a given file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] CalculateHashOfGivenFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    return sha256.ComputeHash(fileStream);
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method is used to convert a byte array to string
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <returns></returns>
        public string ByteArrayToString(byte[] fileBytes)
        {
            if (fileBytes?.Length > 0)
            {
                StringBuilder sb = new StringBuilder(fileBytes.Length);

                foreach (byte fb in fileBytes)
                {
                    sb.Append(fb.ToString());
                }

                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// This method is used to upload file to server
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="fileName"></param>
        public void UploadFilesToServer(string postUrl, string fileName)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(fileName);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = httpClient.PostAsync(postUrl, data);

                    string message = "File upload is successful. Please find the details below: ";
                    string responseMessage = GenerateAPIResponse(response, message);
                    Console.WriteLine(responseMessage);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"File Upload is Unsuccessful. The error message is: {ex.Message}");
            }
        }


        /// <summary>
        /// This method is used to construct the API response message
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public string GenerateAPIResponse(Task<HttpResponseMessage> response, string message)
        {
            StringBuilder responseMessage = new StringBuilder();

            responseMessage.Append(message);
            responseMessage.AppendLine();
            responseMessage.AppendLine(response.Result.StatusCode.ToString());
            responseMessage.AppendLine(response.Result.Content.Headers.ToString());
            responseMessage.AppendLine(response.Result.RequestMessage.Method.ToString());
            responseMessage.AppendLine(response.Result.RequestMessage.RequestUri.ToString());
            responseMessage.AppendLine(response.Id.ToString());
            responseMessage.AppendLine(response.Status.ToString());

            return responseMessage.ToString();
        }
    }
}
