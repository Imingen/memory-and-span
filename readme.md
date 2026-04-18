A simple project for learning how to use Memory<T> and Span<T> (and possible other performance types and patterns) in C#.

## LogParser
Generates a big logfile and parses it, first just by looping through and comparing strings. 
Next parsing run is by using Span<T>. It removes the string allocations on the heap when each line is split; one allocation for array and then allocation for each string item, resulting in about ~40% faster parsing time. 

Must be run with -c Release, otherwise all the debugging stuff will make the running time about equal. 
If not slower :thinking: 