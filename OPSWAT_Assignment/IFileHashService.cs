using System;
using System.Collections.Generic;
using System.Linq;
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
        byte[] CalculateHashOfGivenFile(string fileName);

        /// <summary>
        /// This method is used to convert a byte array to string
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <returns></returns>
        string ByteArrayToString(byte[] fileBytes);
    }
}
