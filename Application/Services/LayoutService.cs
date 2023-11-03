using Application.Data;
using Application.Elements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Application.Services;

public static class LayoutService
{
    public static LayoutStyle ActiveLayoutStyle { get; private set; } = LayoutStyle.Default;
    
    public static event Action<LayoutStyle> LayoutStyleChanged = delegate {  };
    
    public static void ChangeLayoutStyle(LayoutStyle layoutStyle)
    {
        if (ActiveLayoutStyle == layoutStyle)
            return;
        
        Console.WriteLine("Changing layout style!");

        ActiveLayoutStyle = layoutStyle;
        LayoutStyleChanged(layoutStyle);
    }
    
    public static SortOrder ActiveSortOrder { get; private set; } = SortOrder.Default;
    
    public static event Action<SortOrder> SortOrderChanged = delegate {  };

    public static void ChangeSortOrder(SortOrder sortOrder = SortOrder.Default)
    {
        if (ActiveSortOrder == sortOrder)
            return;
        
        Console.WriteLine("Changing sort order!");

        ActiveSortOrder = sortOrder;
        SortOrderChanged(sortOrder);
    }

    private static Overlay? _overlay;
    
    public static event Action OverlayChanged = delegate {  };

    public static RenderFragment RenderOverlay()
    {
        return _RenderFragment;

        void _RenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent<Overlay>(0);
            builder.AddAttribute(1, nameof(Overlay.Id),  "overlay");
            builder.AddComponentReferenceCapture(2, overlay =>
            {
                _overlay = (Overlay)overlay;
            });
            builder.CloseComponent();
        }
    }

    public static void ShowOverlay()
    {
        _overlay?.Show();
        OverlayChanged();
    }

    public static void HideOverlay()
    {
        _overlay?.Hide();
        OverlayChanged();
    }
}