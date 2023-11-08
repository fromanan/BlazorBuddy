using Microsoft.AspNetCore.Components;

namespace Application.Interfaces;

public interface IDialogService
{
    #region Methods

    void ShowDialog(string id, Action<bool, object?>? callback = null);

    Task<bool> ShowDialogAsync(string id, Action<bool, object?>? callback = null);
    
    Task<bool> ShowDialogAsync(string id, Func<bool, object?, Task> asyncCallback);

    RenderFragment RenderDialog(string id);

    IEnumerable<RenderFragment> RenderDialogs(params string[] ids);

    IEnumerable<RenderFragment> RenderAllDialogs();

    #endregion
}