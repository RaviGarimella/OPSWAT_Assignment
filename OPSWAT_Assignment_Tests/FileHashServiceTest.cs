using NUnit.Framework;
using OPSWAT_Assignment;

namespace OPSWAT_Assignment_Tests
{
    /// <summary>
    /// This class contains the unit test for FileHashService.cs
    /// </summary>
    public class Tests
    {
        private FileHashService _fileHashService;

        [SetUp]
        public void Setup()
        {            
            _fileHashService = new FileHashService();
        }

        [Test]
        public void TestCalculateHashOfGivenFileSuccess()
        {
            string filePath = @"C:\Users\Ravi Garimella\source\repos\OPSWAT_Assignment\OPSWAT_Assignment\SampleFile.txt";
            var result = _fileHashService.CalculateHashOfGivenFile(filePath);
            Assert.IsNotNull(result);
            Assert.Greater(result.Length, 0);
        }

        [Test]
        public void TestCalculateHashOfGivenFileFailure()
        {
            string fileName = string.Empty;
            var result = _fileHashService.CalculateHashOfGivenFile(fileName);
            Assert.IsNull(result);
        }

        [Test]
        public void TestByteArrayToString()
        {
            // Create a byte array
            var fileBytes = new byte[3];
            fileBytes[0] = byte.MinValue;
            fileBytes[1] = 0;
            fileBytes[2] = byte.MaxValue;

            var result = _fileHashService.ByteArrayToString(fileBytes);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestUploadFilesToServer()
        {
            string postUrl = "https://api.metadefender.com/v4/file";
            string fileName = "Sample File Name";
            _fileHashService.UploadFilesToServer(postUrl, fileName);
        }
    }
}