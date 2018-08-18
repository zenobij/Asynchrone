using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;

namespace BLL.Repository
{
    public class RepoFile : IDisposable
    {
        public IEnumerable<string> ExtractZipFiles(string filePath)
        {
            //await Task.Delay(200);
            string finalDirectoryName = $@"C:\Users\studentdot0403\Desktop\Extraction\{filePath.Split('\\').Last().Replace(".zip", "")}";
            
            ZipFile.ExtractToDirectory(filePath, finalDirectoryName);
            List<string> myList = new List<string>();
            foreach (string path in Directory.EnumerateFiles(finalDirectoryName))
            {
                if (path.EndsWith(".txt") && path.Contains("readme.txt") == false)
                {
                    Debug.WriteLine("Id : " + Thread.CurrentThread.ManagedThreadId + ". Threadpool : " + Thread.CurrentThread.IsThreadPoolThread);
                    myList.AddRange(GetLines(path));
                }
            }

            
            return myList;

        }

        private static IEnumerable<string> GetLines(string path)
        {
            List<string> myList = new List<string>();
            using (FileStream fs = new FileStream(path, FileMode.Open,FileAccess.Read))
            {
                
                using (StreamReader sr = new StreamReader(path))
                {
                    
                    while (!sr.EndOfStream)
                    {
                        yield return sr.ReadLine();
                    }
                }
            }
        }

        public void Dispose()
        {
            
        }
    }
}
