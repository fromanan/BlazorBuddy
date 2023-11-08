using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Models;

[Table("Tabs")]
[PrimaryKey(nameof(Id))]
public class Tab : IEntity<Tab>
{
    #region Data Members

    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key, Column("id")]
    public int Id { get; set; }
    
    [Column("title")]
    public string? Title { get; set; }
    
    [Column("url")]
    public string? Url { get; set; }
    
    [ForeignKey(nameof(Window)), Column("window_id")]
    public int WindowId { get; set; }

    #endregion
    
    #region Constructors

    // Default Constructor
    public Tab() { }

    // Copy Constructor
    public Tab(Tab other)
    {
        Id = -1; //< Do not copy the id, this will cause database conflicts
        Title = other.Title;
        Url = other.Url;
    }

    #endregion

    #region Public Methods

    public Tab Copy()
    {
        return new Tab(this);
    }

    #endregion
}