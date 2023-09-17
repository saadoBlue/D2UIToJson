using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2UIToJson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] file = File.ReadAllBytes(@".\d2ui\Ankama_Connection.d2ui");
            PreCompiledUiModuleReader reader = new PreCompiledUiModuleReader();
            var DecompiledUiModule = reader.ReadFromStream(new MemoryStream(file));

            string json = JsonConvert.SerializeObject(DecompiledUiModule, Formatting.Indented);
            string filePath = @".\d2ui\Ankama_Connection.json";
            File.WriteAllText(filePath, json);
        }
    }
}
