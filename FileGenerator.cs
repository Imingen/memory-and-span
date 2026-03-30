using System.Diagnostics;
using System.Net;

namespace MemoryAndSpan;

public class FileGenerator
{
    private const string FILEPATH = "output.txt";
    private const int MAX_LOG_ENTRIES = 100_000_000;

    public void GenerateFile()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var ips = generateIPAddresses(50);
        var random = new Random();
        var statusCodes = new Dictionary<int, int>
        {
            { 200, 90 },
            { 500, 5  },
            { 404, 5  }
        };

        using StreamWriter sw = new(FILEPATH);
        for (int i = 0; i < MAX_LOG_ENTRIES; i++)
        {
            var randomStatusCodeValue = random.Next(0, 100);
            var logEntry = new LogFileEntry
            (
                TimeStamp: DateTime.Now,
                IPAddress: ips.ElementAt(random.Next(0, ips.Count)),
                HttpMethod: "GET",
                URL: "www.dummy.url.com",
                StatusCode: getRandomStatusCode(statusCodes, randomStatusCodeValue),
                ResponseTimeMs: random.Next(0, 250)
            );
            sw.WriteLine(logEntry.ToString());
        }
        stopwatch.Stop();

        var logFileSize = new FileInfo(FILEPATH).Length;
        Console.WriteLine($"Wrote a logfile with size: {logFileSize / (1024 * 1024)} MB. It took {stopwatch.Elapsed.TotalSeconds} seconds.");
    }

    private List<string> generateIPAddresses(int amount)
    {
        Random random = new();
        var ips = new List<string>();
        for (int i = 1; i <= amount; i++)
        {
            var ipAddress = new IPAddress(i * random.NextInt64(amount, 1000)).ToString();
            ips.Add(ipAddress);
        }
        return ips;
    }

    private int getRandomStatusCode(Dictionary<int, int> weights, int randomValue)
    {
        var cumulativeWeight = 0;

        foreach (var entry in weights)
        {
            cumulativeWeight += entry.Value;
            if (randomValue <= cumulativeWeight)
            {
                return entry.Key;
            }
        }

        return 200;
    }
}


public record LogFileEntry(
    DateTime TimeStamp,
    string IPAddress,
    string HttpMethod,
    string URL,
    int StatusCode,
    int ResponseTimeMs)
{
    public override string ToString()
    {
        return $"{TimeStamp} {IPAddress} {HttpMethod} {URL} {StatusCode} {ResponseTimeMs}";
    }
};
