using System.Diagnostics;

namespace MemoryAndSpan;

public class FastLogParser : ILogParser
{
    public void ParseLog(string filePath)
    {
        var ipAddress = new Dictionary<string, int>();
        var sw = new Stopwatch();
        sw.Start();
        using var sr = new StreamReader(filePath);
        while (sr.ReadLine() is string line)
        {
            ReadOnlySpan<char> remaining = line.AsSpan();

            var spaceIndex = remaining.IndexOf(' ');
            remaining = remaining.Slice(spaceIndex + 1);

            spaceIndex = remaining.IndexOf(' ');
            remaining = remaining.Slice(spaceIndex + 1);

            spaceIndex = remaining.IndexOf(' ');
            ReadOnlySpan<char> ipSpan = remaining.Slice(0, spaceIndex);
            remaining = remaining.Slice(spaceIndex + 1);

            spaceIndex = remaining.IndexOf(' ');
            remaining = remaining.Slice(spaceIndex + 1);

            spaceIndex = remaining.IndexOf(' ');
            remaining = remaining.Slice(spaceIndex + 1);

            spaceIndex = remaining.IndexOf(' ');
            ReadOnlySpan<char> httpCodeSpan = remaining.Slice(0, spaceIndex);

            if (httpCodeSpan.SequenceEqual("500"))
            {
                var ipAddressString = ipSpan.ToString();
                if (ipAddress.ContainsKey(ipAddressString))
                {
                    ipAddress[ipAddressString]++;
                }
                else
                {
                    ipAddress.Add(ipAddressString, 1);
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