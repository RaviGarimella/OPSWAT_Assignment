using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPSWAT_Assignment.Data
{
    /// <summary>
    /// This class represents the output file data
    /// </summary>
    public class FileOutputData
    {
        public string FileName { get; set; }
        public List<EngineData> EngineData { get; set; }
        public string OverallStatus { get; set; }
        public override string ToString()
        {
            string output = "FileName: " + FileName + "\n" + "OverallStatus: " + OverallStatus + "\n";
            foreach(EngineData eData in EngineData)
            {
                output += eData.ToString();
            }

            output += "END";
            return output;
        }        
    }


    /// <summary>
    /// This class represents the Engine Information in file output
    /// </summary>
    public class EngineData
    {
        public string EngineName { get; set; }
        public string ThreatFound { get; set; }
        public string ScanTime { get; set; }
        public string ScanResult_i { get; set; }
        public string DefTime { get; set; }

        public override string ToString()
        {
            return "engine: " + EngineName + "\n" + "threat_found: " + ThreatFound + "\n" +
                   "scan_result: " + ScanResult_i + "\n" + "def_time: " + DefTime + "\n";
        }
    }
}
