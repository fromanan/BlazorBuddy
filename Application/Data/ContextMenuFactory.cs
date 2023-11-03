using Application.Elements.ContextMenus;
using BlazorContextMenu;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Application.Data;

public static class ContextMenuFactory
{
    private static readonly Dictionary<string, RenderFragment> _ContextMenus = new()
    {
        { ContextMenuId.WINDOW,     RenderMenu<WindowMenu>()   },
        { ContextMenuId.MAIN,       RenderMenu<MainMenu>()     },
        { ContextMenuId.SETTINGS,   RenderMenu<SettingsMenu>() },
        { ContextMenuId.OPEN,       RenderMenu<OpenMenu>()     },
        { ContextMenuId.OPTIONS,    RenderMenu<OptionsMenu>()  }
    };

    public static RenderFragment RegisterNewMenu(string id, MouseButtonTrigger trigger, string? cssClass = null, 
        RenderFragment? childContent = null)
    {
        if (!_ContextMenus.ContainsKey(id))
            throw new KeyNotFoundException($"Menu with id='{id}' not registered!");

        return _RenderFragment;

        void _RenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent(0, typeof(ContextMenuTrigger));
            builder.AddAttribute(1, nameof(ContextMenuTrigger.MenuId), id);
            builder.AddAttribute(2, nameof(ContextMenuTrigger.MouseButtonTrigger), trigger);
            builder.AddAttribute(3, nameof(ContextMenuTrigger.CssClass), $"context-menu-trigger {cssClass}");
            if (childContent is not null)
                builder.AddContent(4, childContent);
            builder.CloseComponent();
        }
    }

    public static RenderFragment RenderContextMenu(string id)
    {
        return _ContextMenus[id];
    }
    
    public static IEnumerable<RenderFragment> RenderContextMenus(params string[] ids)
    {
        return ids.Length <= 0 ? _ContextMenus.Values : ids.Select(id => _ContextMenus[id]);
    }
    
    public static RenderFragment RenderMenu<T>() where T : IComponent
    {
        return _RenderFragment;

        void _RenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent(1, typeof(T));
            builder.CloseComponent();
        }
    }
}