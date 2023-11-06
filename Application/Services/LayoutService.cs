using Application.Data;
using Application.Interfaces;
using Application.Models;
/*using Application.Elements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;*/

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

    #region Overlay

    /*private Overlay? _Overlay;

    public RenderFragment RenderOverlay()
    {
        return _RenderFragment;

        void _RenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent<Overlay>(0);
            builder.AddAttribute(1, nameof(Overlay.Id),  "overlay");
            builder.AddComponentReferenceCapture(2, overlay =>
            {
                _Overlay = (Overlay)overlay;
            });
            builder.CloseComponent();
        }
    }

    public void ShowOverlay()
    {
        _Overlay?.Show();
    }

    public void HideOverlay()
    {
        _Overlay?.Hide();
    }*/

    #endregion
}