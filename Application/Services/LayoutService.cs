using Application.Data;
using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class LayoutService : ILayoutService
{
    #region Properties

    public LayoutStyle ActiveLayoutStyle { get; private set; } = LayoutStyle.Default;
    
    public SortOrder ActiveSortOrder { get; private set; } = SortOrder.Default;

    #endregion

    #region Events

    public event Action<LayoutStyle> LayoutStyleChanged = delegate {  };
    
    public event Action<SortOrder> SortOrderChanged = delegate {  };

    #endregion

    #region Public Methods

    public void ChangeLayoutStyle(LayoutStyle layoutStyle)
    {
        if (ActiveLayoutStyle == layoutStyle)
            return;
        
        Console.WriteLine("Changing layout style!");

        ActiveLayoutStyle = layoutStyle;
        LayoutStyleChanged(layoutStyle);
    }

    public void ChangeSortOrder(SortOrder sortOrder = SortOrder.Default)
    {
        if (ActiveSortOrder == sortOrder)
            return;
        
        Console.WriteLine("Changing sort order!");

        ActiveSortOrder = sortOrder;
        SortOrderChanged(sortOrder);
    }
    
    public Session GetSessionByPosition(double x, double y)
    {
        return null!;
    }

    public Window GetWindowByPosition(double x, double y)
    {
        return null!;
    }

    public Tab GetTabByPosition(double x, double y)
    {
        return null!;
    }

    public void FocusWindow(int windowId)
    {
        
    }
    
    #endregion

    #region IUpdateTrigger Implementation

    public void SubscribeToUpdate(Action updateView)
    {
        LayoutStyleChanged += _ => updateView();
        SortOrderChanged   += _ => updateView();
    }

    #endregion
}