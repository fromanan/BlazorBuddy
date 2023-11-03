namespace Application.Models;

public class Window
{
    public string Title { get; set; } = "Window";
    
    public bool IsCurrentWindow { get; set; }

    public List<Tab> Tabs { get; set; } = new();
    
    public int TabCount => Tabs.Count;

    public string SelectedWindowClass => $"{(IsCurrentWindow ? "selected-window" : string.Empty)}";
}