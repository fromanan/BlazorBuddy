using Application.Data;
using Application.Elements.ContextMenus;
using Application.Interfaces;
using BlazorContextMenu;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Application.Services;

public class ContextMenuService : IContextMenuService
{
    #region Data Members

    private static readonly Dictionary<string, RenderFragment> _ContextMenuFragments = new()
    {
        { ContextMenuId.MAIN,       RenderMenu<MainMenu>(ContextMenuId.MAIN)         },
        { ContextMenuId.SETTINGS,   RenderMenu<SettingsMenu>(ContextMenuId.SETTINGS) },
        { ContextMenuId.WINDOW,     RenderMenu<WindowMenu>(ContextMenuId.WINDOW)     },
        { ContextMenuId.OPEN,       RenderMenu<OpenMenu>(ContextMenuId.OPEN)         },
        { ContextMenuId.OPTIONS,    RenderMenu<OptionsMenu>(ContextMenuId.OPTIONS)   },
        { ContextMenuId.SESSION,    RenderMenu<SessionMenu>(ContextMenuId.SESSION)   }
    };
    
    private static readonly Dictionary<string, object?> _ContextMenus = new()
    {
        { ContextMenuId.MAIN,       null },
        { ContextMenuId.SETTINGS,   null },
        { ContextMenuId.WINDOW,     null },
        { ContextMenuId.OPEN,       null },
        { ContextMenuId.OPTIONS,    null },
        { ContextMenuId.SESSION,    null }
    };

    #endregion

    #region Public Methods

    public T? GetMenu<T>(string id) => (T?) _ContextMenus[id];

    #endregion

    #region Rendering
    
    public RenderFragment RenderContextMenu(string id)
    {
        return _ContextMenuFragments[id];
    }
    
    public IEnumerable<RenderFragment> RenderContextMenus(params string[] ids)
    {
        return ids.Length <= 0 ? _ContextMenuFragments.Values : ids.Select(id => _ContextMenuFragments[id]);
    }
    
    public IEnumerable<RenderFragment> RenderAllContextMenus()
    {
        return _ContextMenuFragments.Values;
    }
    
    public RenderFragment RenderMenuHandler(string id, MouseButtonTrigger trigger, string? cssClass = null, 
        RenderFragment? childContent = null)
    {
        if (!_ContextMenuFragments.ContainsKey(id))
            throw new KeyNotFoundException($"Menu with id='{id}' not registered!");

        return _RenderFragment;

        void _RenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent<ContextMenuTrigger>(0);
            builder.AddAttribute(1, nameof(ContextMenuTrigger.MenuId), id);
            builder.AddAttribute(2, nameof(ContextMenuTrigger.MouseButtonTrigger), trigger);
            builder.AddAttribute(3, nameof(ContextMenuTrigger.CssClass), $"context-menu-trigger {cssClass}");
            if (childContent is not null)
                builder.AddContent(4, childContent);
            builder.CloseComponent();
        }
    }

    private static RenderFragment RenderMenu<T>(string id) where T : IComponent
    {
        return _RenderFragment;

        void _RenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent<T>(1);
            builder.AddComponentReferenceCapture(2, componentReference =>
            {
                _ContextMenus[id] = componentReference;
            });
            builder.CloseComponent();
        }
    }

    #endregion
}