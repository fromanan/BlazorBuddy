﻿using Application.Data;

namespace Application.Models;

public class Session
{
    #region Properties

    public int Id { get; set; }
    
    public string? Title { get; set; }
    
    public DateTime LastChange { get; set; }
    
    public DateTime? Created { get; set; }
    
    public SessionType SessionType { get; set; }

    public List<Window> Windows { get; set; } = new();
    
    public int Identifier { get; set; }
    
    public int WindowCount => Windows.Count;
    
    public int TabCount => Windows.Sum(w => w.TabCount);

    #endregion

    #region Constructors

    public Session() { }

    public Session(Session other)
    {
        Title = other.Title;
        LastChange = other.LastChange;
        SessionType = other.SessionType;
        Windows = other.Windows;
        Identifier = -1;
    }

    #endregion

    #region Public Methods

    public Session Copy()
    {
        return new Session(this);
    }

    #endregion
}