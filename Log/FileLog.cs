using System;
using System.IO;

namespace Log
{
    public class FileLog : ILog
    {
        private readonly string file_path;

        public FileLog(string path)
        {
            file_path = path;
        }

        public void print(string message)
        {
            try
            {
                File.AppendAllText(file_path, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with uploading in file: {ex.Message}");
            }
        }
    }
}