using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest
{
    class Pipeline
    {
        private const string path = "d:\\final.txt";
        public void Push(string time, string text)
        {
            string txt = "{\n    'time':'" + time + "',\n    'content':'" + text.Replace('\n', ' ') + "'\n}";
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(txt);
            sw.Flush();
            sw.Close();
            fs.Close();
            Console.WriteLine("file saved:" + txt);
        }
    }
}
