using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Helpers
{
    public class FileHelper
    {
        private const string AccountFileName = "..\\Accounts.txt";

        public void ClearFile()
        {
            using (StreamWriter sw = new StreamWriter(AccountFileName, false))
            {
                sw.WriteLine("");
                sw.Close();
            }
        }

        public void CreateFile(string content)
        {
            using (StreamWriter sw = new StreamWriter(AccountFileName, true))
            {
                sw.WriteLine();
                sw.Write(content);

                sw.Dispose();
            }
        }
    }
}
