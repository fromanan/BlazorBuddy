namespace Application.Models;

public class Window
{
    public int Id { get; set; }

    public string Title { get; set; } = "Window";
    
    public bool IsCurrentWindow { get; set; }
    
    public bool IsIncognito { get; set; }

    public List<Tab> Tabs { get; set; } = new();
    
    public int TabCount => Tabs.Count;

    public string SelectedWindowClass => $"{(IsCurrentWindow ? "selected-window" : string.Empty)}";
}