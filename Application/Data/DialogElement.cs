using Microsoft.JSInterop;

namespace Application.Data;

public class DialogElement
{
    #region Data Members

    private readonly IJSRuntime _jsRuntime;

    private readonly string _id;

    #endregion

    #region Constructor

    public DialogElement(IJSRuntime jsRuntime, string id)
    {
        _jsRuntime = jsRuntime;
        _id = id;
    }

    #endregion

    #region JavaScript Methods

    public async void OpenDialog()
    {
        await using IJSObjectReference module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/dialog.js");
        await module.InvokeVoidAsync("openDialog", _id);
    }

    public async void CloseDialog()
    {
        await using IJSObjectReference module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/dialog.js");
        await module.InvokeVoidAsync("closeDialog", _id);
    }

    public async Task OpenDialogAsync()
    {
        await using IJSObjectReference module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/dialog.js");
        await module.InvokeVoidAsync("openDialog", _id);
    }

    public async Task CloseDialogAsync()
    {
        await using IJSObjectReference module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/dialog.js");
        await module.InvokeVoidAsync("closeDialog", _id);
    }

    #endregion
}