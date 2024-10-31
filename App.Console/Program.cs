using App.Console.Extensions;
using App.Console.Impl;
using Data.Entities;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

Stopwatch timer = new();
try
{
    timer.Start();

    IConfiguration config = new ConfigurationBuilder().GetCurrentConfiguration();
    Console.WriteLine(config.GetApplicationInformation());
    foreach (string path in Directory.GetFiles(config.GetDataPath()))
    {
        Console.WriteLine($"Processing: {Path.GetFileName(path)}");

        FileProcessor<Todo> processor = Factory.CreateProcessor<Todo>(path);
        IEnumerable<Todo>? todos = await processor.Process();
        todos.WriteToConsole();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    timer.Stop();
    Console.WriteLine("Application ran in " + timer.Elapsed.TotalMilliseconds + "ms");
}