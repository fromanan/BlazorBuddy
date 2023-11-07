using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Data;
using Application.Extensions;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Models;

[PrimaryKey(nameof(Id))]
public class Session : IEntity<Session>
{
    #region Data Members
    
    public const string DEFAULT_NAME = "Unnamed session";

    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key, Column("id")]
    public int Id { get; set; } = -1;
    
    [Column("title")]
    public string? Title { get; set; }
    
    [Column("last_change")]
    public DateTime LastChange { get; set; }
    
    [Column("created")]
    public DateTime? Created { get; set; }
    
    [Column("session_type")]
    public SessionType SessionType { get; set; }
    
    [ForeignKey(nameof(Window))]
    public List<Window> Windows { get; set; } = new();

    #endregion
    
    #region Properties
    
    public int WindowCount => Windows.Count;
    
    public int TabCount => Windows.Sum(w => w.TabCount);

    #endregion

    #region Constructors

    // Default Constructor
    public Session() { }

    // Copy Constructor
    public Session(Session other)
    {
        Id = -1; //< Do not copy the id, this will cause database conflicts
        Title = other.Title;
        LastChange = other.LastChange;
        SessionType = other.SessionType;
        Windows = other.Windows.Copy().ToList();
    }

    #endregion

    #region Public Methods

    public Session Copy()
    {
        return new Session(this);
    }

    #endregion
}