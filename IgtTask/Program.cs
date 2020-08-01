using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace IgtTask
{
    public static class InputConverter
    {
        public static bool ConvertInput(string input, string delimiter, out string csv_line)
        {
            Regex pattern = new Regex(@"([\d\-]+)\s([\d\:\.]+).+Request for (\w+)_(\w+)_(\w+)_(\w+)");
            int regex_group_count = 6;
            csv_line = "";

            Match match = pattern.Match(input);
            if (match.Success)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i <= regex_group_count; i++)
                {
                    sb.Append(match.Groups[i].Value);
                    if (i != regex_group_count) sb.Append(delimiter);
                }
                csv_line = sb.ToString();
                return true;
            }            
            return false;
        }
    }
    class Program
    {
        static int Main(string[] args)
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();

            string filePath = args[0];

            string delimiter = ",";

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the filename.");
                return 1;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"The file {filePath} does not exist.");
                return 1;
            }            

            string csvHeader = $"Date{delimiter}TimeX{delimiter}Y{delimiter}X{delimiter}K";

            using (TextWriter sw = new StreamWriter($"{currentDirectory}\\files.csv"))
            using (TextReader rdr = new StreamReader(filePath))
            {
                sw.WriteLine(csvHeader);
                string line;

                while ((line = rdr.ReadLine()) != null)
                {
                    bool res = InputConverter.ConvertInput(line, delimiter, out string csv_line);
                    if (res) sw.WriteLine(csv_line);
                }
            }
            return 0;
        }
    }
}
