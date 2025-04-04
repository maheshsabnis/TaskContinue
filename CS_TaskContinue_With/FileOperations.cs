using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_TaskContinue_With
{
    internal class FileOperations
    {
        public string ReadFileOne(string fileName)
        {
            try
            {
                FileStream Fs = File.OpenRead(fileName);
                StreamReader sr = new StreamReader(Fs);
                string fileContent = sr.ReadToEnd();
                sr.Close();
                Fs.Close();
                return fileContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool WriteFile(string fileName, string content)
        {
            FileStream fs = File.Create(fileName);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(content);
            sw.Close();
            fs.Close();
            return true;
        }
    }
}
