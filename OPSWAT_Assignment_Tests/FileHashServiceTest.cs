using NUnit.Framework;
using OPSWAT_Assignment;

namespace OPSWAT_Assignment_Tests
{
    /// <summary>
    /// This class contains the unit test for FileHashService.cs
    /// </summary>
    public class Tests
    {
        private IFileHashService _fileHashService;

        [SetUp]
        public void Setup()
        {            
            _fileHashService = new FileHashService();
        }

        [Test]
        public void TestCalculateHashOfGivenFileSuccess()
        {
            string filePath = @"C:\Users\Ravi Garimella\source\repos\OPSWAT_Assignment\OPSWAT_Assignment\SampleFile.txt";
            var result = _fileHashService.CalculateMD5(filePath);
            Assert.IsNotNull(result);
            Assert.Greater(result.Length, 0);
        }

        [Test]
        public void TestCalculateHashOfGivenFileFailure()
        {
            string fileName = string.Empty;
            var result = _fileHashService.CalculateMD5(fileName);
            Assert.IsNull(result);
        }

        [Test]
        public void TestUploadFile()
        {
            string filePath = @"C:\Users\Ravi Garimella\source\repos\OPSWAT_Assignment\OPSWAT_Assignment\SampleFile.txt";
            var responseObj = _fileHashService.UploadFile(filePath);
            Assert.IsNotNull(responseObj);
        }
    }
}