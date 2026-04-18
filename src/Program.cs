
using MemoryAndSpan;

/* var fileGenerator = new FileGenerator();
fileGenerator.GenerateFile(maxLogEntries: 100_000_000); */

var logParser = new LogParser();
logParser.ParseLog("../output/output.txt");

//Force cleanup so no "hangover" for next run
GC.Collect();
GC.WaitForPendingFinalizers();

//must be ran with -c Release 
var betterLogParser = new FastLogParser();
betterLogParser.ParseLog("../output/output.txt");
