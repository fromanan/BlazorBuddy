using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Extensions;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Models;

[PrimaryKey(nameof(Id))]
public class Window : IEntity<Window>
{
    #region Data Member
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key, Column("id")]
    public int Id { get; set; }

    [Column("title")]
    public string Title { get; set; } = "Window";
    
    [NotMapped]
    public bool IsCurrentWindow { get; set; }
    
    [Column("is_incognito")]
    public bool IsIncognito { get; set; }

    [ForeignKey(nameof(Tab))]
    public List<Tab> Tabs { get; set; } = new();

    #endregion
    
    #region Properties
    
    public int TabCount => Tabs.Count;

    public string SelectedWindowClass => $"{(IsCurrentWindow ? "selected-window" : string.Empty)}";
    
    public string WindowTitleClass => $"window-title{(IsCurrentWindow ? " highlight-accent" : string.Empty)}";

    #endregion
    
    #region Constructors

    // Default Constructor
    public Window() { }

    // Copy Constructor
    public Window(Window other)
    {
        Id = -1; //< Do not copy the id, this will cause database conflicts
        Title = other.Title is "Current Window" ? "Window" : other.Title;
        IsIncognito = other.IsIncognito;
        Tabs = other.Tabs.Copy().ToList();
    }

    #endregion

    #region Public Methods

    public Window Copy()
    {
        return new Window(this);
    }

    #endregion
}