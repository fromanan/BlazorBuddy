using Microsoft.JSInterop;

namespace Application.Data;

public class DialogElement
{
    private readonly IJSRuntime _jsRuntime;

    private readonly string _id;

    public DialogElement(IJSRuntime jsRuntime, string id)
    {
        _jsRuntime = jsRuntime;
        _id = id;
    }


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
}