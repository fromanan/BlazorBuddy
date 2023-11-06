using BlazorContextMenu;
using Microsoft.AspNetCore.Components;

namespace Application.Interfaces;

public interface IContextMenuService
{
    #region Methods

    T? GetMenu<T>(string id);

    RenderFragment RegisterNewMenu(string id, MouseButtonTrigger trigger, string? cssClass = null,
        RenderFragment? childContent = null);

    RenderFragment RenderContextMenu(string id);

    IEnumerable<RenderFragment> RenderContextMenus(params string[] ids);

    IEnumerable<RenderFragment> RenderAllContextMenus();

    #endregion
}