
using MemoryAndSpan;

var fileGenerator = new FileGenerator();
fileGenerator.GenerateFile(maxLogEntries: 100_000_000);
