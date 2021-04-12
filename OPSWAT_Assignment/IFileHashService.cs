using Newtonsoft.Json.Linq;
using OPSWAT_Assignment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OPSWAT_Assignment
{
    /// <summary>
    /// This class acts as interface for FileHashService.cs class
    /// </summary>
    public interface IFileHashService
    {

        /// <summary>
        /// This method is used to generate the hash of a given file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string CalculateMD5(string filename);

        /// <summary>
        /// This method is used to initiate a Post request to upload file to server and return response object
        /// </summary>
        /// <param name="path"></param>
        RootObject UploadFile(string path);

        /// <summary>
        /// This method is used to make a Get Api call using the file hash value
        /// </summary>
        /// <param name="fileHashValue"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string GetFileByHashValue(string fileHashValue, string fileName);

        /// <summary>
        /// This method is used to make a Get Api call using the data Id received from uploading the file
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string GetFileById(string id, string fileName);
    }
}
