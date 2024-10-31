namespace Data.Entities;

public class Todo
{
    public string Name { get; set; } = string.Empty;
    public bool Done { get; set; } = false;

    public override string ToString()
        => $"Todo {{ Name: {Name}, Done: {Done} }}";
}
