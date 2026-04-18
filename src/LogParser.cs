using System.Diagnostics;

namespace MemoryAndSpan;

public class LogParser : ILogParser
{
    public void ParseLog(string filePath)
    {
        var ipAddress = new Dictionary<string, int>();
        var sw = new Stopwatch();
        sw.Start();
        using var sr = new StreamReader(filePath);
        while (sr.ReadLine() is string line)
        {
            var parts = line.Split(' ');

            if (parts[5] == "500")
            {
                if (ipAddress.ContainsKey(parts[2]))
                {
                    ipAddress[parts[2]]++;
                }
                else
                {
                    ipAddress.Add(parts[2], 1);
                }

            }
        }

        var sortedList = ipAddress.OrderByDescending(pair => pair.Value).ToList();
        Console.WriteLine("Top 5 highest 500 IP addresses found in logfile");
        for (int i = 0; i < Math.Min(5, sortedList.Count()); i++)
        {
            Console.WriteLine($"#{i}: {sortedList[i].Key}");
        }
        sw.Stop();
        Console.WriteLine($"It took {sw.Elapsed.TotalSeconds} seconds.");
    }
}