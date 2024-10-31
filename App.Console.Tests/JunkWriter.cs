using Data.Entities;
using Shouldly;
using System.Text.Json;

namespace App.Console.Tests;

public class JunkWriter
{
    [Fact]
    public async void CreateTodoJsonFiles()
    {
        string path = "C:\\Users\\samue\\source\\repos\\Sandbox\\App.Console\\Data\\";

        IEnumerable<Todo> todos = [];
        using (StreamWriter writer = new StreamWriter(path + "todos.json"))
        {
            todos = Enumerable.Range(1, 10).Select(i => new Todo
            {
                Name = $"Item {i}",
                Done = Random.Shared.Next(0, 1) % 2 == 0
            });
            string content = JsonSerializer.Serialize(todos, options: new() { WriteIndented = true });
            await writer.WriteLineAsync(content);
        }            

        using (StreamReader reader = new StreamReader(path + "todos.json"))
        {
            IEnumerable<Todo>? writtenTodos = JsonSerializer.Deserialize<IEnumerable<Todo>>(await reader.ReadToEndAsync());
            writtenTodos.ShouldNotBeNull();
            writtenTodos.Count().ShouldBe(todos.Count());
        }
    }
}
