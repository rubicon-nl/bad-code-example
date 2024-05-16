using System;
using System.IO;

namespace RubiconTest.Tests.Helper
{
    public static class Helper
    {
        public static string ReadFileData(string response)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"MockData/{response}");
            var strSearchResult = "";
            using (StreamReader r = new StreamReader(filePath))
            {
                var reader = new StreamReader(filePath);
                strSearchResult = reader.ReadToEnd();
            }

            return strSearchResult;
        }
    }
}
