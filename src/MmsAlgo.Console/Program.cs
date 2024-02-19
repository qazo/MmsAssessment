// See https://aka.ms/new-console-template for more information

using MmsAlgo.Core;

string Prompt(string str)
{
    Console.Write(str);
    return Console.ReadLine()!;
}

var finder = new FirstMajorityFinder();

var sample = new int[]{3, 3, 4, 2, 4, 4, 2, 4, 4};
var result = finder.FindElement(sample);
Console.WriteLine($"Sample 1: {result}");

sample = new int[] { 3, 3, 4, 2, 4, 2, 2 };
result = finder.FindElement(sample);
Console.WriteLine($"Sample 2: {result}");

