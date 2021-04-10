using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OPSWAT_Assignment
{
    /// <summary>
    /// This class contains the methods for performing hash on a given file
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
        public byte[] CalculateHashOfGivenFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                using (FileStream fileStream = File.OpenRead(fileName))
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
            StringBuilder sb = new StringBuilder(fileBytes.Length);

            foreach (byte fb in fileBytes)
            {
                sb.Append(fb.ToString());
            }

            return sb.ToString();
        }
    }
}
